using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// ������Ϸ��ɫ
public class Spawner : View
{
	#region ����
	#endregion

	#region �¼�
	#endregion

	#region �ֶ�
	Map m_Map = null;
	Luobo m_Luobo = null;
	#endregion

	#region ����
	public override string Name {
		get { return Consts.V_Spawner; }
	}
	#endregion

	#region ����
	// �����ܲ�
	void SpawnLuobo(Vector3 position)
	{
		string prefabName = Consts.PrefabsDir + "Luobo";
		PoolMgr.GetInstance().GetObj(prefabName, (obj) => {
			m_Luobo = obj.GetComponent<Luobo>();
			m_Luobo.Dead += luobo_Dead;
			m_Luobo.transform.position = position;
		});
	}

	// ��������
	void SpawnMonster(int monsterType)
	{
		// �Ӷ���ػ�ȡ����
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
			//����Tile����
			tile.Data = tower;
		});
	}
	#endregion

	#region Unity�ص�
	#endregion

	#region �¼��ص�
	void luobo_Dead(Role luobo)
	{
		// �����ܲ�
		PoolMgr.GetInstance().PushObj(luobo.gameObject);

		// �ܲ���������Ϸʧ��
		GameModel gModel = GetModel<GameModel>();
		SendEvent(Consts.E_EndLevel, new EndLevelArgs() { LevelID = gModel.PlayLevelID, IsWin = false });
	}

	void monster_HpChanged(int hp, int maxHp)
	{

	}

	void monster_Dead(Role monster)
	{
		// ���������Ĺ���
		PoolMgr.GetInstance().PushObj(monster.gameObject);

		GameModel gModel = GetModel<GameModel>();
		RoundModel rModel = GetModel<RoundModel>();
		GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
		// ʤ�����������йֶ����ꡢ�ܲ�δ���������й����ѳ���
		if (rModel.IsAllRoundComplete && !m_Luobo.IsDead && monsters.Length <= 0) {
			SendEvent(Consts.E_EndLevel, new EndLevelArgs() { LevelID = gModel.PlayLevelID, IsWin = true });
		}
	}

	void monster_Reached(Monster monster)
	{
		// �ܲ���Ѫ
		m_Luobo.Damage(1);  // ���� �̶���1Ѫ

		// ��������
		monster.Hp = 0;
	}

	// �����ͼ�����¼�
	private void map_OnTileClick(object sender, TileClickEventArgs e)
	{
		GameModel gModel = GetModel<GameModel>();

		//��Ϸ��δ��ʼ����ô�������˵�
		if (!gModel.IsPlaying) {
			return;
		}

		//����в˵���ʾ����ô���ز˵�
		if (TowerPopup.Instance.IsPopShow) {
			SendEvent(Consts.E_HidePopups);
			return;
		}

		// �Ƿ������ӣ��������˵�
		if(!e.Tile.CanHold) {
			SendEvent(Consts.E_HidePopups);
			return;
		}

		if(e.Tile.Data == null) {
			// ����Ϊ�գ�����ʾ������������
			ShowSpawnPanelArgs e1 = new ShowSpawnPanelArgs() {
				Position = m_Map.GetPosition(e.Tile),
				// �·��ĸ��������ʾ�����棬�Ϸ�����ʾ�����棨��ֹ�����ʾ���磩
				IsUpSide = e.Tile.Y < (Map.RowCount / 2)  
			};
			SendEvent(Consts.E_ShowSpawnPanel, e1);
		}
		else {
			// ���Ӳ�Ϊ��ʱ������Ϊ�Ѵ�����������ʾ������������
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
					// ��ȡ��ͼ���
					m_Map = GetComponent<Map>();
					m_Map.OnTileClick += map_OnTileClick;

					// ���ص�ͼ
					GameModel gModel = GetModel<GameModel>();
					m_Map.LoadLevel(gModel.PlayLevel);

					// �����ܲ�
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

	#region ��������
	#endregion
}
