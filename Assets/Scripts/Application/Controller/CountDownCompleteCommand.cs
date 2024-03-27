using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownCompleteCommand : Controller
{
	public override void Execute(object data)
	{
		// 回合开始
		GameModel gModel = GetModel<GameModel>();
		gModel.IsPlaying = true;

		// 开始出怪
		RoundModel rModel = GetModel<RoundModel>();
		rModel.StartRound();
	}
}
