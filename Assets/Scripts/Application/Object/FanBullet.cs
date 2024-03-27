using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FanBullet : Bullet
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	//旋转速度（度/秒）
	public float RotateSpeed = 180f;
	#endregion

	#region 属性
	public Vector2 Direction { get; private set; }
	#endregion

	#region 方法
	public void Load(int bulletID, int level, Rect mapRect, Vector3 direction)
	{
		Load(bulletID, level, mapRect);
		Direction = direction;
	}
	#endregion

	#region Unity回调

	protected override void Update()
	{
		//已爆炸跳过
		if (m_IsExploded) {
			return;
		}

		//子弹移动
		transform.Translate(Direction * Speed * Time.deltaTime, Space.World);

		//旋转
		transform.Rotate(Vector3.forward, RotateSpeed * Time.deltaTime, Space.World);

		//检测（存活/死亡）
		GameObject[] monsterObjects = GameObject.FindGameObjectsWithTag("Monster");

		foreach (GameObject monsterObject in monsterObjects) {
			Monster monster = monsterObject.GetComponent<Monster>();

			//忽略已死亡的怪物
			if (monster.IsDead)
				continue;

			if (Vector3.Distance(transform.position, monster.transform.position) <= Consts.RangeClosedDistance) {
				//敌人受伤
				monster.Damage(this.Attack);

				//爆炸
				Explode();

				//退出
				break;
			}
		}

		// 超出地图边界后爆炸回收
		if (!m_IsExploded && !MapRect.Contains(transform.position)) {
			Explode();
		}
	}
	#endregion

	#region 事件回调
	#endregion

	#region 帮助方法
	#endregion
}
