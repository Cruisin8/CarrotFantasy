using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	private void Start()
	{
		// 在主程序中挂载Mono监听
		MonoTest t = new MonoTest();
		MonoMgr.GetInstance().AddUpdateListener(t.Update);
	}
}