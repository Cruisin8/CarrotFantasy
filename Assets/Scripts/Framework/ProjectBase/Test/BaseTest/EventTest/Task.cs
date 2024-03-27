using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
	private void Start()
	{
		// ��������¼�
		EventCenter.GetInstance().AddEventListener<TestMonster>("MonsterDead", TaskWaitMonsterDeadDo);
	}

	private void OnDestroy()
	{
		// ����ʱ�Ƴ������¼�
		EventCenter.GetInstance().RemoveEventListener<TestMonster>("MonsterDead", TaskWaitMonsterDeadDo);
	}

	public void TaskWaitMonsterDeadDo(TestMonster info)
	{
		Debug.Log("���������������¼...");
	}
}
