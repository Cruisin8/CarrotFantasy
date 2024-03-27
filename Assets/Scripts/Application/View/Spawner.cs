using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// 生产游戏角色
public class Spawner : View
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	Map m_Map = null;
	Luobo m_Luobo = null;
	#endregion

	#region 属性
	public override string Name {
		get { return Consts.V_Spawner; }
	}
	#endregion

	#region 方法
	// 产生萝卜
	void SpawnLuobo(Vector3 position)
	{
		string prefabName = Consts.PrefabsDir + "Luobo";
		PoolMgr.GetInstance().GetObj(prefabName, (obj) => {
			m_Luobo = obj.GetComponent<Luobo>();
			m_Luobo.Dead += luobo_Dead;
			m_Luobo.transform.position = position;
		});
	}

	// 产生怪物
	void SpawnMonster(int monsterType)
	{
		// 从对象池获取怪物
		string prefabName = Consts.PrefabsDir + "Monster" + monsterType;
		PoolMgr.GetInstance().GetObj(prefabName, (obj) => {
			Monster monster = obj.GetComponent<Monster>();
			monster.Reached += monster_Reached;
			monster.HpChanged += monster_HpChanged;
			monster.Dead += monster_Dead;
			monster.Load(m_Map.Path);
		});
	}

	void SpawnTower(int towerID, Vector3 position)
	{
		TowerInfo towerInfo = StaticData.GetInstance().GetTowerInfo(towerID);
		Tile tile = m_Map.GetTile(position);

		PoolMgr.GetInstance().GetObj(towerInfo.PrefabName, (obj) => {
			Tower tower = obj.GetComponent<Tower>();
			tower.transform.position = position;
			tower.Load(towerID, tile, m_Map.MapRect);
			//设置Tile数据
			tile.Data = tower;
		});
	}
	#endregion

	#region Unity回调
	#endregion

	#region 事件回调
	void luobo_Dead(Role luobo)
	{
		// 回收萝卜
		PoolMgr.GetInstance().PushObj(luobo.gameObject);

		// 萝卜死亡，游戏失败
		GameModel gModel = GetModel<GameModel>();
		SendEvent(Consts.E_EndLevel, new EndLevelArgs() { LevelID = gModel.PlayLevelID, IsWin = false });
	}

	void monster_HpChanged(int hp, int maxHp)
	{

	}

	void monster_Dead(Role monster)
	{
		// 回收死亡的怪物
		PoolMgr.GetInstance().PushObj(monster.gameObject);

		GameModel gModel = GetModel<GameModel>();
		RoundModel rModel = GetModel<RoundModel>();
		GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
		// 胜利条件：所有怪都出完、萝卜未死亡、所有怪物已出完
		if (rModel.IsAllRoundComplete && !m_Luobo.IsDead && monsters.Length <= 0) {
			SendEvent(Consts.E_EndLevel, new EndLevelArgs() { LevelID = gModel.PlayLevelID, IsWin = true });
		}
	}

	void monster_Reached(Monster monster)
	{
		// 萝卜掉血
		m_Luobo.Damage(1);  // 测试 固定掉1血

		// 怪物死亡
		monster.Hp = 0;
	}

	// 点击地图格子事件
	private void map_OnTileClick(object sender, TileClickEventArgs e)
	{
		GameModel gModel = GetModel<GameModel>();

		//游戏还未开始，那么不操作菜单
		if (!gModel.IsPlaying) {
			return;
		}

		//如果有菜单显示，那么隐藏菜单
		if (TowerPopup.Instance.IsPopShow) {
			SendEvent(Consts.E_HidePopups);
			return;
		}

		// 非放塔格子，不操作菜单
		if(!e.Tile.CanHold) {
			SendEvent(Consts.E_HidePopups);
			return;
		}

		if(e.Tile.Data == null) {
			// 格子为空，则显示放置炮塔界面
			ShowSpawnPanelArgs e1 = new ShowSpawnPanelArgs() {
				Position = m_Map.GetPosition(e.Tile),
				// 下方的格子面板显示在上面，上方的显示在下面（防止面板显示出界）
				IsUpSide = e.Tile.Y < (Map.RowCount / 2)  
			};
			SendEvent(Consts.E_ShowSpawnPanel, e1);
		}
		else {
			// 格子不为空时，则视为已存在炮塔，显示升级炮塔界面
			ShowUpgradePanelArgs e2 = new ShowUpgradePanelArgs() {
				Tower = e.Tile.Data as Tower
			};
			SendEvent(Consts.E_ShowUpgradePanel, e2);
		}
	}

	public override void RegisterEvents()
	{
		AttentionEvents.Add(Consts.E_EnterScene);
		AttentionEvents.Add(Consts.E_SpawnMonster);
		AttentionEvents.Add(Consts.E_SpawnTower);
		
	}

	public override void HandleEvent(string eventName, object data)
	{
		switch(eventName) {
			case Consts.E_EnterScene:
				SceneArgs e1 = (SceneArgs)data;
				if(e1.SceneIndex == 3) {
					// 获取地图组件
					m_Map = GetComponent<Map>();
					m_Map.OnTileClick += map_OnTileClick;

					// 加载地图
					GameModel gModel = GetModel<GameModel>();
					m_Map.LoadLevel(gModel.PlayLevel);

					// 加载萝卜
					Vector3[] path = m_Map.Path;
					Vector3 pos = path[path.Length - 1];
					SpawnLuobo(pos);
				}
				break;
			case Consts.E_SpawnMonster:
				SpawnMonsterArgs e2 = (SpawnMonsterArgs)data;
				SpawnMonster(e2.MonsterType);
				break;
			case Consts.E_SpawnTower:
				SpawnTowerArgs e3 = (SpawnTowerArgs)data;
				SpawnTower(e3.TowerID, e3.Position);
				break;
		}
	}
	#endregion

	#region 帮助方法
	#endregion
}
