using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBoard : View
{
	#region ����
	#endregion

	#region �¼�
	#endregion

	#region �ֶ�
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

	// ����/��ͣ
	bool m_IsPlaying = false;

	// ��Ϸ�ٶ�
	GameSpeed m_Speed = GameSpeed.One;

	// ����
	int m_Score = 0;
	#endregion

	#region ����
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

	#region ����
	// ���»غ���Ϣ
	public void UpdateRoundInfo(int currentRound, int totalRound)
	{
		txtCurrent.text = currentRound.ToString("D2");//ʼ�ձ���2λ����
		txtTotal.text = totalRound.ToString();
	}
	#endregion

	#region Unity�ص�

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

	#region �¼��ص�
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
		// �˵���

	}
	#endregion

	#region ��������
	#endregion
}
