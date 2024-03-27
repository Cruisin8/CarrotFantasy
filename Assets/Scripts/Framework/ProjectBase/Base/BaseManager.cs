using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����ģ�� 
/// 1.���̳�Mono�ĵ������ࣨ�Ƽ���
/// </summary>
public class BaseManager<T> where T: new()
{
    private static T instance;

    public static T GetInstance()
    {
        if(instance == null) {
            instance = new T();
        }

        return instance;
    }
}


/* *
===========================================================================

ʹ�ð���
1.����������ʱ��ֱ�Ӽ̳�BaseManager<T> �����ܼ̳�BaseManager�еķ���
public class GameManager : BaseManager<GameManager>
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
