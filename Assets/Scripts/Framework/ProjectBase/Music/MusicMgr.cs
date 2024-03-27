using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 音效管理模块
/// </summary>
public class MusicMgr : BaseManager<MusicMgr>
{
    // 背景音乐
    private AudioSource bkMusic = null;
    // 背景音乐音量
    private float bkVolume = 1;

    // 音效依附对象
    private GameObject soundObj = null;
    // 音效容器
    private List<AudioSource> soundList = new List<AudioSource>();
    // 音效音量
    private float soundVolume = 1; 

    public MusicMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
    }

    private void Update()
    {
        // 在Update中，持续检测是否有音效播放完毕，并移除播放完毕的音效
        for (int i = soundList.Count - 1; i >= 0 ; i--) {
            if (!soundList[i].isPlaying) {
				GameObject.Destroy(soundList[i]);
				soundList.RemoveAt(i);
			}
        }
    }

    // 播放背景音乐
    public void PlayBkMusic(string name)
    {
        if(bkMusic == null) {
            GameObject obj = new GameObject();
            obj.name = "BkMusic";
            bkMusic = obj.AddComponent<AudioSource>();
        }

		// 异步加载Music/BK/...路径下的音乐资源，加载完成后执行委托函数，委托函数的入参为clip
		ResMgr.GetInstance().LoadAsync<AudioClip>("Music/BK/" + name, (clip) => {
            bkMusic.clip = clip;
            // 设置为循环播放
            bkMusic.loop = true;
            // 设置初始音量
            bkMusic.volume = bkVolume;
            bkMusic.Play();
        });
    }

	// 暂停背景音乐
	public void PauseBkMusic()
	{
		if (bkMusic == null) {
			return;
		}
		bkMusic.Pause();
	}

	// 停止背景音乐
	public void StopBkMusic()
    {
        if(bkMusic == null) {
            return;
        }
        bkMusic.Stop();
    }

    // 修改背景音乐声音大小
	public void ChangeBkValue(float v)
	{
		bkVolume = v;
		if (bkMusic == null) {
			return;
		}
        bkMusic.volume = bkVolume;
	}

	// 播放音效
	public void PlaySound(string name, bool isLoop,UnityAction<AudioSource> callback = null)
    {
        if(soundObj == null) {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }

		// 异步加载Music/Sound/...路径下的音乐资源，等加载结束后再添加这个音效到场景上
		ResMgr.GetInstance().LoadAsync<AudioClip>("Music/Sound/" + name, (clip) => {
			AudioSource source = soundObj.AddComponent<AudioSource>();
			source.clip = clip;
            source.loop = isLoop;
			source.volume = soundVolume;
			source.Play();
			soundList.Add(source);
            if(callback != null) {
                callback(source);
            }
		});
	}

	// 修改音效音量大小
	public void ChangeSoundValue(float v)
	{
		soundVolume = v;
        for (int i = 0; i < soundList.Count; i++) {
            soundList[i].volume = v;
        }
	}

	// 停止音效
	public void StopSound(AudioSource source)
    {
        if(soundList.Contains(source)) {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
}
