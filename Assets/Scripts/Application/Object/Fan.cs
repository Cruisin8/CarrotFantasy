using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Tower
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	public int BulletCount = 6;	// 每次攻击发射的子弹数量
	#endregion

	#region 属性
	#endregion

	#region 方法
	protected override void Attack(Monster monster)
	{
		base.Attack(monster);

		for (int i = 0; i < BulletCount; i++) {
			// 发射方向
			float radians = (Mathf.PI * 2f / BulletCount) * i;
			Vector3 dir = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f);

			// 产生子弹
			PoolMgr.GetInstance().GetObj(Consts.PrefabsDir + "FanBullet1", (obj) => {
				FanBullet bullet = obj.GetComponent<FanBullet>();
				// 以中心点为发射位置
				bullet.transform.position = transform.position;
				bullet.Load(this.UseBulletID, this.Level, this.MapRect, dir);
			});
		}

		// 音效

	}
	#endregion

	#region Unity回调
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
