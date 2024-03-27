using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeIcon : MonoBehaviour
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	SpriteRenderer m_Render;
	Tower m_Tower;
	#endregion

	#region 属性
	#endregion

	#region 方法
	public void Load(Tower tower)
	{
		m_Tower = tower;
		TowerInfo info = StaticData.GetInstance().GetTowerInfo(tower.ID);
		string path = Consts.RolesDir + (tower.IsTopLevel ? info.DisabledIcon : info.NormalIcon);
		m_Render.sprite = Resources.Load<Sprite>(path);
	}
	#endregion

	#region Unity回调
	private void Awake()
	{
		m_Render = GetComponent<SpriteRenderer>();
	}

	private void OnMouseDown()
	{
		if (m_Tower.IsTopLevel)
			return;

		UpgradeTowerArgs e = new UpgradeTowerArgs() {
			Tower = m_Tower
		};

		SendMessageUpwards("OnUpgradeTower", m_Tower, SendMessageOptions.RequireReceiver);
	}
	#endregion

	#region 事件回调
	#endregion

	#region 帮助方法
	#endregion
}
