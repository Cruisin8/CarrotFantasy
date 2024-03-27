using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 缓存池对象
public abstract class ReusbleObject : MonoBehaviour, IReusable
{
	//当取出时调用
	public abstract void OnGetObj();

	//当回收时调用
	public abstract void OnPushObj();
}
