using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  用于测试音效管理模块
/// </summary>
public class MusicTest : MonoBehaviour
{
	float volume = 0;

	AudioSource source;

	// 在场景中创建按钮测试音效
	void OnGUI()
	{
		// 测试背景音乐功能
		if(GUI.Button(new Rect(0, 0, 100, 100), "播放音乐")) {
			volume = 0;
			MusicMgr.GetInstance().PlayBkMusic("TestBK_music");
		}

		if (GUI.Button(new Rect(0, 100, 100, 100), "暂停音乐")) {
			MusicMgr.GetInstance().PauseBkMusic();
		}

		if (GUI.Button(new Rect(0, 200, 100, 100), "停止音乐")) {
			MusicMgr.GetInstance().StopBkMusic();
		}

		// 修改背景音乐音量测试
		volume += Time.deltaTime / 100;
		MusicMgr.GetInstance().ChangeBkValue(volume);

		// 测试音效功能
		if (GUI.Button(new Rect(0, 300, 100, 100), "播放音效")) {
			MusicMgr.GetInstance().PlaySound("TestSound_shoot", false, (clip) => {
				source = clip;
			});
		}

		if (GUI.Button(new Rect(0, 400, 100, 100), "停止音效")) {
			MusicMgr.GetInstance().StopSound(source);
			source = null;
		}
	}
}
