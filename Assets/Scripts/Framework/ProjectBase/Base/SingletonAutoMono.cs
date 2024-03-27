using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例模块
/// 3.继承MonoBehaviour的自动化单例基类 （推荐）
/// </summary>
public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;

	public static T GetInstance()
	{
		if (instance == null) {
			// 自动创建一个空对象，并将它的名字设置为脚本名，然后挂载单例脚本
			GameObject obj = new GameObject();
			obj.name = typeof(T).ToString();

			// 防止切换场景时，单例对象被移除
			DontDestroyOnLoad(obj);

			instance = obj.AddComponent<T>();
		}

		return instance;
	}
}


/* *
===========================================================================
注：子类调用GetInstance时，会自动在场景中创建一个挂载好了脚本的空物体。
	（在第一次调用该对象的GetInstance之前，如果场景中已存在该对象，则应该使用SingletonMono类继承，否则会使对象被初始化两次）
	切换Scene场景时，单例空物体会被删除，需要加上 DontDestroyOnLoad 方法。

使用案例
1.创建单例类时，直接继承SingletonAutoMono<T> ，就能继承SingletonAutoMono中的方法
public class GameManager : SingletonAutoMono<GameManager>
{
	
}

2.使用时直接调用GetInstance ，返回的就是静态类
public class Test
{
    void Main()
    {
        GameManager.GetInstance();
    }
}

===========================================================================
 * */