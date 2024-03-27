using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
	private void Start()
	{
		// 玩家监听事件
		EventCenter.GetInstance().AddEventListener<TestMonster>("MonsterDead", PlayerWaitMonsterDeadDo);
	}

	private void OnDestroy()
	{
		// 销毁时移除监听事件
		EventCenter.GetInstance().RemoveEventListener<TestMonster>("MonsterDead", PlayerWaitMonsterDeadDo);
	}

	public void PlayerWaitMonsterDeadDo(TestMonster info)
    {
        Debug.Log("怪兽" + (info as TestMonster).monsterName + "死亡后，玩家得到奖励...");
    }

}
