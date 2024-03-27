using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 公共Mono模块
/// </summary>
public class MonoController : MonoBehaviour
{
	public event UnityAction updateEvent;

	private void Start()
	{
		DontDestroyOnLoad(this.gameObject);

	}

	private void Update()
	{
		if (updateEvent != null) {
			updateEvent();
		}
	}

	// 添加帧更新事件
	public void AddUpdateListener(UnityAction fun)
	{
		updateEvent += fun;
	}

	// 移除帧更新事件
	public void RemoveUpdateListener(UnityAction fun)
	{
		updateEvent -= fun;
	}
}
