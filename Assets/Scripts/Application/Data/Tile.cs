using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 格子信息
public class Tile
{
    public int X;
    public int Y;

	// 标识该格是否可以放置塔
	public bool CanHold;

	// 该格子保存的数据
	public object Data;

	public Tile(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	public override string ToString()
	{
		return string.Format("[X:{0},Y:{1},CanHold:{2}]", this.X, this.Y, this.CanHold);
	}
}
