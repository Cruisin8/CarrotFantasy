using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : SingletonAutoMono<StaticData>
{
    Dictionary<int, LuoboInfo> m_Luobos = new Dictionary<int, LuoboInfo>();
    Dictionary<int, MonsterInfo> m_Monsters = new Dictionary<int, MonsterInfo>();
    Dictionary<int, TowerInfo> m_Towers = new Dictionary<int, TowerInfo>();
    Dictionary<int, BulletInfo> m_Bullets = new Dictionary<int, BulletInfo>();

	private void Awake()
	{
		InitLuobos();
		InitMonsters();
		InitTowers();
		InitBullets();
	}

	void InitLuobos()
	{
		m_Luobos.Add(0, new LuoboInfo() { ID = 0, Hp = 5 });
	}

	void InitMonsters()
	{
		
		// 各种怪物类型数据 Todo(后续要改为从excel中读取)
		m_Monsters.Add(0, new MonsterInfo() { ID = 0, Hp = 1, MoveSpeed = 1.5f});
		m_Monsters.Add(1, new MonsterInfo() { ID = 1, Hp = 2, MoveSpeed = 1f });
		m_Monsters.Add(2, new MonsterInfo() { ID = 2, Hp = 5, MoveSpeed = 1f });
		m_Monsters.Add(3, new MonsterInfo() { ID = 3, Hp = 10, MoveSpeed = 1f });
		m_Monsters.Add(4, new MonsterInfo() { ID = 4, Hp = 10, MoveSpeed = 1f });
		m_Monsters.Add(5, new MonsterInfo() { ID = 5, Hp = 100, MoveSpeed = 0.5f });

		/*
		m_Monsters.Add(0, new MonsterInfo() { ID = 0, Hp = 1, MoveSpeed = 5f });
		m_Monsters.Add(1, new MonsterInfo() { ID = 1, Hp = 2, MoveSpeed = 5f });
		m_Monsters.Add(2, new MonsterInfo() { ID = 2, Hp = 5, MoveSpeed = 5f });
		m_Monsters.Add(3, new MonsterInfo() { ID = 3, Hp = 10, MoveSpeed = 5f });
		m_Monsters.Add(4, new MonsterInfo() { ID = 4, Hp = 10, MoveSpeed = 5f });
		m_Monsters.Add(5, new MonsterInfo() { ID = 5, Hp = 100, MoveSpeed = 5f });
		*/
	}

	void InitTowers()
	{
		m_Towers.Add(0, new TowerInfo() { ID = 0, PrefabName = "Prefabs/Bottle", NormalIcon = "Bottle/Bottle01", DisabledIcon = "Bottle/Bottle00", MaxLevel = 3, BasePrice = 1, ShootRate = 2, GuardRange = 3f, UseBulletID = 0 });
		m_Towers.Add(1, new TowerInfo() { ID = 1, PrefabName = "Prefabs/Fan", NormalIcon = "Fan/Fan01", DisabledIcon = "Fan/Fan00", MaxLevel = 3, BasePrice = 2, ShootRate = 1, GuardRange = 3f, UseBulletID = 1 });
	}

	void InitBullets()
	{
		m_Bullets.Add(0, new BulletInfo() { ID = 0, PrefabName = "Prefabs/BottleBullet1", BaseSpeed = 5f, BaseAttack = 1 });
		m_Bullets.Add(1, new BulletInfo() { ID = 1, PrefabName = "Prefabs/FanBullet1", BaseSpeed = 2f, BaseAttack = 1 });
	}

	public LuoboInfo GetLuoboInfo()
	{
		return m_Luobos[0];
	}

	public MonsterInfo GetMonsterInfo(int monsterType)
	{
		return m_Monsters[monsterType];
	}

	public TowerInfo GetTowerInfo(int towerType)
	{
		return m_Towers[towerType];
	}

	public BulletInfo GetBulletInfo(int bulletType)
	{
		return m_Bullets[bulletType];
	}
}
