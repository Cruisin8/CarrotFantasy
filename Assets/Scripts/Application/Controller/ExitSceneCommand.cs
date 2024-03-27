using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSceneCommand : Controller
{
	public override void Execute(object data)
	{
		// 离开场景前清空缓存池
		PoolMgr.GetInstance().Clear();
	}
}
