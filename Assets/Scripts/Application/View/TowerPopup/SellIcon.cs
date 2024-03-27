using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellIcon : MonoBehaviour
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
		// 保存塔数据
		m_Tower = tower;
	}
	#endregion

	#region Unity回调
	private void Awake()
	{
		m_Render = GetComponent<SpriteRenderer>();
	}

	private void OnMouseDown()
	{
		SendMessageUpwards("OnSellTower", m_Tower, SendMessageOptions.RequireReceiver);
	}
	#endregion

	#region 事件回调
	#endregion

	#region 帮助方法
	#endregion
}

