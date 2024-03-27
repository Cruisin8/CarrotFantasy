using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ApplicationBase<T> : SingletonMono<T> where T : MonoBehaviour
{
	// ע�������
	protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    // �����¼�
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
