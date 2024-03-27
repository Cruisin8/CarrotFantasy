using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameModel : Model
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	// 所有关卡的集合
	List<Level> m_AllLevels = new List<Level>();

	// 最大通关关卡索引
	int m_GameProgress = -1;

	// 当前关卡索引
	int m_PlayLevelIndex = -1;

	// 游戏当前分数
	int m_Gold = -1;

	// 是否游戏中
	bool m_IsPlaying = false;
	#endregion

	#region 属性
	public override string Name {
		get { return Consts.M_GameModel; }
	}

	public int Gold {
		get => m_Gold;
		set => m_Gold = value;
	}

	// 关卡总数
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

	// 是否已通关
	public bool IsGamePass {
		get { return m_GameProgress > LevelCount - 1; }
	}

	public List<Level> AllLevels {
		get { return m_AllLevels; }
	}

	public Level PlayLevel {
		get { 
			if(m_PlayLevelIndex < 0 || m_PlayLevelIndex > m_AllLevels.Count - 1) {
				throw new IndexOutOfRangeException("关卡不存在");
			}
			return m_AllLevels[m_PlayLevelIndex]; 
		}
	}
	#endregion

	#region 方法
	// 初始化
	public void Initialize()
	{
		// 构建Level集合
		List<FileInfo> files = Tools.GetLevelFiles();
		List<Level> levels = new List<Level>();

		for (int i = 0; i < files.Count; i++) {
			Level level = new Level();
			Tools.FillLevel(files[i].FullName, ref level);
			levels.Add(level);
		}

		m_AllLevels = levels;

		// 读档游戏进度
		m_GameProgress = Saver.GetProgress();
		//m_GameProgress = 1;	// 测试用
	}

	// 游戏开始
	public void StartLevel(int levelIndex)
	{
		m_PlayLevelIndex = levelIndex;
		PoolMgr.GetInstance().Clear();
	}

	// 游戏结束
	public void StopLevel(bool isWin)
	{
		if (isWin && PlayLevelID > GameProgress) {
			// 保存进度
			Saver.SetProgress(PlayLevelID);

			// 更新进度
			m_GameProgress = Saver.GetProgress();
		}
		m_IsPlaying = false;
		PoolMgr.GetInstance().Clear();
	}

	// 清除存档
	public void ClearProgress()
	{
		m_IsPlaying = false;
		m_PlayLevelIndex = -1;
		m_GameProgress = -1;
		Saver.SetProgress(-1);
	}
	#endregion

	#region Unity回调
	#endregion

	#region 事件回调
	#endregion

	#region 帮助方法
	#endregion

}
