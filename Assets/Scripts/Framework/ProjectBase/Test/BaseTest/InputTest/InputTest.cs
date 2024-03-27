using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ڲ���InputMgr ������ƹ���
/// </summary>
public class InputTest : MonoBehaviour
{
	private void Start()
	{
		InputMgr.GetInstance().EnableInputCheck();

		EventCenter.GetInstance().AddEventListener<KeyCode>("ĳ������", CheckInputDown);
		EventCenter.GetInstance().AddEventListener<KeyCode>("ĳ��̧��", CheckInputUp);
	}

	// ���¼�λ
	private void CheckInputDown(KeyCode key)
	{
		KeyCode keyCode = (KeyCode)key;
		switch(keyCode) {
			case KeyCode.W:
				Debug.Log("ǰ��");
				break;
			case KeyCode.A:
				Debug.Log("��ת");
				break;
			case KeyCode.S:
				Debug.Log("����");
				break;
			case KeyCode.D:
				Debug.Log("��ת");
				break;
			case KeyCode.Space:
				// δ��InputMgr�зַ������ᴥ��
				Debug.Log("��Ծ");
				break;
		}
	}

	// ̧���λ
	private void CheckInputUp(KeyCode key)
	{
		KeyCode keyCode = (KeyCode)key;
		switch (keyCode) {
			case KeyCode.W:
				Debug.Log("ֹͣǰ��");
				break;
			case KeyCode.A:
				Debug.Log("ֹͣ��ת");
				break;
			case KeyCode.S:
				Debug.Log("ֹͣ����");
				break;
			case KeyCode.D:
				Debug.Log("ֹͣ��ת");
				break;
		}
	}
}
