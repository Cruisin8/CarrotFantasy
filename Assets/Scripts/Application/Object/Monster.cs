﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 怪物基类
public class Monster : Role
{
	#region 常量
	public const float CLOSED_DISTANCE = 0.1f;	// 接近终点的距离(float类数据距离相近视为相等)
	#endregion

	#region 事件
	public event Action<Monster> Reached;	// 到达终点事件
	#endregion

	#region 字段
	public MonsterType MonsterType = MonsterType.Monster0;	// 怪物类型
	float m_MoveSpeed;			// 移动速度
	Vector3[] m_Path = null;	// 路径拐点
	int m_PointIndex = -1;		// 下一个拐点的索引值
	bool m_IsReached = false;   // 是否到达终点
	#endregion

	#region 属性
	public float MoveSpeed {
		get { return m_MoveSpeed; }
		set { m_MoveSpeed = value; }
	}
	#endregion

	#region 方法
	public void Load(Vector3[] path)
	{
		m_Path = path;
		MoveNext();
	}

	// 是否还有下一个移动点
	bool HasNext() 
	{
		return (m_PointIndex + 1) < (m_Path.Length - 1);
	}

	void MoveTo(Vector3 position)
	{
		transform.position = position;
	}

	void MoveNext()
	{
		if(!HasNext()) {
			return;
		}

		if(m_PointIndex == -1) {
			// 起点位置
			m_PointIndex = 0;
			MoveTo(m_Path[m_PointIndex]);
		} else {
			// 下一点的位置
			m_PointIndex++;
		}
	}
	#endregion

	#region Unity回调
	private void Update()
	{
		// 已到达终点
		if(m_IsReached) {
			return;
		}

		// 当前位置
		Vector3 pos = transform.position;
		// 目标位置
		Vector3 dest = m_Path[m_PointIndex + 1];
		// 计算距离
		float dis = Vector3.Distance(pos, dest);

		if(dis < CLOSED_DISTANCE) {
			// 到达拐点
			MoveTo(dest);

			if(HasNext()) {
				MoveNext();
			} else {
				// 到达终点
				m_IsReached = true;

				// 触发到达终点事件
				if(Reached != null) {
					Reached(this);
				}
			}
		}
		else {
			// 移动的单位方向
			Vector3 direction = (dest - pos).normalized;

			// 每帧移动(米/帧 = 米/秒 * Time.deltaTime)
			transform.Translate(direction * m_MoveSpeed * Time.deltaTime);
		}
	}
	#endregion

	#region 事件回调
	public override void OnGetObj()
	{
		base.OnGetObj();

		MonsterInfo info = StaticData.GetInstance().GetMonsterInfo((int)MonsterType);
		// 先赋值MaxHp，防止Hp赋值失败
		this.MaxHp = info.Hp;
		this.Hp = info.Hp;
		this.MoveSpeed = info.MoveSpeed;
	}

	public override void OnPushObj()
	{
		base.OnPushObj();

		this.m_Path = null;
		this.m_PointIndex = -1;
		this.m_IsReached = false;
		this.m_MoveSpeed = 0;
		this.Reached = null;
	}
	#endregion

	#region 帮助方法
	#endregion
}
