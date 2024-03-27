using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPanel : MonoBehaviour
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	TowerIcon[] m_TowerIcons;
	#endregion

	#region 属性
	#endregion

	#region 方法
	// 显示界面
	public void Show(GameModel gModel, Vector3 position, bool isUpSide)
	{
		// 设置位置
		transform.position = position;

		// 动态加载图标
		for (int i = 0; i < m_TowerIcons.Length; i++) {
			TowerInfo towerInfo = StaticData.GetInstance().GetTowerInfo(i);
			m_TowerIcons[i].Load(gModel, towerInfo, position, isUpSide);
		}

		// 显示
		gameObject.SetActive(true);
	}

	// 隐藏界面
	public void Hide()
	{
		gameObject.SetActive(false);
	}
	#endregion

	#region Unity回调
	private void Awake()
	{
		m_TowerIcons = GetComponentsInChildren<TowerIcon>();
	}
	#endregion

	#region 事件回调
	#endregion

	#region 帮助方法
	#endregion
}
