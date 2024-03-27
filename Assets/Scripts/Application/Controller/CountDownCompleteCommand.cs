using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownCompleteCommand : Controller
{
	public override void Execute(object data)
	{
		// �غϿ�ʼ
		GameModel gModel = GetModel<GameModel>();
		gModel.IsPlaying = true;

		// ��ʼ����
		RoundModel rModel = GetModel<RoundModel>();
		rModel.StartRound();
	}
}
