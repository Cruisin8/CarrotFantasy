using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于测试事件中心：
/// 怪兽死亡TestMonster→玩家获取奖励Player、任务记录Task、其他Other
/// </summary>
public class TestMonster : MonoBehaviour
{
    // 怪物类型
    public int monsterType = 1;
    // 怪物姓名
    public string monsterName = "史莱姆";

	private void Start()
	{
        // 测试事件中心
        Dead();	
	}

    // 怪兽死亡
	void Dead()
    {
        Debug.Log("怪物死亡");
        // 其他对象在怪物死亡后做的事（不使用事件中心）：
        // 1.玩家 获得奖励
        //GameObject.Find("Player").GetComponent<Player>().PlayerWaitMonsterDeadDo();
        // 2.任务记录
        //GameObject.Find("Task").GetComponent<Task>().TaskWaitMonsterDeadDo();
        // 3.其他事件，比如成就记录、副本继续创建怪物等
        //GameObject.Find("Other").GetComponent<Other>().OtherWaitMonsterDeadDo();

        // 使用事件中心：
        // 触发事件
        EventCenter.GetInstance().EventTrigger("MonsterDead", this);

    }
}
