using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于测试PoolMgr 从缓存池中获取的功能
/// </summary>
public class PoolGetTest : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			// 点击左键，从缓存池中获取一个Cube
			PoolMgr.GetInstance().GetObj("Test/BaseTest/BaseTestPrefabs/TestPrefabs/Cube", (obj) => {
				Debug.Log("异步加载Cube完成");
			});
		}
		else if(Input.GetMouseButtonDown(1)) {
			// 点击右键，从缓存池中获取一个Sphere
			PoolMgr.GetInstance().GetObj("Test/BaseTest/BaseTestPrefabs/TestPrefabs/Sphere", (obj) => {
				Debug.Log("异步加载Sphere完成");
			});
		}
	}
}
