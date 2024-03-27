using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BottlBullet : Bullet
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	#endregion

	#region 属性
	// 目标
	public Monster Target { get; private set; }
	// 方向
	public Vector3 Direction { get; private set; }
	#endregion

	#region 方法
	public void Load(int bulletID, int level, Rect mapRect, Monster monster)
	{
		// 调用父类Load
		Load(bulletID, level, mapRect);

		Target = monster;
		Direction = (Target.transform.position - transform.position).normalized;
	}

	// 子弹模型面向目标
	void LookAt()
	{
		float angle = Mathf.Atan2(Direction.y, Direction.x);
		Vector3 eulerAngles = transform.eulerAngles;
		eulerAngles.z = angle * Mathf.Rad2Deg - 90;
		transform.eulerAngles = eulerAngles;
	}
	#endregion

	#region Unity回调
	protected override void Update()
	{
		// 子弹已爆炸
		if(m_IsExploded ) {
			return;
		}

		if (Target != null) {
			// 敌人未死亡，则实时调整子弹方向
			if (!Target.IsDead) {
				Direction = (Target.transform.position - transform.position).normalized;
			}

			// 调整子弹模型角度
			LookAt();

			// 子弹移动
			transform.Translate(Direction * Speed * Time.deltaTime, Space.World);

			// 击中目标
			if (Vector3.Distance(transform.position, Target.transform.position) <= Consts.DotClosedDistance) {
				// 敌人受伤
				Target.Damage(this.Attack);

				// 爆炸
				Explode();
			}
		}
		else {
			// 子弹失去目标，继续移动
			transform.Translate(Direction * Speed * Time.deltaTime, Space.World);

			// 超出地图边界后爆炸回收
			if (!m_IsExploded && !MapRect.Contains(transform.position)) {
				Explode();
			}
		}
	}
	#endregion

	#region 事件回调
	#endregion

	#region 帮助方法
	#endregion
}
