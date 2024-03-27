using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILose : View
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	public TMP_Text txtCurrent;
	public TMP_Text txtTotal;
	public Button btnRestart;
	#endregion

	#region 属性
	public override string Name {
		get { return Consts.V_Lost; }
	}
	#endregion

	#region 方法
	public void Show()
	{
		this.gameObject.SetActive(true);

		RoundModel rModel = GetModel<RoundModel>();
		UpdateRoundInfo(rModel.RoundIndex + 1, rModel.RoundTotal);
	}

	public void Hide()
	{
		this.gameObject.SetActive(false);
	}

	public void UpdateRoundInfo(int currentRound, int totalRound)
	{
		txtCurrent.text = currentRound.ToString("D2");
		txtTotal.text = totalRound.ToString();
	}
	#endregion

	#region Unity回调
	void Awake()
	{
		UpdateRoundInfo(0, 0);
	}
	#endregion

	#region 事件回调
	public override void HandleEvent(string eventName, object data)
	{

	}

	public void OnRestartClick()
	{
		GameModel gModel = GetModel<GameModel>();
		SendEvent(Consts.E_StartLevel, new StartLevelArgs() { LevelIndex = gModel.PlayLevelID });
	}
	#endregion

	#region 帮助方法
	#endregion
}
