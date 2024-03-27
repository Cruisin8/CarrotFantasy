using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÓÃÓÚ²âÊÔMonoÄ£¿é
/// </summary>
public class MonoTest
{
	public void Update()
	{
		Debug.Log("MonoTest");
	}

	public MonoTest() 
	{ 
		MonoMgr.GetInstance().StartCoroutine(IEnumeratorTest());
	}

	IEnumerator IEnumeratorTest()
	{
		yield return new WaitForSeconds (1f);
		Debug.Log("IEnumeratorTest");
	}
}
