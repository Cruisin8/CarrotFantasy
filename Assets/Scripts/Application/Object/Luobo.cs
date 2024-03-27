using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Luobo : Role
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	Animator m_Animator;
	#endregion

	#region 属性
	#endregion

	#region 方法
	public override void Damage(int hit)
	{
		//if(IsDead){
		//	return;
		//}

		base.Damage(hit);

		m_Animator.SetTrigger("IsDamage");

		Debug.Log("luobo_Damage");
	}

	protected override void Die(Role role)
	{
		base.Die(role);

		m_Animator.SetBool("IsDead", true);

		Debug.Log("luobo_Dead");
	}

	public override void OnGetObj()
	{
		// 初始化
		base.OnGetObj();
		m_Animator = GetComponent<Animator>();
		m_Animator.Play("Luobo_Idle");

		LuoboInfo info = StaticData.GetInstance().GetLuoboInfo();
		MaxHp = info.Hp;
		Hp = info.Hp;
	}

	public override void OnPushObj()
	{
		// 还原
		base.OnPushObj();
		m_Animator.SetBool("IsDead", false);
		m_Animator.ResetTrigger("IsDamage");
	}
	#endregion

	#region Unity回调
	#endregion

	#region 事件回调
	#endregion

	#region 帮助方法
	#endregion
}
