using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 资源加载模块
/// 1.同步加载
/// 2.异步加载
/// </summary>
public class ResMgr : BaseManager<ResMgr> 
{
    // 同步加载资源
    public T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);

        if(res is GameObject) {
			// 如果加载对象是GameObject类型，则实例化后返回，外部直接使用即可
			return GameObject.Instantiate(res);
        }

        // 如果不是GameObject类型，不需要实例化，直接返回出去，例如TextAsset AudioClip
        return res;
    }

    // 异步加载资源
    public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        // 开启异步加载的协程
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadAsync(name, callback));
    }

    // 真正的异步加载协程函数，用于开启异步加载对应的资源
    // callback的返回值只有一个，即加载出来的资源r，如果需要返回多个值，可以自定义数据结构返回
    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;

        if(r.asset is GameObject) {
            callback(GameObject.Instantiate(r.asset) as T);
        } else {
            callback(r.asset as T);
        }
    }
}
