using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ģ��
/// 2.�̳�MonoBehaviour�ĵ������� �����Ƽ���
/// </summary>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
		// �̳���MonoBehaviour�Ľű� ����ֱ��new 
		// ֻ��ͨ���϶��������� ���� ͨ��AddComponent�ӽű� 
		// Unity�ڲ����Զ�ʵ����
		return instance;
    }

	protected virtual void Awake()
	{
		instance = this as T;
	}
}


/* *
===========================================================================
ע���̳���MonoBehaviour�ĵ������󣬲������ظ����ص����������
	���ص����������ʱ�����ƻ������ԣ�instanceֻ��ָ�����һ�����������Ķ���

ʹ�ð���
1.����������ʱ��ֱ�Ӽ̳�SingletonMono<T> �����ܼ̳�SingletonMono�еķ���
public class GameManager : SingletonMono<GameManager>
{
	2.�������޸�Awake�����Ļ�����Ҫ��дAwake��������һ�θ����е�Awake����
	protected override void Awake()
	{
		base.Awake();
	}
}

3.ʹ��ʱֱ�ӵ���GetInstance �����صľ��Ǿ�̬��
public class Test
{
    void Main()
    {
        GameManager.GetInstance();
    }
}

===========================================================================
 * */