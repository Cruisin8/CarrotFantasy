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

		// Í£Ö¹³ö¹Ö
		rModel.StopRound();

		// Í£Ö¹ÓÎÏ·
		gModel.StopLevel(e.IsWin);

		// µ¯³öUI
		if (e.IsWin) {
			GetView<UIWin>().Show();
		}
		else {
			GetView<UILose>().Show();
		}
	}
}
