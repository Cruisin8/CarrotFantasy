using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 缓存池中一类相同的对象数据
/// </summary>
public class PoolData
{
    // 缓存池中,相同的对象挂载的父节点
    public GameObject fatherObj;
    // 缓存池中，存放此类相同的对象的容器
    public List<GameObject> poolList;

	// 构造函数： 参数obj为存入缓存池的对象  参数poolObj为缓存池
	public PoolData(GameObject obj, GameObject poolObj)
    {
		// 创建一个父对象，作为根节点Pool的子物体，归纳对象池中相同的对象
		fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>();
        PushObj(obj);
	}

	// 从缓存池中，此类对象的容器中，取出对象
	public GameObject GetObj()
	{
		GameObject obj = null;
		// 获取第一个对象
		obj = poolList[0];
		// 从容器中移除已取出的对象
		poolList.RemoveAt(0);
		// 激活对象
		obj.SetActive(true);
		// 发送事件
		obj.SendMessage("OnGetObj", SendMessageOptions.DontRequireReceiver);
		// 断开父节点
		obj.transform.parent = null;

		return obj;
	}

	// 向缓存池中，此类对象的容器中，存入对象
	public void PushObj(GameObject obj)
    {
		// 发送事件
		obj.SendMessage("OnPushObj", SendMessageOptions.DontRequireReceiver);
		// 失活对象
		obj.SetActive(false);
        // 存入容器
		poolList.Add(obj);
        // 设置父对象
        obj.transform.parent = fatherObj.transform;
	}
}

/// <summary>
/// 缓存池模块
/// </summary>
public class PoolMgr : BaseManager<PoolMgr> 
{
    // 缓存池容器
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

	// 缓存池父节点Pool，用于收纳创建的对象
	private GameObject poolObj;

	// 从缓存池中获取  参数name为资源路径
	public void GetObj(string name, UnityAction<GameObject> callback)
	{
		if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0) {
			// 缓存池中有，则获取缓存池中该类的第一个对象
			callback(poolDic[name].GetObj());
		}
		else {
			// 缓存池中没有，则从资源路径加载
			// 异步加载资源（不推荐同步加载，因为当加载的资源较大时可能会卡顿）
			ResMgr.GetInstance().LoadAsync<GameObject>(name, (obj) => {
				// 将对象名称设置为缓存池中的名称（资源路径）
				obj.name = name;
				// 发送事件
				obj.SendMessage("OnGetObj", SendMessageOptions.DontRequireReceiver);
				// 加载完成后，执行callback函数
				callback(obj);
			});
		}
	}

	public GameObject GetObj(string name)
	{
		GameObject go = null;

		if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0) {
			// 缓存池中有，则获取缓存池中该类的第一个对象
			go = poolDic[name].GetObj();
		}
		else {
			// 缓存池中没有，则从资源路径加载
			// 异步加载资源（不推荐同步加载，因为当加载的资源较大时可能会卡顿）
			ResMgr.GetInstance().LoadAsync<GameObject>(name, (obj) => {
				// 将对象名称设置为缓存池中的名称（资源路径）
				obj.name = name;
				// 发送事件
				obj.SendMessage("OnGetObj", SendMessageOptions.DontRequireReceiver);
				// 加载完成后，执行callback函数
				go = obj;
			});
		}

		return go;
	}

	// 放回缓存池    参数name一般为资源路径
	public void PushObj(GameObject obj)
    {
        // 创建空物体Pool作为缓存池父节点，收纳创建的对象
        if (poolObj == null) {
			poolObj = new GameObject("Pool");
		}

        if (poolDic.ContainsKey(obj.name)){
            // 缓存池中有，则添加对象
            poolDic[obj.name].PushObj(obj);
        }
        else {
            // 缓存池中没有，则创建新的对象类型，并存入对象
            poolDic.Add(obj.name, new PoolData(obj, poolObj));
			// 发送事件
			obj.SendMessage("OnPushObj", SendMessageOptions.DontRequireReceiver);
		}
    }

    // 清空缓存池    主要用在场景切换时
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
