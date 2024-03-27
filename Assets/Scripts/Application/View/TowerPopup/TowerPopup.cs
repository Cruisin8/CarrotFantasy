using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerPopup : View
{
	#region 常量
	#endregion

	#region 事件

	#endregion

	#region 字段
	public SpawnPanel SpawnPanel;
	public UpgradePanel UpgradePanel;

	private static TowerPopup m_Instance = null;
	#endregion

	#region 属性
	public static TowerPopup Instance {
		get { return m_Instance; }
	}

	public override string Name {
		get { return Consts.V_TowerPopup; }
	}

	public bool IsPopShow {
		get { 
			foreach(Transform child in transform) {
				if (child.gameObject.activeSelf) {
					return true;
				}
			}
			return false;
		}
	}
	#endregion

	#region 方法
	// 显示炮塔放置界面
	void ShowSpawnPanel(Vector3 position, bool isUpSide)
	{
		HideAllPanels();

		GameModel gModel = GetModel<GameModel>();
		SpawnPanel.Show(gModel, position, isUpSide);
	}

	// 显示炮塔升级面板
	void ShowUpgradePanel(Tower tower)
	{
		HideAllPanels();

		UpgradePanel.Show(tower);
	}
	
	// 隐藏炮塔面板
	void HideAllPanels()
	{
		SpawnPanel.Hide();
		UpgradePanel.Hide();
	}

	void OnSpawnTower(object[] args)
	{
		int towerID = (int)args[0];
		Vector3 position = (Vector3)args[1];

		SendEvent(Consts.E_SpawnTower, new SpawnTowerArgs() { TowerID = towerID, Position = position });
	}

	void OnUpgardeTower(Tower tower)
	{
		SendEvent(Consts.E_UpgradeTower, new UpgradeTowerArgs() { Tower = tower });
	}

	void OnSellTower(Tower tower)
	{
		SendEvent(Consts.E_SellTower, new SellTowerArgs() { Tower = tower });
	}
	#endregion

	#region Unity回调
	private void Awake()
	{
		m_Instance = this;
	}

	private void Start()
	{
		HideAllPanels();
	}
	#endregion

	#region 事件回调
	public override void RegisterEvents()
	{
		AttentionEvents.Add(Consts.E_ShowSpawnPanel);
		AttentionEvents.Add(Consts.E_ShowUpgradePanel);
		AttentionEvents.Add(Consts.E_HidePopups);
	}

	public override void HandleEvent(string eventName, object data)
	{
		switch(eventName) {
			case Consts.E_ShowSpawnPanel:
				ShowSpawnPanelArgs e1 = (ShowSpawnPanelArgs)data;
				ShowSpawnPanel(e1.Position, e1.IsUpSide);
				break;
			case Consts.E_ShowUpgradePanel:
				ShowUpgradePanelArgs e2 = (ShowUpgradePanelArgs)data;
				ShowUpgradePanel(e2.Tower);
				break;
			case Consts.E_HidePopups:
				HideAllPanels();
				break;
			default: 
				break;
		}
	}
	#endregion

	#region 帮助方法
	#endregion


}
