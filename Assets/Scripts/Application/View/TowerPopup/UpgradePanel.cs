using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	UpgradeIcon m_UpgradeIcon;
	SellIcon m_SellIcon;
	#endregion

	#region 属性
	#endregion

	#region 方法
	public void Show(Tower tower)
	{
		// 位置
		transform.position = tower.transform.position;

		// 显示
		m_UpgradeIcon.Load(tower);
		m_SellIcon.Load(tower);

		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
	#endregion

	#region Unity回调
	private void Awake()
	{
		m_UpgradeIcon = GetComponentInChildren<UpgradeIcon>();
		m_SellIcon = GetComponentInChildren<SellIcon>();
	}
	#endregion

	#region 事件回调
	#endregion

	#region 帮助方法
	#endregion
}
