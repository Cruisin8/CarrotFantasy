using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelCommand : Controller
{
	public override void Execute(object data)
	{
		StartLevelArgs e = data as StartLevelArgs;

		// 1.创建游戏数据
		GameModel gModel = GetModel<GameModel>();
		gModel.StartLevel(e.LevelIndex);

		// 2.创建关卡数据
		RoundModel rModel = GetModel<RoundModel>();
		rModel.LoadLevel(gModel.PlayLevel);

		// 进入游戏关卡
		Game.GetInstance().LoadScene((int)SceneID.Level);
	}
}
