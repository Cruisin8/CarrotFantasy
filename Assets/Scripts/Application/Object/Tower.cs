using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Tower : ReusbleObject, IReusable
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	protected Animator m_Animator;	// 动画组件
	Monster m_Target;		// 攻击目标
	Tile m_Tile;            // 所在格子
	float m_LastAttackTime;	// 上次攻击时间
	int m_Level = 0;        // 等级
	#endregion

	#region 属性
	// ID
	public int ID { get; private set; }
	// 当前级别
	public int Level {
		get {
			return m_Level;
		}
		set {
			m_Level = Mathf.Clamp(value, 0, MaxLevel);
			// 级别越高，炮塔图片越大
			transform.localScale = Vector3.one * (1 + m_Level * 0.25f);
		}
	}
	// 最大级别
	public int MaxLevel { get; private set; }
	// 是否为最高级别
	public bool IsTopLevel { get { return Level >= MaxLevel; } }
	// 当前价格
	public int Price { get { return BasePrice * Level; } }
	// 基础价格
	public int BasePrice { get; private set; }
	// 攻击范围
	public float GuardRange { get; private set; }
	// 攻速（平均1秒发射的子弹数量）
	public float ShootRate { get; private set; }
	// 使用的子弹ID
	public int UseBulletID { get; private set; }
	// 地图边界
	public Rect MapRect { get; private set; }
	#endregion

	#region 方法
	// 初始化炮塔数据
	void InitTower()
	{
		m_Animator.ResetTrigger("IsAttack");
		m_Animator.Play("Idle");
		m_Animator = null;
		m_Target = null;
		ID = -1;
		Level = 0;
		MaxLevel = 0;
		BasePrice = 0;
		GuardRange = 0;
		UseBulletID = 0;
	}

	// 加载炮塔数据
	public void Load(int towerID, Tile tile, Rect mapRect)
	{
		TowerInfo towerInfo = StaticData.GetInstance().GetTowerInfo(towerID);
		this.ID = towerInfo.ID;
		this.MaxLevel = towerInfo.MaxLevel;
		this.BasePrice = towerInfo.BasePrice;
		this.GuardRange = towerInfo.GuardRange;
		this.ShootRate = towerInfo.ShootRate;
		this.UseBulletID = towerInfo.UseBulletID;
		this.Level = 1;
		MapRect = mapRect;
	}

	// 炮塔攻击
	protected virtual void Attack(Monster monster)
	{
		// 动画
		m_Animator.SetTrigger("IsAttack");
	}

	// 炮塔朝向锁定目标
	void LookAt()
	{
		Vector3 eulerAngles = transform.eulerAngles;

		if (m_Target == null) {
			// 没有目标时，还原朝向
			eulerAngles.z = 0;
			transform.eulerAngles = eulerAngles;
		}
		else {
			// 计算两点距离
			Vector3 dir = (m_Target.transform.position - transform.position).normalized;
			float dy = dir.y;
			float dx = dir.x;

			// 计算炮塔和目标的夹角（Atan2方法可以将tan的数值扩展到180°，防止出现tan90度的情况）
			float angles = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg; // 弧度值[-180°,180°]，再转为角度值

			// 修改炮塔旋转角度
			eulerAngles.z = angles - 90f;   // -90f为了让炮塔保持垂直图像，防止tan0°时炮塔图像变为横向
			transform.eulerAngles = eulerAngles;
		}
	}
	#endregion

	#region Unity回调
	protected virtual void Awake()
	{
		m_Animator = GetComponent<Animator>();
	}

	private void Update()
	{
		// 炮塔盯着目标
		LookAt();

		if (m_Target == null) {
			// 搜索目标
			GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
			foreach(GameObject monster in monsters) {
				Monster m = monster.GetComponent<Monster>();
				float dis = Vector3.Distance(m.transform.position, transform.position);
				if (!m.IsDead && dis <= GuardRange) {
					// 找到目标就退出
					m_Target = m;
					break;
				}
			}
		}
		else {
			// 攻击目标
			float dis = Vector3.Distance(m_Target.transform.position, transform.position);
			if (m_Target.IsDead || dis > GuardRange) {
				// 超出攻击范围
				m_Target = null;
				m_LastAttackTime = 0;
				return;
			}

			// 计算攻击时间点
			float attackTime = m_LastAttackTime + 1f / ShootRate;
			if(Time.time >= attackTime) {
				// 攻击
				Attack(m_Target);

				// 记录攻击时间
				m_LastAttackTime = Time.time;
			}
		}


	}
	#endregion

	#region 事件回调
	public override void OnGetObj()
	{

	}

	public override void OnPushObj()
	{
		InitTower();
	}
	#endregion

	#region 帮助方法
	#endregion
}
