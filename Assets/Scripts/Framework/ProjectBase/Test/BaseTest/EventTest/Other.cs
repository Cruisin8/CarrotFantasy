using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{
	private void Start()
	{
		// 其他监听事件
		EventCenter.GetInstance().AddEventListener<TestMonster>("MonsterDead", OtherWaitMonsterDeadDo);

		EventCenter.GetInstance().AddEventListener("Win", Win);
	}

	private void OnDestroy()
	{
		// 销毁时移除监听事件
		EventCenter.GetInstance().RemoveEventListener<TestMonster>("MonsterDead", OtherWaitMonsterDeadDo);

		EventCenter.GetInstance().RemoveEventListener("Win", Win);

		//EventCenter.GetInstance().EventTrigger<TestMonster>("MonsterDead", 怪物参数);
		EventCenter.GetInstance().EventTrigger("Win");
	}

	public void OtherWaitMonsterDeadDo(TestMonster info)
	{
		Debug.Log("怪兽死亡后，其他事件，比如成就记录、副本继续创建怪物等...");
	}

	public void Win()
	{
		Debug.Log("胜利...（用于测试不需要传递参数的）");
	}
}
