using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回合
public class Round
{
	// 怪物类型ID
    public int Monster;
	// 怪物数量
    public int Count;

    public Round(int monster, int count)
	{
		this.Monster = monster;
		this.Count = count;
	}
}
