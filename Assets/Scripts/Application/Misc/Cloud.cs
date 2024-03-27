using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	public float duration = 20f; // 动画持续时间
	public float Offset = 1300f; // 云X的移动距离

	void Start()
	{
		CloudStraightMove();
	}

	// 云朵直线移动动画
	private void CloudStraightMove()
	{
		Vector3 endPosition = transform.position + new Vector3(Offset, 0f, 0f); // 计算终点的位置

		transform.DOMove(endPosition, duration).SetLoops(-1, LoopType.Restart);
	}
}
