using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 缓存池接口
public interface IReusable
{
	//当取出时调用
	void OnGetObj();

	//当回收时调用
	void OnPushObj();
}
