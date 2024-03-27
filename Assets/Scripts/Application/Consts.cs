using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Consts
{
    // 关卡目录
    public static readonly string LevelDir = Application.dataPath + @"/Resources/CarrotFantasy/Levels";

	// 地图路径
	public static readonly string MapDir = Application.dataPath + @"/Resources/CarrotFantasy/Maps";

	// 地图卡片目录
	public static readonly string CardDir = Application.dataPath + @"/Resources/CarrotFantasy/Cards";

	// 炮塔图标目录
	public static readonly string RolesDir = "CarrotFantasy/Roles/";

	// 怪物预制体目录
	public static readonly string PrefabsDir = "Prefabs/";

	// 存档
	public const string GameProgress = "GameProgress";

	// 最接近距离（视为相等）
	public const float DotClosedDistance = 0.1f;	// 点攻击最小距离
	public const float RangeClosedDistance = 0.7f;	// 范围攻击最小距离

	// Model
	public const string M_GameModel = "M_GameModel";
	public const string M_RoundModel = "M_RoundModel";

	// View
	public const string V_Start = "V_Start";
	public const string V_Select = "V_Select";
	public const string V_Board = "V_Board";
	public const string V_CountDown = "V_CountDown";
	public const string V_Win = "V_Win";
	public const string V_Lost = "V_Lost";
	public const string V_System = "V_System";
	public const string V_Complete = "V_Complete";
	public const string V_Spawner = "V_Spawner";
	public const string V_SpawnPopup = "V_SpawnPopup";
	public const string V_TowerPopup = "V_TowerPopup";

	// Controller
	public const string E_StartUp = "E_StartUp";

	public const string E_EnterScene = "E_EnterScene";	// SceneArgs
	public const string E_ExitScene = "E_ExitScene";    // SceneArgs

	public const string E_StartLevel = "E_StartLevel";  // StartLevelArgs
	public const string E_EndLevel = "E_EndLevel";		// EndLevelArgs

	public const string E_CountDownComplete = "E_CountDownComplete";


	public const string E_StartRound = "E_StartRound";		// StartRoundArgs
	public const string E_SpawnMonster = "E_SpawnMonster";	// SpawnMonsterArgs

	public const string E_ShowSpawnPanel = "E_ShowSpawnPanel";		// ShowSpawnPanelArgs
	public const string E_ShowUpgradePanel = "E_ShowUpgradePanel";	// ShowUpgradePanelArgs
	public const string E_HidePopups = "E_HidePopups";

	public const string E_SpawnTower = "E_SpawnTower";		// SpawnTowerArgs
	public const string E_UpgradeTower = "E_UpgradeTower";	// UpgradeTowerArgs
	public const string E_SellTower = "E_SellTower";		// SellTowerArgs

}

public enum GameSpeed
{
	Zero,
	One,
	Two
}

public enum MonsterType
{
	Monster0, 
	Monster1, 
	Monster2, 
	Monster3, 
	Monster4, 
	Monster5
}

public enum SceneID
{
	Init,
	Start,
	Select,
	Level,
	Complete
}

