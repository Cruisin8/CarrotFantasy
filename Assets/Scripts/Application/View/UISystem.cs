using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : View
{
	#region ����
	#endregion

	#region �¼�
	#endregion

	#region �ֶ�
	public Button btnResume;
	public Button btnRestart;
	public Button btnSelect;

	#endregion

	#region ����
	public override string Name {
		get { return Consts.V_System; }
	}
	#endregion

	#region ����
	public void Show()
	{
		this.gameObject.SetActive(true);
	}

	public void Hide()
	{
		this.gameObject.SetActive(false);
	}

	#endregion

	#region Unity�ص�
	#endregion

	#region �¼��ص�
	public override void HandleEvent(string eventName, object data)
	{

	}

	public void OnResumeClick()
	{

	}

	public void OnRestartClick()
	{

	}

	public void OnSelectClick()
	{

	}
	#endregion

	#region ��������
	#endregion
}
