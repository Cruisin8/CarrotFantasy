using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWin : View
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	public TMP_Text txtCurrent;
	public TMP_Text txtTotal;
	public Button btnRestart;
	public Button btnContinue;

	#endregion

	#region 属性
	public override string Name {
		get { return Consts.V_Win; }
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

	void UpdateRoundInfo(int currentRound, int totalRound)
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

	// 重新开始该关卡
	public void OnRestartClick()
	{
		GameModel gModel = GetModel<GameModel>();
		SendEvent(Consts.E_StartLevel, new StartLevelArgs() { LevelIndex = gModel.PlayLevelID });
	}

	// 继续进行下一关
	public void OnContinueClick()
	{
		GameModel gModel = GetModel<GameModel>();
		if(gModel.PlayLevelID >= gModel.LevelCount - 1) {
			// 游戏通关
			Game.GetInstance().LoadScene((int)SceneID.Complete);
			return;
		}

		// 开始下一关
		SendEvent(Consts.E_StartLevel, new StartLevelArgs() { LevelIndex = gModel.PlayLevelID + 1 });
	}
	#endregion

	#region 帮助方法
	#endregion
}
