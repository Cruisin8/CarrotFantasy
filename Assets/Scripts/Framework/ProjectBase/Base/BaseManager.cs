using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 单例模块 
/// 1.不继承Mono的单例基类（推荐）
/// </summary>
public class BaseManager<T> where T: new()
{
    private static T instance;

    public static T GetInstance()
    {
        if(instance == null) {
            instance = new T();
        }

        return instance;
    }
}


/* *
===========================================================================

使用案例
1.创建单例类时，直接继承BaseManager<T> ，就能继承BaseManager中的方法
public class GameManager : BaseManager<GameManager>
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
