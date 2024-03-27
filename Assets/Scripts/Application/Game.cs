using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : ApplicationBase<Game>
{
    // 全局方法
    public void LoadScene(int level)
    {
        // 退出旧场景
        // 事件参数
        SceneArgs e = new SceneArgs();
        e.SceneIndex = SceneManager.GetActiveScene().buildIndex;

		// 发布事件
		SendEvent(Consts.E_ExitScene, e);

		// 加载新场景
		ScenesMgr.GetInstance().LoadScene(level, LoadSceneMode.Single);

	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("OnSceneLoaded: " + scene.name + " (Build Index: " + scene.buildIndex + ")");

		// 事件参数
		SceneArgs e = new SceneArgs();
		e.SceneIndex = scene.buildIndex;

		// 发布事件
		SendEvent(Consts.E_EnterScene, e);
	}

    // 游戏入口
    void Start()
    {
		//确保Game对象一直存在
		Object.DontDestroyOnLoad(this.gameObject);

		// 添加场景加载完成事件的监听
		SceneManager.sceneLoaded += OnSceneLoaded;

		// 全局单例赋值
		StaticData.GetInstance();

		// 注册启动命令
		RegisterController(Consts.E_StartUp, typeof(StartUpCommand));

		// 启动游戏
		SendEvent(Consts.E_StartUp);
    }
}
