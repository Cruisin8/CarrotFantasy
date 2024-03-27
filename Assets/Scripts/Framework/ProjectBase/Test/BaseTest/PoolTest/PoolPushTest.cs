using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于测试PoolMgr 放回缓存池的功能
/// </summary>
public class PoolPushTest : MonoBehaviour
{
	private void OnEnable()
	{
		// 延迟1s后，自动将对象放回缓存池
		Invoke("Push", 1);
	}

	void Push()
	{
		PoolMgr.GetInstance().PushObj(this.gameObject);
	}
}
