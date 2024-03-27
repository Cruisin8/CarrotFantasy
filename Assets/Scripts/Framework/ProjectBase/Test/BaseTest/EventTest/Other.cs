using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{
	private void Start()
	{
		// ���������¼�
		EventCenter.GetInstance().AddEventListener<TestMonster>("MonsterDead", OtherWaitMonsterDeadDo);

		EventCenter.GetInstance().AddEventListener("Win", Win);
	}

	private void OnDestroy()
	{
		// ����ʱ�Ƴ������¼�
		EventCenter.GetInstance().RemoveEventListener<TestMonster>("MonsterDead", OtherWaitMonsterDeadDo);

		EventCenter.GetInstance().RemoveEventListener("Win", Win);

		//EventCenter.GetInstance().EventTrigger<TestMonster>("MonsterDead", �������);
		EventCenter.GetInstance().EventTrigger("Win");
	}

	public void OtherWaitMonsterDeadDo(TestMonster info)
	{
		Debug.Log("���������������¼�������ɾͼ�¼�������������������...");
	}

	public void Win()
	{
		Debug.Log("ʤ��...�����ڲ��Բ���Ҫ���ݲ����ģ�");
	}
}
