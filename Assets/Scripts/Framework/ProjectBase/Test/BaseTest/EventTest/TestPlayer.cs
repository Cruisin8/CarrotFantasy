using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
	private void Start()
	{
		// ��Ҽ����¼�
		EventCenter.GetInstance().AddEventListener<TestMonster>("MonsterDead", PlayerWaitMonsterDeadDo);
	}

	private void OnDestroy()
	{
		// ����ʱ�Ƴ������¼�
		EventCenter.GetInstance().RemoveEventListener<TestMonster>("MonsterDead", PlayerWaitMonsterDeadDo);
	}

	public void PlayerWaitMonsterDeadDo(TestMonster info)
    {
        Debug.Log("����" + (info as TestMonster).monsterName + "��������ҵõ�����...");
    }

}
