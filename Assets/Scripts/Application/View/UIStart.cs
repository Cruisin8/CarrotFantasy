using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : View
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	#endregion

	#region 属性
	public override string Name {
		get { return Consts.V_Start; }
	}
	#endregion

	#region 方法
	// 跳转关卡选择场景 btnAdventure
	public void GotoSelect()
	{
		Game.GetInstance().LoadScene((int)SceneID.Select);

		// 场景切换时销毁开始界面的循环动画
		DOTween.KillAll();
	}
	#endregion

	#region Unity回调
	#endregion

	#region 事件回调
	public override void HandleEvent(string eventName, object data)
	{
		throw new System.NotImplementedException();
	}
	#endregion

	#region 帮助方法
	#endregion
}
