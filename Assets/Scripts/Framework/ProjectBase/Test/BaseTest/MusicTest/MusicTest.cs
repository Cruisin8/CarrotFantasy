using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ���ڲ�����Ч����ģ��
/// </summary>
public class MusicTest : MonoBehaviour
{
	float volume = 0;

	AudioSource source;

	// �ڳ����д�����ť������Ч
	void OnGUI()
	{
		// ���Ա������ֹ���
		if(GUI.Button(new Rect(0, 0, 100, 100), "��������")) {
			volume = 0;
			MusicMgr.GetInstance().PlayBkMusic("TestBK_music");
		}

		if (GUI.Button(new Rect(0, 100, 100, 100), "��ͣ����")) {
			MusicMgr.GetInstance().PauseBkMusic();
		}

		if (GUI.Button(new Rect(0, 200, 100, 100), "ֹͣ����")) {
			MusicMgr.GetInstance().StopBkMusic();
		}

		// �޸ı���������������
		volume += Time.deltaTime / 100;
		MusicMgr.GetInstance().ChangeBkValue(volume);

		// ������Ч����
		if (GUI.Button(new Rect(0, 300, 100, 100), "������Ч")) {
			MusicMgr.GetInstance().PlaySound("TestSound_shoot", false, (clip) => {
				source = clip;
			});
		}

		if (GUI.Button(new Rect(0, 400, 100, 100), "ֹͣ��Ч")) {
			MusicMgr.GetInstance().StopSound(source);
			source = null;
		}
	}
}
