using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ʱͼƬ�еĻ�����ת����
public class Rotate : MonoBehaviour
{
	public float Speed = 360; // (��/��)

	void Update()
	{
		transform.Rotate(Vector3.forward, Time.deltaTime * Speed, Space.Self);
	}
}
