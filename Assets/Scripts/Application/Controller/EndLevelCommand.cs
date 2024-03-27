using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelCommand : Controller
{
	public override void Execute(object data)
	{
		EndLevelArgs e = (EndLevelArgs)data;
		GameModel gModel = GetModel<GameModel>();
		RoundModel rModel = GetModel<RoundModel>();

		// ֹͣ����
		rModel.StopRound();

		// ֹͣ��Ϸ
		gModel.StopLevel(e.IsWin);

		// ����UI
		if (e.IsWin) {
			GetView<UIWin>().Show();
		}
		else {
			GetView<UILose>().Show();
		}
	}
}
