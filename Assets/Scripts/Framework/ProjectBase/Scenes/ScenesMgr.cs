using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换模块
/// </summary>
public class ScenesMgr : BaseManager<ScenesMgr> 
{
	// 同步加载场景(使用场景名加载)
	public void LoadScene(string sceneName, UnityAction fun)
    {
        // 场景同步加载
        SceneManager.LoadScene(sceneName);
        // 场景加载完成之后，才会执行fun
        fun();
    }

	// 同步加载场景(使用场景号加载)
	// LoadSceneMode.Single 使用场景号加载单一场景（会关闭其他场景，只加载该场景）
	// LoadSceneMode.Additive 使用场景号加载附加场景（会将场景添加到当前加载的场景中）
	public void LoadScene(int sceneBuildIndex, LoadSceneMode mode)
	{
		// 场景同步加载
		SceneManager.LoadScene(sceneBuildIndex, mode);
	}

	// 异步加载场景 提供给外部的接口
	public void LoadSceneAsyn(string sceneName, UnityAction fun)
    {
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadSceneAsyn(sceneName, fun));
    }

    // 利用协程 异步加载场景
    private IEnumerator ReallyLoadSceneAsyn(string sceneName, UnityAction fun)
	{
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);

        // 获取场景加载进度
        while(!ao.isDone) {
            // 案例：
            // 可以利用事件中心向外分发进度情况，外面根据情况使用
            EventCenter.GetInstance().EventTrigger("进度条更新的函数", ao.progress);
            // 可以在这里更新进度条
            yield return ao.progress;
        }

        yield return ao;
        // 场景加载完成之后，才会执行fun
        fun();
    }
}
