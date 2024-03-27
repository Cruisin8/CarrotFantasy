using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISelect : View
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	public Button btnStart;

	List<Card> m_Cards = new List<Card>();
	int m_SelectedIndex = -1;
	GameModel m_GameModel = null;
	#endregion

	#region 属性
	public override string Name {
		get { return Consts.V_Select; }
	}
	#endregion

	#region 方法
	// 返回开始界面
	public void GoBack()
	{
		Game.GetInstance().LoadScene((int)SceneID.Start);
	}

	// 选中关卡游戏
	public void ChooseLevel()
	{
		StartLevelArgs e = new StartLevelArgs() {
			LevelIndex = m_SelectedIndex
		};

		SendEvent(Consts.E_StartLevel, e);
	}

	// 加载地图卡片
	private void LoadCards()
	{
		// 获取Level集合
		List<Level> levels = m_GameModel.AllLevels;

		// 构建Card集合
		List<Card> cards = new List<Card>();
		for(int i = 0; i < levels.Count;i++) {
			Card card = new Card() {
				LevelID = i,
				CardImage = levels[i].CardImage,
				IsLocked = i > (m_GameModel.GameProgress + 1)	// 已过关卡 和 当前关卡 不锁定
			};

			cards.Add(card);
		}

		// 保存
		this.m_Cards = cards;

		// 监听卡片点击事件
		UICard[] uiCards = this.transform.Find("UICards").GetComponentsInChildren<UICard>();
		foreach(UICard uiCard in uiCards) {
			uiCard.OnClick += (card) => {
				SelectCard(card.LevelID);
			};
		}

		// 默认选择第一个地图卡片
		SelectCard(0);
	}

	// 选择地图卡片
	private void SelectCard(int index)
	{
		if(m_SelectedIndex == index) {
			return;
		}

		m_SelectedIndex = index;

		// 计算索引号
		int lastIndex = m_SelectedIndex - 1;
		int currentIndex = m_SelectedIndex;
		int nextIndex = m_SelectedIndex + 1;

		// 绑定数据
		Transform container = this.transform.Find("UICards");

		// 上一张地图
		if (lastIndex < 0) {
			container.GetChild(0).gameObject.SetActive(false);
		} else {
			container.GetChild(0).gameObject.SetActive(true);

			container.GetChild(0).GetComponent<UICard>().IsTransparent = true;
			container.GetChild(0).GetComponent<UICard>().DataBind(m_Cards[lastIndex]);
		}

		// 当前地图
		container.GetChild(1).GetComponent<UICard>().IsTransparent = false;
		container.GetChild(1).GetComponent<UICard>().DataBind(m_Cards[currentIndex]);
		// 控制开始按钮状态
		btnStart.gameObject.SetActive(!m_Cards[currentIndex].IsLocked);

		// 下一张地图
		if (nextIndex >= m_Cards.Count) {
			container.GetChild(2).gameObject.SetActive(false);
		} else {
			container.GetChild(2).gameObject.SetActive(true);
			container.GetChild(2).GetComponent<UICard>().IsTransparent = true;
			container.GetChild(2).GetComponent<UICard>().DataBind(m_Cards[nextIndex]);
		}

	}
	#endregion

	#region Unity回调
	#endregion

	#region 事件回调
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
					// 获取模型数据
					m_GameModel = GetModel<GameModel>();
					// 初始化地图卡片列表
					LoadCards();
				}
				break;
		}
	}
	#endregion

	#region 帮助方法
	#endregion

}
