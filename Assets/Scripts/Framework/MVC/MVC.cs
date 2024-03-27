using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MVC
{
	// 存储MVC
	// 名字--模型
	public static Dictionary<string, Model> Models = new Dictionary<string, Model>();
	// 名字--视图
	public static Dictionary<string, View> Views = new Dictionary<string, View>();
    // 事件名字--控制器类型
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();

	// 注册模型
	public static void RegisterModel(Model model)
	{
		Models[model.Name] = model;
	}

	// 注册视图
	public static void RegisterView(View view)
	{
		// 防止重复注册
		if(Views.ContainsKey(view.Name)) {
			Views.Remove(view.Name);
		}

		// 注册关心的事件
		view.RegisterEvents();

		// 缓存事件
		Views[view.Name] = view;
	}

	// 注册控制器
	public static void RegisterController(String eventName, Type controllerType)
	{
		CommandMap[eventName] = controllerType;
	}

	// 获取模型
	public static T GetModel<T>() where T : Model
	{
		foreach(Model m in Models.Values) {
			if(m is T) {
				return (T)m;
			}
		}

		return null;
	}

	// 获取视图
	public static T GetView<T>() where T : View
	{
		foreach (View v in Views.Values) {
			if (v is T) {
				return (T)v;
			}
		}

		return null;
	}

	// 发送事件
	// eventName ：事件名称
	// data：事件需要的数据，默认为null
	public static void SendEvent(string eventName, object data = null)
	{
		// 控制器响应事件
		if (CommandMap.ContainsKey(eventName)) {
			Type t = CommandMap[eventName];
			Controller c = Activator.CreateInstance(t) as Controller;
			// 控制器执行
			c.Execute(data);
		}

		// 视图响应事件
		foreach(View v in Views.Values) {
			if(v.AttentionEvents.Contains(eventName)) {
				// 如果视图对此事件监听，则让视图响应此事件
				v.HandleEvent(eventName, data);
			}
		}
	}

}
