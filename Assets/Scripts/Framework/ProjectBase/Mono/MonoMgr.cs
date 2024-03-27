using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono����������װ��MonoBehaviour��
/// 1.�ṩ���ⲿ���֡�����¼��ķ���
/// 2.�ṩ���ⲿ���Э�̵ķ���
/// </summary>
public class MonoMgr : BaseManager<MonoMgr> 
{
    private MonoController controller;

    public MonoMgr()
    {
		// ͨ��BaseManager������������֤��MonoController��Ψһ��
		GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }

	// ���֡�����¼�
	public void AddUpdateListener(UnityAction fun)
	{
		controller.AddUpdateListener(fun);
	}

	// �Ƴ�֡�����¼�
	public void RemoveUpdateListener(UnityAction fun)
	{
		controller.RemoveUpdateListener(fun);
	}

	// ��װЭ�̺�������Unity�Դ���Э�̺���һ�£�
	public Coroutine StartCoroutine(IEnumerator routine)
	{
		return controller.StartCoroutine(routine);
	}

	public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
	{
		return controller.StartCoroutine(methodName, value);
	}

	public Coroutine StartCoroutine(string methodName)
	{
		return controller.StartCoroutine(methodName);
	}
}
