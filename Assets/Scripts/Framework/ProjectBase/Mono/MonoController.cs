using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ����Monoģ��
/// </summary>
public class MonoController : MonoBehaviour
{
	public event UnityAction updateEvent;

	private void Start()
	{
		DontDestroyOnLoad(this.gameObject);

	}

	private void Update()
	{
		if (updateEvent != null) {
			updateEvent();
		}
	}

	// ���֡�����¼�
	public void AddUpdateListener(UnityAction fun)
	{
		updateEvent += fun;
	}

	// �Ƴ�֡�����¼�
	public void RemoveUpdateListener(UnityAction fun)
	{
		updateEvent -= fun;
	}
}
