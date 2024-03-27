using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 塔
public class TowerInfo
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	public int ID;              // ID
	public string PrefabName;	// 塔的名称
	public string NormalIcon;	// 正常图标
	public string DisabledIcon; // 无法购买的图标
	public int BasePrice;       // 基础价格
	public int MaxLevel;        // 最大级别
	public float GuardRange;    // 攻击范围
	public float ShootRate;       // 攻速（平均1秒发射的子弹数量）
	public int UseBulletID;		// 使用的子弹ID
	#endregion

	#region 属性
	#endregion

	#region 方法
	#endregion

	#region Unity回调
	#endregion

	#region 事件回调
	#endregion

	#region 帮助方法
	#endregion
}
