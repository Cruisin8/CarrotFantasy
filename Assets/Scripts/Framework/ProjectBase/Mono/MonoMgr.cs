using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono管理器（封装了MonoBehaviour）
/// 1.提供给外部添加帧更新事件的方法
/// 2.提供给外部添加协程的方法
/// </summary>
public class MonoMgr : BaseManager<MonoMgr> 
{
    private MonoController controller;

    public MonoMgr()
    {
		// 通过BaseManager单例创建，保证了MonoController的唯一性
		GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }

	// 添加帧更新事件
	public void AddUpdateListener(UnityAction fun)
	{
		controller.AddUpdateListener(fun);
	}

	// 移除帧更新事件
	public void RemoveUpdateListener(UnityAction fun)
	{
		controller.RemoveUpdateListener(fun);
	}

	// 封装协程函数（与Unity自带的协程函数一致）
	public Coroutine StartCoroutine(IEnumerator routine)
	{
		return controller.StartCoroutine(routine);
	}

	public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
	{
		return controller.StartCoroutine(methodName, value);
	}

	public Coroutine StartCoroutine(string methodName)
	{
		return controller.StartCoroutine(methodName);
	}
}
