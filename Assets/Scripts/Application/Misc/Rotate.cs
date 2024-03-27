using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 倒计时图片中的火焰旋转动画
public class Rotate : MonoBehaviour
{
	public float Speed = 360; // (度/秒)

	void Update()
	{
		transform.Rotate(Vector3.forward, Time.deltaTime * Speed, Space.Self);
	}
}
