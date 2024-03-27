using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISelect : View
{
	#region ����
	#endregion

	#region �¼�
	#endregion

	#region �ֶ�
	public Button btnStart;

	List<Card> m_Cards = new List<Card>();
	int m_SelectedIndex = -1;
	GameModel m_GameModel = null;
	#endregion

	#region ����
	public override string Name {
		get { return Consts.V_Select; }
	}
	#endregion

	#region ����
	// ���ؿ�ʼ����
	public void GoBack()
	{
		Game.GetInstance().LoadScene((int)SceneID.Start);
	}

	// ѡ�йؿ���Ϸ
	public void ChooseLevel()
	{
		StartLevelArgs e = new StartLevelArgs() {
			LevelIndex = m_SelectedIndex
		};

		SendEvent(Consts.E_StartLevel, e);
	}

	// ���ص�ͼ��Ƭ
	private void LoadCards()
	{
		// ��ȡLevel����
		List<Level> levels = m_GameModel.AllLevels;

		// ����Card����
		List<Card> cards = new List<Card>();
		for(int i = 0; i < levels.Count;i++) {
			Card card = new Card() {
				LevelID = i,
				CardImage = levels[i].CardImage,
				IsLocked = i > (m_GameModel.GameProgress + 1)	// �ѹ��ؿ� �� ��ǰ�ؿ� ������
			};

			cards.Add(card);
		}

		// ����
		this.m_Cards = cards;

		// ������Ƭ����¼�
		UICard[] uiCards = this.transform.Find("UICards").GetComponentsInChildren<UICard>();
		foreach(UICard uiCard in uiCards) {
			uiCard.OnClick += (card) => {
				SelectCard(card.LevelID);
			};
		}

		// Ĭ��ѡ���һ����ͼ��Ƭ
		SelectCard(0);
	}

	// ѡ���ͼ��Ƭ
	private void SelectCard(int index)
	{
		if(m_SelectedIndex == index) {
			return;
		}

		m_SelectedIndex = index;

		// ����������
		int lastIndex = m_SelectedIndex - 1;
		int currentIndex = m_SelectedIndex;
		int nextIndex = m_SelectedIndex + 1;

		// ������
		Transform container = this.transform.Find("UICards");

		// ��һ�ŵ�ͼ
		if (lastIndex < 0) {
			container.GetChild(0).gameObject.SetActive(false);
		} else {
			container.GetChild(0).gameObject.SetActive(true);

			container.GetChild(0).GetComponent<UICard>().IsTransparent = true;
			container.GetChild(0).GetComponent<UICard>().DataBind(m_Cards[lastIndex]);
		}

		// ��ǰ��ͼ
		container.GetChild(1).GetComponent<UICard>().IsTransparent = false;
		container.GetChild(1).GetComponent<UICard>().DataBind(m_Cards[currentIndex]);
		// ���ƿ�ʼ��ť״̬
		btnStart.gameObject.SetActive(!m_Cards[currentIndex].IsLocked);

		// ��һ�ŵ�ͼ
		if (nextIndex >= m_Cards.Count) {
			container.GetChild(2).gameObject.SetActive(false);
		} else {
			container.GetChild(2).gameObject.SetActive(true);
			container.GetChild(2).GetComponent<UICard>().IsTransparent = true;
			container.GetChild(2).GetComponent<UICard>().DataBind(m_Cards[nextIndex]);
		}

	}
	#endregion

	#region Unity�ص�
	#endregion

	#region �¼��ص�
	public override void RegisterEvents()
	{
		AttentionEvents.Add(Consts.E_EnterScene);
	}

	public override void HandleEvent(string eventName, object data)
	{
		switch (eventName) {
			case Consts.E_EnterScene:
				SceneArgs e = (SceneArgs)data;
				if(e.SceneIndex == 2) {
					// ��ȡģ������
					m_GameModel = GetModel<GameModel>();
					// ��ʼ����ͼ��Ƭ�б�
					LoadCards();
				}
				break;
		}
	}
	#endregion

	#region ��������
	#endregion

}
