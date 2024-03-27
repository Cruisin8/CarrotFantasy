using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.VirtualTexturing;

/// <summary>
/// UI�㼶
/// </summary>
public enum E_UI_Layer
{
	BOT,
	MID,
	TOP,
	SYSTEM,
}

/// <summary>
/// UI������
/// 1.����������ʾ�����
/// 2.�ṩ���ⲿ ��ʾ�����صȽӿ�
/// </summary>
public class UIMgr : BaseManager<UIMgr>
{
	// ���panel����
	public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

	// ���ĸ����㼶
	private Transform bot;
	private Transform mid;
	private Transform top;
	private Transform system;

	// ��¼UI��Canvas�������ṩ���ⲿʹ��
	public RectTransform canvas;

	public UIMgr()
	{
		// ͬ�����ش���Canvas��������ʱ���Ƴ�
		GameObject obj = ResMgr.GetInstance().Load<GameObject>("UI/Canvas");
		canvas = obj.transform as RectTransform;
		GameObject.DontDestroyOnLoad(obj);

		// �ҵ�����
		bot = canvas.Find("Bot");
		mid = canvas.Find("Mid");
		top = canvas.Find("Top");
		system = canvas.Find("System");

		// ͬ�����ش���EventSystem��������ʱ���Ƴ�
		obj = ResMgr.GetInstance().Load<GameObject>("UI/EventSystem");
		GameObject.DontDestroyOnLoad(obj);
	}

	// ��ȡ���㼶
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
	/// ��ʾ���
	/// </summary>
	/// <typeparam name="T">���ű�����</typeparam>
	/// <param name="panelName">�����</param>
	/// <param name="layer">��ʾ�Ĳ㼶</param>
	/// <param name="callback">�����Ԥ���崴����ɺ�ִ�иú���</param>
	public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.MID, UnityAction<T> callback = null) where T : BasePanel
	{
		// �������Ѵ��ڣ�˵����屻�ظ����أ���ֱ�ӵ��ûص�������Ȼ�󷵻أ������ظ�ִ���첽�����߼�
		if(panelDic.ContainsKey(panelName)) {
			panelDic[panelName].ShowMe();
			if(callback != null) {
				callback(panelDic[panelName] as T);
				return;
			}
		}

		// �첽������岢��ʼ��
		ResMgr.GetInstance().LoadAsync<GameObject>("UI/" + panelName, (obj) => {
			// �����panel��ΪCanvas���Ӷ��󣬲�����panel�����λ��
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

			// ���ø�����
			obj.transform.SetParent(father);
			// ���ó�ʼ���λ�úʹ�С
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localScale = Vector3.one;
			// ���ó�ʼƫ�ƴ�С
			(obj.transform as RectTransform).offsetMax = Vector2.zero;
			(obj.transform as RectTransform).offsetMin = Vector2.zero;

			// �õ�Ԥ�������ϵ����ű�
			T panel = obj.GetComponent<T>();
			// ��������ɺ��ٴ�����崴����ɺ���߼�
			if(callback != null) {
				callback(panel);
			}

			// ��ʾ���ʱ���õ���ʾ��庯��
			panel.ShowMe();

			// �������
			panelDic.Add(panelName, panel);
		});
	}

	// �������
	public void HidePanel(string panelName) 
	{
		if(panelDic.ContainsKey(panelName)) {
			// �������ʱ���õ�������غ���
			panelDic[panelName].HideMe();
			// ��������gameObject
			GameObject.Destroy(panelDic[panelName].gameObject);
			// ���������Ƴ����
			panelDic.Remove(panelName);
		}
	}

	// ��ȡĳ���Ѿ���ʾ����壬�����ⲿʹ��
	public T GetPanel<T>(string panelName) where T : BasePanel
	{
		if(panelDic.ContainsKey(panelName)) {
			return panelDic[panelName] as T;
		}

		return null;
	}

	/// <summary>
	/// ��ĳ���ؼ�����Զ�������¼�
	/// ����UIMgr�Ĺ���������д�ɾ�̬����
	/// </summary>
	/// <param name="control">�ؼ�����</param>
	/// <param name="type">�¼�����</param>
	/// <param name="callback">�¼���Ӧ����</param>
	public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callback) 
	{
		EventTrigger trigger = control.GetComponent<EventTrigger>();
		if(trigger == null) {
			// ���û�д������������һ��������
			trigger = control.gameObject.AddComponent<EventTrigger>();
		}

		// ��Ӽ�������Ӧ�¼�
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = type;
		entry.callback.AddListener(callback);

		trigger.triggers.Add(entry);
	}
}
