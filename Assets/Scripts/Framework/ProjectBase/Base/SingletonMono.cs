using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例模块
/// 2.继承MonoBehaviour的单例基类 （不推荐）
/// </summary>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
		// 继承了MonoBehaviour的脚本 不能直接new 
		// 只能通过拖动到对象上 或者 通过AddComponent加脚本 
		// Unity内部会自动实例化
		return instance;
    }

	protected virtual void Awake()
	{
		instance = this as T;
	}
}


/* *
===========================================================================
注：继承了MonoBehaviour的单例对象，不建议重复挂载到多个物体上
	挂载到多个物体上时，会破坏单例性，instance只会指向最后一个创建出来的对象

使用案例
1.创建单例类时，直接继承SingletonMono<T> ，就能继承SingletonMono中的方法
public class GameManager : SingletonMono<GameManager>
{
	2.子类想修改Awake方法的话，需要重写Awake，并调用一次父类中的Awake方法
	protected override void Awake()
	{
		base.Awake();
	}
}

3.使用时直接调用GetInstance ，返回的就是静态类
public class Test
{
    void Main()
    {
        GameManager.GetInstance();
    }
}

===========================================================================
 * */