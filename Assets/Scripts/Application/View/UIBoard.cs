using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBoard : View
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	public TMP_Text txtScore;
	public Image imgRoundInfo;
	public TMP_Text txtCurrent;
	public TMP_Text txtTotal;
	public Image imgPauseInfo;
	public Button btnSpeed1;
	public Button btnSpeed2;
	public Button btnResume;
	public Button btnPause;
	public Button btnSystem;

	// 播放/暂停
	bool m_IsPlaying = false;

	// 游戏速度
	GameSpeed m_Speed = GameSpeed.One;

	// 分数
	int m_Score = 0;
	#endregion

	#region 属性
	public override string Name 
	{
		get { return Consts.V_Board; }
	}

	public bool IsPlaying 
	{
		get{ return m_IsPlaying; }
		set{
			m_IsPlaying = value;

			btnPause.gameObject.SetActive(value);
			btnResume.gameObject.SetActive(!value);
			imgPauseInfo.gameObject.SetActive(!value);
		}

	}

	public GameSpeed Speed 
	{
		get { return m_Speed; }
			
		set { 
			m_Speed = value; 

			btnSpeed1.gameObject.SetActive(m_Speed == GameSpeed.One);
			btnSpeed2.gameObject.SetActive(m_Speed == GameSpeed.Two);
		}
	}

	public int Score {
		get { return m_Score; }

		set {
			m_Score = value;
			txtScore.text = value.ToString();
		}
	}
	#endregion

	#region 方法
	// 更新回合信息
	public void UpdateRoundInfo(int currentRound, int totalRound)
	{
		txtCurrent.text = currentRound.ToString("D2");//始终保留2位整数
		txtTotal.text = totalRound.ToString();
	}
	#endregion

	#region Unity回调

	void Awake()
	{
		this.Score = 0;
		this.IsPlaying = true;
		this.Speed = GameSpeed.One;
	}

	private void Update()
	{
		Time.timeScale = (float)Speed;
	}
	#endregion

	#region 事件回调
	public override void RegisterEvents()
	{

	}

	public override void HandleEvent(string eventName, object data)
	{
		
	}

	public void OnSpeed1Click()
	{
		Speed = GameSpeed.Two;
	}

	public void OnSpeed2Click()
	{
		Speed = GameSpeed.One;
	}

	public void OnPauseClick()
	{
		IsPlaying = false;
		Speed = GameSpeed.Zero;
	}

	public void OnResumeClick()
	{
		IsPlaying = true;
		Speed = GameSpeed.One;
	}

	public void OnSystemClick()
	{
		// 菜单栏

	}
	#endregion

	#region 帮助方法
	#endregion
}
