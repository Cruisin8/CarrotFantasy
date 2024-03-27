using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Bottle : Tower
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	// 子弹发射位置
	Transform m_AttackPoint;
	#endregion

	#region 属性
	#endregion

	#region 方法
	protected override void Attack(Monster monster)
	{
		base.Attack(monster);

		PoolMgr.GetInstance().GetObj(Consts.PrefabsDir + "BottleBullet1", (obj) => {
			BottlBullet bullet = obj.GetComponent<BottlBullet>();
			bullet.transform.position = m_AttackPoint.position;
			bullet.Load(this.UseBulletID, this.Level, this.MapRect, monster);
		});

		// 音效

	}

	#endregion

	#region Unity回调
	protected override void Awake()
	{
		base.Awake();
		m_AttackPoint = transform.Find("AttackPoint");
	}
	#endregion

	#region 事件回调
	public override void OnGetObj()
	{
		base.OnGetObj();
	}

	public override void OnPushObj()
	{
		base.OnPushObj();
	}
	#endregion

	#region 帮助方法
	#endregion
}
