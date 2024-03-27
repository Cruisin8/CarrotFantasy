using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelCommand : Controller
{
	public override void Execute(object data)
	{
		StartLevelArgs e = data as StartLevelArgs;

		// 1.������Ϸ����
		GameModel gModel = GetModel<GameModel>();
		gModel.StartLevel(e.LevelIndex);

		// 2.�����ؿ�����
		RoundModel rModel = GetModel<RoundModel>();
		rModel.LoadLevel(gModel.PlayLevel);

		// ������Ϸ�ؿ�
		Game.GetInstance().LoadScene((int)SceneID.Level);
	}
}
