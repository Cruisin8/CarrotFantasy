using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountDown : View
{
	#region ����
	#endregion

	#region �¼�
	#endregion

	#region �ֶ�
	public Image Count;
	public Sprite[] Numbers;
	#endregion

	#region ����
	public override string Name {
		get { return Consts.V_CountDown; }
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

	public void StartCountDown()
	{
		Show();
		StartCoroutine("DisplayCount");
	}

	// ����ʱ����
	IEnumerator DisplayCount()
	{
		int count = 3;
		while (count > 0) {
			// ��ʾ
			Count.sprite = Numbers[count - 1];

			count--;

			yield return new WaitForSeconds(1f);

			if(count <= 0) {
				break;
			}
		}

		// ���ص���ʱ����
		Hide();

		// ����ʱ����
		SendEvent(Consts.E_CountDownComplete);

	}
	#endregion

	#region Unity�ص�

	#endregion

	#region �¼��ص�
	public override void RegisterEvents()
	{
		// ��עE_EnterScene���볡���¼�
		this.AttentionEvents.Add(Consts.E_EnterScene);
	}

	public override void HandleEvent(string eventName, object data)
	{
		// ���볡��3ʱ��������ʱ
		switch (eventName) {
			case Consts.E_EnterScene:
				SceneArgs e = (SceneArgs)data;
				if (e.SceneIndex == 3) {
					StartCountDown();
				}
				break;
		}
	}
	#endregion

	#region ��������
	#endregion

}
