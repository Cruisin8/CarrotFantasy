using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ڲ���ResMgr ��Դ���ع���
/// </summary>
public class ResTest : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			// ����������res��Դͬ������һ��ResCube
			GameObject obj = ResMgr.GetInstance().Load<GameObject>("Test/BaseTest/BaseTestPrefabs/TestPrefabs/ResCube");
			// ���Ի�ȡ���ķ���ֵ
			Debug.Log("ͬ��������Դ��"+obj.name);
		}
		else if(Input.GetMouseButtonDown(1)) {
			// ����Ҽ�����res��Դ�첽����һ��ResSphere������Դ�������س�������ִ��xxx���õ��ú����ķ�ʽд
			ResMgr.GetInstance().LoadAsync<GameObject>("Test/BaseTest/BaseTestPrefabs/TestPrefabs/ResSphere", DoSomething);

			// ����Ҽ�����res��Դ�첽����һ��ResSphere������Դ�������س�������ִ��xxx����lamda���ʽд��Ҫִ�еĺ���
			ResMgr.GetInstance().LoadAsync<GameObject>("Test/BaseTest/BaseTestPrefabs/TestPrefabs/ResSphere", (obj) => {
				Debug.Log("�첽������Դ��" + obj.name + "��ִ��lamda���ʽ��ʽ��xxx����");
			});
		}
	}

	// ִ��xxx����
	private void DoSomething(GameObject obj)
	{
		// �첽���أ�����Դ�������س�������ִ��xxx.
		Debug.Log("�첽������Դ��" + obj.name + "��ִ��xxx����");
	}
}
