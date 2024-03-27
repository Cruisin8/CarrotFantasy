using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
	private void Start()
	{
		// 异步加载创建panel执行InitInfo，必须调用回调函数，才能保证创建完成后调用
		UIMgr.GetInstance().ShowPanel<LoginPanel>("LoginPanel", E_UI_Layer.MID, ShowPanelOver);

		// 同步加载情况，创建一个panel后执行InitInfo
		//LoginPanel p = this.gameObject.GetComponent<LoginPanel>();
		//p.InitInfo();
	}

	private void ShowPanelOver(LoginPanel panel)
	{
		panel.InitInfo();
		// 延迟3s执行DelayHide函数
		Invoke("DelayHide", 3);
	}

	private void DelayHide()
	{
		UIMgr.GetInstance().HidePanel("LoginPanel");
	}
}
