using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
	// ��ͼ��ʶ
	public abstract string Name { get; }

	// �������¼��б�
	[HideInInspector]
	public List<string> AttentionEvents = new List<string>();

	public virtual void RegisterEvents(){ }

	// �¼�������
	public abstract void HandleEvent(string eventName, object data);

	// ��ȡģ��
	protected T GetModel<T>() where T : Model
	{
		return MVC.GetModel<T>() as T;
	}

	// �����¼�
	protected void SendEvent(string eventName, object data = null)
	{
		MVC.SendEvent(eventName, data);
	}

}
