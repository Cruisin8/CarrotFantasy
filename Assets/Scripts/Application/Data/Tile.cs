using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ϣ
public class Tile
{
    public int X;
    public int Y;

	// ��ʶ�ø��Ƿ���Է�����
	public bool CanHold;

	// �ø��ӱ��������
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
