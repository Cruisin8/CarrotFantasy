using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model
{
    // 模型标识
    public abstract string Name { get; }  

    // 发送事件
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
