using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : ReusbleObject, IReusable
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	//延迟回收时间(秒)（给播放爆炸动画预留的时间）
	public float DelayToDestory = 1f;

	//是否爆炸
	protected bool m_IsExploded = false;

	//动画组件
	Animator m_Animator;
	#endregion

	#region 属性
	//类型
	public int ID { get; private set; }
	//等级
	public int Level { get; set; }
	//基本速度
	public float BaseSpeed { get; private set; }
	//基本攻击力
	public int BaseAttack { get; private set; }

	//移动速度
	public float Speed { get { return BaseSpeed * Level; } }
	//攻击力
	public int Attack { get { return BaseAttack * Level; } }

	//地图范围（用于判定子弹回收时机）
	public Rect MapRect { get; private set; }
	#endregion

	#region 方法
	// 子弹初始化
	private void InitBullet()
	{
		m_IsExploded = false;
		m_Animator.Play("Play");
		m_Animator.ResetTrigger("IsExplode");
	}

	// 加载子弹数据
	public void Load(int bulletID, int level, Rect mapRect)
	{
		MapRect = mapRect;

		this.ID = bulletID;
		this.Level = level;

		BulletInfo bulletInfo = StaticData.GetInstance().GetBulletInfo(bulletID);
		this.BaseSpeed = bulletInfo.BaseSpeed;
		this.BaseAttack = bulletInfo.BaseAttack;
	}

	// 子弹爆炸
	public void Explode()
	{
		// 标记已爆炸
		m_IsExploded = true;
		// 播放爆炸动画
		m_Animator.SetTrigger("IsExplode");
		// 延迟回收
		StartCoroutine("DestoryCoroutine");
	}

	IEnumerator DestoryCoroutine()
	{
		// 延迟
		yield return new WaitForSeconds(DelayToDestory);

		// 回收
		PoolMgr.GetInstance().PushObj(this.gameObject);
	}
	#endregion

	#region Unity回调
	protected virtual void Awake()
	{
		m_Animator = GetComponent<Animator>();
	}

	protected virtual void Update()
	{

	}
	#endregion

	#region 事件回调
	public override void OnGetObj()
	{
		
	}

	public override void OnPushObj()
	{
		InitBullet();
	}
	#endregion

	#region 帮助方法
	#endregion

}
