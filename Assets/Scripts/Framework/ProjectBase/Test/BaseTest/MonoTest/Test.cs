using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	private void Start()
	{
		// ���������й���Mono����
		MonoTest t = new MonoTest();
		MonoMgr.GetInstance().AddUpdateListener(t.Update);
	}
}