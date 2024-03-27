using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MVC
{
	// �洢MVC
	// ����--ģ��
	public static Dictionary<string, Model> Models = new Dictionary<string, Model>();
	// ����--��ͼ
	public static Dictionary<string, View> Views = new Dictionary<string, View>();
    // �¼�����--����������
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();

	// ע��ģ��
	public static void RegisterModel(Model model)
	{
		Models[model.Name] = model;
	}

	// ע����ͼ
	public static void RegisterView(View view)
	{
		// ��ֹ�ظ�ע��
		if(Views.ContainsKey(view.Name)) {
			Views.Remove(view.Name);
		}

		// ע����ĵ��¼�
		view.RegisterEvents();

		// �����¼�
		Views[view.Name] = view;
	}

	// ע�������
	public static void RegisterController(String eventName, Type controllerType)
	{
		CommandMap[eventName] = controllerType;
	}

	// ��ȡģ��
	public static T GetModel<T>() where T : Model
	{
		foreach(Model m in Models.Values) {
			if(m is T) {
				return (T)m;
			}
		}

		return null;
	}

	// ��ȡ��ͼ
	public static T GetView<T>() where T : View
	{
		foreach (View v in Views.Values) {
			if (v is T) {
				return (T)v;
			}
		}

		return null;
	}

	// �����¼�
	// eventName ���¼�����
	// data���¼���Ҫ�����ݣ�Ĭ��Ϊnull
	public static void SendEvent(string eventName, object data = null)
	{
		// ��������Ӧ�¼�
		if (CommandMap.ContainsKey(eventName)) {
			Type t = CommandMap[eventName];
			Controller c = Activator.CreateInstance(t) as Controller;
			// ������ִ��
			c.Execute(data);
		}

		// ��ͼ��Ӧ�¼�
		foreach(View v in Views.Values) {
			if(v.AttentionEvents.Contains(eventName)) {
				// �����ͼ�Դ��¼�������������ͼ��Ӧ���¼�
				v.HandleEvent(eventName, data);
			}
		}
	}

}
