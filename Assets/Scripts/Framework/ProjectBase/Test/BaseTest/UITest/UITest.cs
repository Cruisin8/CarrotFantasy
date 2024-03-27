using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
	private void Start()
	{
		// �첽���ش���panelִ��InitInfo��������ûص����������ܱ�֤������ɺ����
		UIMgr.GetInstance().ShowPanel<LoginPanel>("LoginPanel", E_UI_Layer.MID, ShowPanelOver);

		// ͬ���������������һ��panel��ִ��InitInfo
		//LoginPanel p = this.gameObject.GetComponent<LoginPanel>();
		//p.InitInfo();
	}

	private void ShowPanelOver(LoginPanel panel)
	{
		panel.InitInfo();
		// �ӳ�3sִ��DelayHide����
		Invoke("DelayHide", 3);
	}

	private void DelayHide()
	{
		UIMgr.GetInstance().HidePanel("LoginPanel");
	}
}
