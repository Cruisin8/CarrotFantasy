using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
	private void Start()
	{
		// 任务监听事件
		EventCenter.GetInstance().AddEventListener<TestMonster>("MonsterDead", TaskWaitMonsterDeadDo);
	}

	private void OnDestroy()
	{
		// 销毁时移除监听事件
		EventCenter.GetInstance().RemoveEventListener<TestMonster>("MonsterDead", TaskWaitMonsterDeadDo);
	}

	public void TaskWaitMonsterDeadDo(TestMonster info)
	{
		Debug.Log("怪兽死亡后，任务记录...");
	}
}
