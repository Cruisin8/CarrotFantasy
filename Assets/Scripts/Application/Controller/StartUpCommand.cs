using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpCommand : Controller
{
	public override void Execute(object data)
	{
		// ע��ģ��(Model)
		RegisterModel(new GameModel());
		RegisterModel(new RoundModel());

		// ע������(Controlller)
		RegisterController(Consts.E_EnterScene, typeof(EnterSceneCommand));
		RegisterController(Consts.E_ExitScene, typeof(ExitSceneCommand));
		RegisterController(Consts.E_StartLevel, typeof(StartLevelCommand));
		RegisterController(Consts.E_EndLevel, typeof(EndLevelCommand));
		RegisterController(Consts.E_CountDownComplete, typeof(CountDownCompleteCommand));

		RegisterController(Consts.E_UpgradeTower, typeof(UpgradeTowerCommand));
		RegisterController(Consts.E_SellTower, typeof(SellTowerCommand));

		// ��ʼ��
		GameModel gModel = GetModel<GameModel>();
		gModel.Initialize();

		// ���뿪ʼ����
		Game.GetInstance().LoadScene((int)SceneID.Start);
	}
}
