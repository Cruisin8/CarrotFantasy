using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 通用事件信息接口，用于实现不同类型的事件信息
/// 用接口将事件类封装（里式转换原则）：
///		1.可以不使用泛型，保证EventCenter的静态类属性
///		2.可以避免使用object类定义事件，减少封箱拆箱操作
/// </summary>
public interface IEventInfo { }

/// <summary>
/// 用于保存具有一个参数的事件信息的泛型类
/// </summary>
/// <typeparam name="T">事件参数的类型。</typeparam>
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
	{
        actions += action;
	}
}

/// <summary>
/// 用于保存不带参数的事件信息的类
/// </summary>
public class EventInfo : IEventInfo
{
	public UnityAction actions;

	public EventInfo(UnityAction action)
	{
		actions += action;
	}
}


/// <summary>
/// 事件中心
/// 1.Dictionary
/// 2.委托
/// 3.观察者模式
/// </summary>
public class EventCenter : BaseManager<EventCenter> 
{
    // 事件中心
    // key：事件名
    // value：监听事件对应的委托函数
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

	// 添加事件监听
	// eventName：事件名
	// action：准备用来处理事件的委托函数  泛型T填委托的对象（用于识别监听的具体对象）
	public void AddEventListener<T>(string eventName, UnityAction<T> action)
    {
        if(eventDic.ContainsKey(eventName)) {
            // 已有该事件，则添加委托函数
            (eventDic[eventName] as EventInfo<T>).actions += action;
        }
        else {
            // 没有该事件，则新建
            eventDic.Add(eventName, new EventInfo<T>(action));
        }
    }

	// 添加事件监听（重载） 适合不需要传递参数的事件
	// eventName：事件名
	// action：准备用来处理事件的委托函数  无泛型
	public void AddEventListener(string eventName, UnityAction action)
	{
		if (eventDic.ContainsKey(eventName)) {
			// 已有该事件，则添加委托函数
			(eventDic[eventName] as EventInfo).actions += action;
		}
		else {
			// 没有该事件，则新建
			eventDic.Add(eventName, new EventInfo(action));
		}
	}

	// 移除监听（主要用于对象被销毁时，解除监听关系）
	// eventName：事件名
	// action：准备用来处理事件的委托函数  泛型T填委托的对象（用于识别监听的具体对象）
	public void RemoveEventListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName)) {
            (eventDic[eventName] as EventInfo<T>).actions -= action;
        }
    }

	// 移除监听（重载） 适合不需要传递参数的事件
	// eventName：事件名
	// action：准备用来处理事件的委托函数  无泛型
	public void RemoveEventListener(string eventName, UnityAction action)
	{
		if (eventDic.ContainsKey(eventName)) {
			(eventDic[eventName] as EventInfo).actions -= action;
		}
	}

	// 事件触发
	// eventName：事件名
	// info：委托对象（一般为委托对象自己this）
	public void EventTrigger<T>(string eventName, T info)
    {
        // 已有该事件，则激活委托函数
        // 没有该事件，则忽视
        if(eventDic.ContainsKey(eventName)) {
            if((eventDic[eventName] as EventInfo<T>).actions != null) {
				(eventDic[eventName] as EventInfo<T>).actions.Invoke(info);
			}
        }
    }

	// 事件触发（重载） 适合不需要传递参数的事件
	// eventName：事件名
	public void EventTrigger(string eventName)
	{
		// 已有该事件，则激活委托函数
		// 没有该事件，则忽视
		if (eventDic.ContainsKey(eventName)) {
			if ((eventDic[eventName] as EventInfo).actions != null) {
				(eventDic[eventName] as EventInfo).actions.Invoke();
			}
		}
	}

	// 清空事件中心（主要用于场景切换中）
	public void Clear()
    {
        eventDic.Clear();
    }

}
