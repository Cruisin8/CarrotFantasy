using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �������һ����ͬ�Ķ�������
/// </summary>
public class PoolData
{
    // �������,��ͬ�Ķ�����صĸ��ڵ�
    public GameObject fatherObj;
    // ������У���Ŵ�����ͬ�Ķ��������
    public List<GameObject> poolList;

	// ���캯���� ����objΪ���뻺��صĶ���  ����poolObjΪ�����
	public PoolData(GameObject obj, GameObject poolObj)
    {
		// ����һ����������Ϊ���ڵ�Pool�������壬���ɶ��������ͬ�Ķ���
		fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>();
        PushObj(obj);
	}

	// �ӻ�����У��������������У�ȡ������
	public GameObject GetObj()
	{
		GameObject obj = null;
		// ��ȡ��һ������
		obj = poolList[0];
		// ���������Ƴ���ȡ���Ķ���
		poolList.RemoveAt(0);
		// �������
		obj.SetActive(true);
		// �����¼�
		obj.SendMessage("OnGetObj", SendMessageOptions.DontRequireReceiver);
		// �Ͽ����ڵ�
		obj.transform.parent = null;

		return obj;
	}

	// �򻺴���У��������������У��������
	public void PushObj(GameObject obj)
    {
		// �����¼�
		obj.SendMessage("OnPushObj", SendMessageOptions.DontRequireReceiver);
		// ʧ�����
		obj.SetActive(false);
        // ��������
		poolList.Add(obj);
        // ���ø�����
        obj.transform.parent = fatherObj.transform;
	}
}

/// <summary>
/// �����ģ��
/// </summary>
public class PoolMgr : BaseManager<PoolMgr> 
{
    // ���������
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

	// ����ظ��ڵ�Pool���������ɴ����Ķ���
	private GameObject poolObj;

	// �ӻ�����л�ȡ  ����nameΪ��Դ·��
	public void GetObj(string name, UnityAction<GameObject> callback)
	{
		if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0) {
			// ��������У����ȡ������и���ĵ�һ������
			callback(poolDic[name].GetObj());
		}
		else {
			// �������û�У������Դ·������
			// �첽������Դ�����Ƽ�ͬ�����أ���Ϊ�����ص���Դ�ϴ�ʱ���ܻῨ�٣�
			ResMgr.GetInstance().LoadAsync<GameObject>(name, (obj) => {
				// ��������������Ϊ������е����ƣ���Դ·����
				obj.name = name;
				// �����¼�
				obj.SendMessage("OnGetObj", SendMessageOptions.DontRequireReceiver);
				// ������ɺ�ִ��callback����
				callback(obj);
			});
		}
	}

	public GameObject GetObj(string name)
	{
		GameObject go = null;

		if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0) {
			// ��������У����ȡ������и���ĵ�һ������
			go = poolDic[name].GetObj();
		}
		else {
			// �������û�У������Դ·������
			// �첽������Դ�����Ƽ�ͬ�����أ���Ϊ�����ص���Դ�ϴ�ʱ���ܻῨ�٣�
			ResMgr.GetInstance().LoadAsync<GameObject>(name, (obj) => {
				// ��������������Ϊ������е����ƣ���Դ·����
				obj.name = name;
				// �����¼�
				obj.SendMessage("OnGetObj", SendMessageOptions.DontRequireReceiver);
				// ������ɺ�ִ��callback����
				go = obj;
			});
		}

		return go;
	}

	// �Żػ����    ����nameһ��Ϊ��Դ·��
	public void PushObj(GameObject obj)
    {
        // ����������Pool��Ϊ����ظ��ڵ㣬���ɴ����Ķ���
        if (poolObj == null) {
			poolObj = new GameObject("Pool");
		}

        if (poolDic.ContainsKey(obj.name)){
            // ��������У�����Ӷ���
            poolDic[obj.name].PushObj(obj);
        }
        else {
            // �������û�У��򴴽��µĶ������ͣ����������
            poolDic.Add(obj.name, new PoolData(obj, poolObj));
			// �����¼�
			obj.SendMessage("OnPushObj", SendMessageOptions.DontRequireReceiver);
		}
    }

    // ��ջ����    ��Ҫ���ڳ����л�ʱ
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
