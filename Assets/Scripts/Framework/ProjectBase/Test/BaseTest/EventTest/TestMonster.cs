using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ڲ����¼����ģ�
/// ��������TestMonster����һ�ȡ����Player�������¼Task������Other
/// </summary>
public class TestMonster : MonoBehaviour
{
    // ��������
    public int monsterType = 1;
    // ��������
    public string monsterName = "ʷ��ķ";

	private void Start()
	{
        // �����¼�����
        Dead();	
	}

    // ��������
	void Dead()
    {
        Debug.Log("��������");
        // ���������ڹ��������������£���ʹ���¼����ģ���
        // 1.��� ��ý���
        //GameObject.Find("Player").GetComponent<Player>().PlayerWaitMonsterDeadDo();
        // 2.�����¼
        //GameObject.Find("Task").GetComponent<Task>().TaskWaitMonsterDeadDo();
        // 3.�����¼�������ɾͼ�¼�������������������
        //GameObject.Find("Other").GetComponent<Other>().OtherWaitMonsterDeadDo();

        // ʹ���¼����ģ�
        // �����¼�
        EventCenter.GetInstance().EventTrigger("MonsterDead", this);

    }
}
