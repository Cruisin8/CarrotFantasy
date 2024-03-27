using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��Ч����ģ��
/// </summary>
public class MusicMgr : BaseManager<MusicMgr>
{
    // ��������
    private AudioSource bkMusic = null;
    // ������������
    private float bkVolume = 1;

    // ��Ч��������
    private GameObject soundObj = null;
    // ��Ч����
    private List<AudioSource> soundList = new List<AudioSource>();
    // ��Ч����
    private float soundVolume = 1; 

    public MusicMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
    }

    private void Update()
    {
        // ��Update�У���������Ƿ�����Ч������ϣ����Ƴ�������ϵ���Ч
        for (int i = soundList.Count - 1; i >= 0 ; i--) {
            if (!soundList[i].isPlaying) {
				GameObject.Destroy(soundList[i]);
				soundList.RemoveAt(i);
			}
        }
    }

    // ���ű�������
    public void PlayBkMusic(string name)
    {
        if(bkMusic == null) {
            GameObject obj = new GameObject();
            obj.name = "BkMusic";
            bkMusic = obj.AddComponent<AudioSource>();
        }

		// �첽����Music/BK/...·���µ�������Դ��������ɺ�ִ��ί�к�����ί�к��������Ϊclip
		ResMgr.GetInstance().LoadAsync<AudioClip>("Music/BK/" + name, (clip) => {
            bkMusic.clip = clip;
            // ����Ϊѭ������
            bkMusic.loop = true;
            // ���ó�ʼ����
            bkMusic.volume = bkVolume;
            bkMusic.Play();
        });
    }

	// ��ͣ��������
	public void PauseBkMusic()
	{
		if (bkMusic == null) {
			return;
		}
		bkMusic.Pause();
	}

	// ֹͣ��������
	public void StopBkMusic()
    {
        if(bkMusic == null) {
            return;
        }
        bkMusic.Stop();
    }

    // �޸ı�������������С
	public void ChangeBkValue(float v)
	{
		bkVolume = v;
		if (bkMusic == null) {
			return;
		}
        bkMusic.volume = bkVolume;
	}

	// ������Ч
	public void PlaySound(string name, bool isLoop,UnityAction<AudioSource> callback = null)
    {
        if(soundObj == null) {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }

		// �첽����Music/Sound/...·���µ�������Դ���ȼ��ؽ���������������Ч��������
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

	// �޸���Ч������С
	public void ChangeSoundValue(float v)
	{
		soundVolume = v;
        for (int i = 0; i < soundList.Count; i++) {
            soundList[i].volume = v;
        }
	}

	// ֹͣ��Ч
	public void StopSound(AudioSource source)
    {
        if(soundList.Contains(source)) {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
}
