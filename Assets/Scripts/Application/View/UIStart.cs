using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : View
{
	#region ����
	#endregion

	#region �¼�
	#endregion

	#region �ֶ�
	#endregion

	#region ����
	public override string Name {
		get { return Consts.V_Start; }
	}
	#endregion

	#region ����
	// ��ת�ؿ�ѡ�񳡾� btnAdventure
	public void GotoSelect()
	{
		Game.GetInstance().LoadScene((int)SceneID.Select);

		// �����л�ʱ���ٿ�ʼ�����ѭ������
		DOTween.KillAll();
	}
	#endregion

	#region Unity�ص�
	#endregion

	#region �¼��ص�
	public override void HandleEvent(string eventName, object data)
	{
		throw new System.NotImplementedException();
	}
	#endregion

	#region ��������
	#endregion
}
