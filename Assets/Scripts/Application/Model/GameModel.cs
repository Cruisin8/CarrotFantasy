using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameModel : Model
{
	#region ����
	#endregion

	#region �¼�
	#endregion

	#region �ֶ�
	// ���йؿ��ļ���
	List<Level> m_AllLevels = new List<Level>();

	// ���ͨ�عؿ�����
	int m_GameProgress = -1;

	// ��ǰ�ؿ�����
	int m_PlayLevelIndex = -1;

	// ��Ϸ��ǰ����
	int m_Gold = -1;

	// �Ƿ���Ϸ��
	bool m_IsPlaying = false;
	#endregion

	#region ����
	public override string Name {
		get { return Consts.M_GameModel; }
	}

	public int Gold {
		get => m_Gold;
		set => m_Gold = value;
	}

	// �ؿ�����
	public int LevelCount {
		get { return m_AllLevels.Count; }
	}

	public int GameProgress {
		get { return m_GameProgress; }
	}

	public int PlayLevelID {
		get { return m_PlayLevelIndex; }
	}

	public bool IsPlaying {
		get => m_IsPlaying;
		set => m_IsPlaying = value;
	}

	// �Ƿ���ͨ��
	public bool IsGamePass {
		get { return m_GameProgress > LevelCount - 1; }
	}

	public List<Level> AllLevels {
		get { return m_AllLevels; }
	}

	public Level PlayLevel {
		get { 
			if(m_PlayLevelIndex < 0 || m_PlayLevelIndex > m_AllLevels.Count - 1) {
				throw new IndexOutOfRangeException("�ؿ�������");
			}
			return m_AllLevels[m_PlayLevelIndex]; 
		}
	}
	#endregion

	#region ����
	// ��ʼ��
	public void Initialize()
	{
		// ����Level����
		List<FileInfo> files = Tools.GetLevelFiles();
		List<Level> levels = new List<Level>();

		for (int i = 0; i < files.Count; i++) {
			Level level = new Level();
			Tools.FillLevel(files[i].FullName, ref level);
			levels.Add(level);
		}

		m_AllLevels = levels;

		// ������Ϸ����
		m_GameProgress = Saver.GetProgress();
		//m_GameProgress = 1;	// ������
	}

	// ��Ϸ��ʼ
	public void StartLevel(int levelIndex)
	{
		m_PlayLevelIndex = levelIndex;
		PoolMgr.GetInstance().Clear();
	}

	// ��Ϸ����
	public void StopLevel(bool isWin)
	{
		if (isWin && PlayLevelID > GameProgress) {
			// �������
			Saver.SetProgress(PlayLevelID);

			// ���½���
			m_GameProgress = Saver.GetProgress();
		}
		m_IsPlaying = false;
		PoolMgr.GetInstance().Clear();
	}

	// ����浵
	public void ClearProgress()
	{
		m_IsPlaying = false;
		m_PlayLevelIndex = -1;
		m_GameProgress = -1;
		Saver.SetProgress(-1);
	}
	#endregion

	#region Unity�ص�
	#endregion

	#region �¼��ص�
	#endregion

	#region ��������
	#endregion

}
