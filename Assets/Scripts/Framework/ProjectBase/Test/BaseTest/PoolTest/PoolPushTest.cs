using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ڲ���PoolMgr �Żػ���صĹ���
/// </summary>
public class PoolPushTest : MonoBehaviour
{
	private void OnEnable()
	{
		// �ӳ�1s���Զ�������Żػ����
		Invoke("Push", 1);
	}

	void Push()
	{
		PoolMgr.GetInstance().PushObj(this.gameObject);
	}
}
