using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller
{
	// ��ȡģ��
	protected T GetModel<T>() where T : Model
	{
		return MVC.GetModel<T>() as T;
	}

	// ��ȡ��ͼ
	protected T GetView<T>() where T : View
	{
		return MVC.GetView<T>() as T;
	}

	// ע��ģ��
	protected void RegisterModel(Model model)
	{
		MVC.RegisterModel(model);
	}

	// ע����ͼ
	protected void RegisterView(View view)
	{
		MVC.RegisterView(view);
	}

	// ע�������
	protected void RegisterController(string eventName, Type controllerType)
	{
		MVC.RegisterController(eventName, controllerType);
	}

	// ����ϵͳ��Ϣ
	public abstract void Execute(object data);
}
