using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWin : View
{
	#region ����
	#endregion

	#region �¼�
	#endregion

	#region �ֶ�
	public TMP_Text txtCurrent;
	public TMP_Text txtTotal;
	public Button btnRestart;
	public Button btnContinue;

	#endregion

	#region ����
	public override string Name {
		get { return Consts.V_Win; }
	}
	#endregion

	#region ����
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

	#region Unity�ص�
	void Awake()
	{
		UpdateRoundInfo(0, 0);
	}
	#endregion

	#region �¼��ص�
	public override void HandleEvent(string eventName, object data)
	{

	}

	// ���¿�ʼ�ùؿ�
	public void OnRestartClick()
	{
		GameModel gModel = GetModel<GameModel>();
		SendEvent(Consts.E_StartLevel, new StartLevelArgs() { LevelIndex = gModel.PlayLevelID });
	}

	// ����������һ��
	public void OnContinueClick()
	{
		GameModel gModel = GetModel<GameModel>();
		if(gModel.PlayLevelID >= gModel.LevelCount - 1) {
			// ��Ϸͨ��
			Game.GetInstance().LoadScene((int)SceneID.Complete);
			return;
		}

		// ��ʼ��һ��
		SendEvent(Consts.E_StartLevel, new StartLevelArgs() { LevelIndex = gModel.PlayLevelID + 1 });
	}
	#endregion

	#region ��������
	#endregion
}
