using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ڲ���PoolMgr �ӻ�����л�ȡ�Ĺ���
/// </summary>
public class PoolGetTest : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			// ���������ӻ�����л�ȡһ��Cube
			PoolMgr.GetInstance().GetObj("Test/BaseTest/BaseTestPrefabs/TestPrefabs/Cube", (obj) => {
				Debug.Log("�첽����Cube���");
			});
		}
		else if(Input.GetMouseButtonDown(1)) {
			// ����Ҽ����ӻ�����л�ȡһ��Sphere
			PoolMgr.GetInstance().GetObj("Test/BaseTest/BaseTestPrefabs/TestPrefabs/Sphere", (obj) => {
				Debug.Log("�첽����Sphere���");
			});
		}
	}
}
