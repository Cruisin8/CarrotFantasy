using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpCommand : Controller
{
	public override void Execute(object data)
	{
		// 注册模型(Model)
		RegisterModel(new GameModel());
		RegisterModel(new RoundModel());

		// 注册命令(Controlller)
		RegisterController(Consts.E_EnterScene, typeof(EnterSceneCommand));
		RegisterController(Consts.E_ExitScene, typeof(ExitSceneCommand));
		RegisterController(Consts.E_StartLevel, typeof(StartLevelCommand));
		RegisterController(Consts.E_EndLevel, typeof(EndLevelCommand));
		RegisterController(Consts.E_CountDownComplete, typeof(CountDownCompleteCommand));

		RegisterController(Consts.E_UpgradeTower, typeof(UpgradeTowerCommand));
		RegisterController(Consts.E_SellTower, typeof(SellTowerCommand));

		// 初始化
		GameModel gModel = GetModel<GameModel>();
		gModel.Initialize();

		// 进入开始界面
		Game.GetInstance().LoadScene((int)SceneID.Start);
	}
}
