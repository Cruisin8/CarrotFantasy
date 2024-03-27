using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于测试ResMgr 资源加载功能
/// </summary>
public class ResTest : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			// 点击左键，从res资源同步加载一个ResCube
			GameObject obj = ResMgr.GetInstance().Load<GameObject>("Test/BaseTest/BaseTestPrefabs/TestPrefabs/ResCube");
			// 测试获取到的返回值
			Debug.Log("同步加载资源："+obj.name);
		}
		else if(Input.GetMouseButtonDown(1)) {
			// 点击右键，从res资源异步加载一个ResSphere，等资源真正加载出来后，再执行xxx，用调用函数的方式写
			ResMgr.GetInstance().LoadAsync<GameObject>("Test/BaseTest/BaseTestPrefabs/TestPrefabs/ResSphere", DoSomething);

			// 点击右键，从res资源异步加载一个ResSphere，等资源真正加载出来后，再执行xxx，用lamda表达式写需要执行的函数
			ResMgr.GetInstance().LoadAsync<GameObject>("Test/BaseTest/BaseTestPrefabs/TestPrefabs/ResSphere", (obj) => {
				Debug.Log("异步加载资源：" + obj.name + "并执行lamda表达式形式的xxx函数");
			});
		}
	}

	// 执行xxx函数
	private void DoSomething(GameObject obj)
	{
		// 异步加载，等资源真正加载出来后，再执行xxx.
		Debug.Log("异步加载资源：" + obj.name + "并执行xxx函数");
	}
}
