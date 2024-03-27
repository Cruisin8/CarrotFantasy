using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
	public float duration = 4f; // 动画持续时间
	public float radius = 20f; // 环绕半径

	void Start()
	{
		BirdCircleMove();
	}

	// 小鸟绕圈移动动画
	private void BirdCircleMove()
	{
		Vector3 centerPosition = transform.position + new Vector3(-radius, 0f, 0f); // 计算环绕圆心的位置

		Vector3[] path = new Vector3[60]; // 定义路径点，这里以60个点来确保路径闭合
		float angle = 0f;
		for (int i = 0; i < 60; i++) {
			float x = centerPosition.x + Mathf.Cos(Mathf.Deg2Rad * angle) * radius; // 角度转弧度
			float y = centerPosition.y + Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
			path[i] = new Vector3(x, y, centerPosition.z);
			angle -= 360f / 60f; // 顺时针绕圈
		}

		transform.DOPath(path, duration, PathType.CatmullRom).SetEase(Ease.Linear).SetLoops(-1); // 以Catmull-Rom样条曲线方式循环绕圈
	}
}
