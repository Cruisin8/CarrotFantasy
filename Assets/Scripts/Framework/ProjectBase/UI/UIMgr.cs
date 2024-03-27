using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.VirtualTexturing;

/// <summary>
/// UI层级
/// </summary>
public enum E_UI_Layer
{
	BOT,
	MID,
	TOP,
	SYSTEM,
}

/// <summary>
/// UI管理器
/// 1.管理所有显示的面板
/// 2.提供给外部 显示、隐藏等接口
/// </summary>
public class UIMgr : BaseManager<UIMgr>
{
	// 面板panel容器
	public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

	// 面板的各个层级
	private Transform bot;
	private Transform mid;
	private Transform top;
	private Transform system;

	// 记录UI的Canvas父对象，提供给外部使用
	public RectTransform canvas;

	public UIMgr()
	{
		// 同步加载创建Canvas，过场景时不移除
		GameObject obj = ResMgr.GetInstance().Load<GameObject>("UI/Canvas");
		canvas = obj.transform as RectTransform;
		GameObject.DontDestroyOnLoad(obj);

		// 找到各层
		bot = canvas.Find("Bot");
		mid = canvas.Find("Mid");
		top = canvas.Find("Top");
		system = canvas.Find("System");

		// 同步加载创建EventSystem，过场景时不移除
		obj = ResMgr.GetInstance().Load<GameObject>("UI/EventSystem");
		GameObject.DontDestroyOnLoad(obj);
	}

	// 获取面板层级
	public Transform GetLayerFather(E_UI_Layer layer)
	{
		switch (layer) {
			case E_UI_Layer.BOT:
				return bot;
			case E_UI_Layer.MID:
				return mid;
			case E_UI_Layer.TOP:
				return top;
			case E_UI_Layer.SYSTEM:
				return system;
		}

		return null;
	}

	/// <summary>
	/// 显示面板
	/// </summary>
	/// <typeparam name="T">面板脚本类型</typeparam>
	/// <param name="panelName">面板名</param>
	/// <param name="layer">显示的层级</param>
	/// <param name="callback">当面板预设体创建完成后，执行该函数</param>
	public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.MID, UnityAction<T> callback = null) where T : BasePanel
	{
		// 如果面板已存在，说明面板被重复加载，则直接调用回调函数，然后返回，不再重复执行异步加载逻辑
		if(panelDic.ContainsKey(panelName)) {
			panelDic[panelName].ShowMe();
			if(callback != null) {
				callback(panelDic[panelName] as T);
				return;
			}
		}

		// 异步加载面板并初始化
		ResMgr.GetInstance().LoadAsync<GameObject>("UI/" + panelName, (obj) => {
			// 把这个panel作为Canvas的子对象，并设置panel的相对位置
			Transform father = bot;
			switch(layer) {
				case E_UI_Layer.BOT:
					father = bot;
					break;
				case E_UI_Layer.MID:
					father = mid;
					break;
				case E_UI_Layer.TOP:
					father = top;
					break;
				case E_UI_Layer.SYSTEM:
					father = system;
					break;
			}

			// 设置父对象
			obj.transform.SetParent(father);
			// 设置初始相对位置和大小
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localScale = Vector3.one;
			// 设置初始偏移大小
			(obj.transform as RectTransform).offsetMax = Vector2.zero;
			(obj.transform as RectTransform).offsetMin = Vector2.zero;

			// 得到预设体身上的面板脚本
			T panel = obj.GetComponent<T>();
			// 面板加载完成后，再处理面板创建完成后的逻辑
			if(callback != null) {
				callback(panel);
			}

			// 显示面板时调用的显示面板函数
			panel.ShowMe();

			// 保存面板
			panelDic.Add(panelName, panel);
		});
	}

	// 隐藏面板
	public void HidePanel(string panelName) 
	{
		if(panelDic.ContainsKey(panelName)) {
			// 隐藏面板时调用的面板隐藏函数
			panelDic[panelName].HideMe();
			// 销毁面板的gameObject
			GameObject.Destroy(panelDic[panelName].gameObject);
			// 从容器中移除面板
			panelDic.Remove(panelName);
		}
	}

	// 获取某个已经显示的面板，方便外部使用
	public T GetPanel<T>(string panelName) where T : BasePanel
	{
		if(panelDic.ContainsKey(panelName)) {
			return panelDic[panelName] as T;
		}

		return null;
	}

	/// <summary>
	/// 给某个控件添加自定义监听事件
	/// 属于UIMgr的公共方法，写成静态函数
	/// </summary>
	/// <param name="control">控件对象</param>
	/// <param name="type">事件类型</param>
	/// <param name="callback">事件响应函数</param>
	public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callback) 
	{
		EventTrigger trigger = control.GetComponent<EventTrigger>();
		if(trigger == null) {
			// 如果没有触发器，则挂载一个触发器
			trigger = control.gameObject.AddComponent<EventTrigger>();
		}

		// 添加监听的响应事件
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = type;
		entry.callback.AddListener(callback);

		trigger.triggers.Add(entry);
	}
}
