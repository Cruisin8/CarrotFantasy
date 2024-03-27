using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIComplete : View
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	public Button btnSelect;
	public Button btnClear;
	#endregion

	#region 属性
	public override string Name {
		get { return Consts.V_Complete; }
	}
	#endregion

	#region 方法
	#endregion

	#region Unity回调
	#endregion

	#region 事件回调
	public override void RegisterEvents()
	{

	}

	public override void HandleEvent(string eventName, object data)
	{
		
	}

	// 选择关卡按钮
	public void OnSelectClick()
	{
		// 回到开始界面
		Game.GetInstance().LoadScene((int)SceneID.Start);
	}

	// 清档按钮
	public void OnClearClick()
	{
		GameModel gModel = GetModel<GameModel>();
		gModel.ClearProgress();
	}
	#endregion

	#region 帮助方法
	#endregion


}
