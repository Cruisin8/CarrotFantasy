using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIComplete : View
{
	#region ����
	#endregion

	#region �¼�
	#endregion

	#region �ֶ�
	public Button btnSelect;
	public Button btnClear;
	#endregion

	#region ����
	public override string Name {
		get { return Consts.V_Complete; }
	}
	#endregion

	#region ����
	#endregion

	#region Unity�ص�
	#endregion

	#region �¼��ص�
	public override void RegisterEvents()
	{

	}

	public override void HandleEvent(string eventName, object data)
	{
		
	}

	// ѡ��ؿ���ť
	public void OnSelectClick()
	{
		// �ص���ʼ����
		Game.GetInstance().LoadScene((int)SceneID.Start);
	}

	// �嵵��ť
	public void OnClearClick()
	{
		GameModel gModel = GetModel<GameModel>();
		gModel.ClearProgress();
	}
	#endregion

	#region ��������
	#endregion


}
