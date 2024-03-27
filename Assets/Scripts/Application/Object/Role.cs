using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 游戏角色基类
public abstract class Role : ReusbleObject, IReusable
{
	#region 常量

	#endregion

	#region 事件
	public event Action<int /* m_Hp */, int /* m_MaxHp */> HpChanged;	// 血量变化事件
	public event Action<Role> Dead;				// 死亡事件
	#endregion

	#region 字段
	int m_Hp;			// 当前生命值
	int m_MaxHp;		// 最大生命值
	#endregion

	#region 属性
	public int Hp {
		get { return m_Hp; }
		set {
			// 将传入的生命值限制在 0 - m_MaxHp 之间
			value = Mathf.Clamp(value, 0, m_MaxHp);

			// 减少重复传入
			if(value == m_Hp) {
				return;
			}

			// 赋值血量
			m_Hp = value; 

			// 血量变化
			if(HpChanged != null) {
				HpChanged(m_Hp, m_MaxHp);
			}

			// 死亡事件
			if(m_Hp == 0) {
				if (Dead != null) {
					Dead(this);
				}
			}
		}
	}

	public int MaxHp
	{
		get{ return m_MaxHp; }
		set{ 
			if(value < 0) {
				value = 0;
			}

			m_MaxHp = value;
		}
	}

	// 是否死亡
	public bool IsDead {
		get { return m_Hp == 0; }
	}
	#endregion

	#region 方法
	// 受伤
	public virtual void Damage(int hit)
	{
		if (IsDead) {
			return;
		}

		Hp -= hit;
	}

	// 死亡事件时调用
	protected virtual void Die(Role role)
	{

	}
	#endregion

	#region Unity回调
	#endregion

	#region 事件回调
	public override void OnGetObj()
	{
		this.Dead += Die;
	}

	public override void OnPushObj()
	{
		Hp = 0;
		MaxHp = 0;

		while(HpChanged != null) {
			HpChanged -= HpChanged;
		}

		while(Dead != null) {
			Dead -= Dead;
		}
	}
	#endregion

	#region 帮助方法
	#endregion
}
