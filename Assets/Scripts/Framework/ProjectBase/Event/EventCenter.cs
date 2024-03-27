using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ͨ���¼���Ϣ�ӿڣ�����ʵ�ֲ�ͬ���͵��¼���Ϣ
/// �ýӿڽ��¼����װ����ʽת��ԭ�򣩣�
///		1.���Բ�ʹ�÷��ͣ���֤EventCenter�ľ�̬������
///		2.���Ա���ʹ��object�ඨ���¼������ٷ���������
/// </summary>
public interface IEventInfo { }

/// <summary>
/// ���ڱ������һ���������¼���Ϣ�ķ�����
/// </summary>
/// <typeparam name="T">�¼����������͡�</typeparam>
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
	{
        actions += action;
	}
}

/// <summary>
/// ���ڱ��治���������¼���Ϣ����
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
/// �¼�����
/// 1.Dictionary
/// 2.ί��
/// 3.�۲���ģʽ
/// </summary>
public class EventCenter : BaseManager<EventCenter> 
{
    // �¼�����
    // key���¼���
    // value�������¼���Ӧ��ί�к���
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

	// ����¼�����
	// eventName���¼���
	// action��׼�����������¼���ί�к���  ����T��ί�еĶ�������ʶ������ľ������
	public void AddEventListener<T>(string eventName, UnityAction<T> action)
    {
        if(eventDic.ContainsKey(eventName)) {
            // ���и��¼��������ί�к���
            (eventDic[eventName] as EventInfo<T>).actions += action;
        }
        else {
            // û�и��¼������½�
            eventDic.Add(eventName, new EventInfo<T>(action));
        }
    }

	// ����¼����������أ� �ʺϲ���Ҫ���ݲ������¼�
	// eventName���¼���
	// action��׼�����������¼���ί�к���  �޷���
	public void AddEventListener(string eventName, UnityAction action)
	{
		if (eventDic.ContainsKey(eventName)) {
			// ���и��¼��������ί�к���
			(eventDic[eventName] as EventInfo).actions += action;
		}
		else {
			// û�и��¼������½�
			eventDic.Add(eventName, new EventInfo(action));
		}
	}

	// �Ƴ���������Ҫ���ڶ�������ʱ�����������ϵ��
	// eventName���¼���
	// action��׼�����������¼���ί�к���  ����T��ί�еĶ�������ʶ������ľ������
	public void RemoveEventListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName)) {
            (eventDic[eventName] as EventInfo<T>).actions -= action;
        }
    }

	// �Ƴ����������أ� �ʺϲ���Ҫ���ݲ������¼�
	// eventName���¼���
	// action��׼�����������¼���ί�к���  �޷���
	public void RemoveEventListener(string eventName, UnityAction action)
	{
		if (eventDic.ContainsKey(eventName)) {
			(eventDic[eventName] as EventInfo).actions -= action;
		}
	}

	// �¼�����
	// eventName���¼���
	// info��ί�ж���һ��Ϊί�ж����Լ�this��
	public void EventTrigger<T>(string eventName, T info)
    {
        // ���и��¼����򼤻�ί�к���
        // û�и��¼��������
        if(eventDic.ContainsKey(eventName)) {
            if((eventDic[eventName] as EventInfo<T>).actions != null) {
				(eventDic[eventName] as EventInfo<T>).actions.Invoke(info);
			}
        }
    }

	// �¼����������أ� �ʺϲ���Ҫ���ݲ������¼�
	// eventName���¼���
	public void EventTrigger(string eventName)
	{
		// ���и��¼����򼤻�ί�к���
		// û�и��¼��������
		if (eventDic.ContainsKey(eventName)) {
			if ((eventDic[eventName] as EventInfo).actions != null) {
				(eventDic[eventName] as EventInfo).actions.Invoke();
			}
		}
	}

	// ����¼����ģ���Ҫ���ڳ����л��У�
	public void Clear()
    {
        eventDic.Clear();
    }

}
