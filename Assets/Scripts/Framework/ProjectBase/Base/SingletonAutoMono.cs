using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ģ��
/// 3.�̳�MonoBehaviour���Զ����������� ���Ƽ���
/// </summary>
public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;

	public static T GetInstance()
	{
		if (instance == null) {
			// �Զ�����һ���ն��󣬲���������������Ϊ�ű�����Ȼ����ص����ű�
			GameObject obj = new GameObject();
			obj.name = typeof(T).ToString();

			// ��ֹ�л�����ʱ�����������Ƴ�
			DontDestroyOnLoad(obj);

			instance = obj.AddComponent<T>();
		}

		return instance;
	}
}


/* *
===========================================================================
ע���������GetInstanceʱ�����Զ��ڳ����д���һ�����غ��˽ű��Ŀ����塣
	���ڵ�һ�ε��øö����GetInstance֮ǰ������������Ѵ��ڸö�����Ӧ��ʹ��SingletonMono��̳У������ʹ���󱻳�ʼ�����Σ�
	�л�Scene����ʱ������������ᱻɾ������Ҫ���� DontDestroyOnLoad ������

ʹ�ð���
1.����������ʱ��ֱ�Ӽ̳�SingletonAutoMono<T> �����ܼ̳�SingletonAutoMono�еķ���
public class GameManager : SingletonAutoMono<GameManager>
{
	
}

2.ʹ��ʱֱ�ӵ���GetInstance �����صľ��Ǿ�̬��
public class Test
{
    void Main()
    {
        GameManager.GetInstance();
    }
}

===========================================================================
 * */