using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
	public float duration = 4f; // ��������ʱ��
	public float radius = 20f; // ���ư뾶

	void Start()
	{
		BirdCircleMove();
	}

	// С����Ȧ�ƶ�����
	private void BirdCircleMove()
	{
		Vector3 centerPosition = transform.position + new Vector3(-radius, 0f, 0f); // ���㻷��Բ�ĵ�λ��

		Vector3[] path = new Vector3[60]; // ����·���㣬������60������ȷ��·���պ�
		float angle = 0f;
		for (int i = 0; i < 60; i++) {
			float x = centerPosition.x + Mathf.Cos(Mathf.Deg2Rad * angle) * radius; // �Ƕ�ת����
			float y = centerPosition.y + Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
			path[i] = new Vector3(x, y, centerPosition.z);
			angle -= 360f / 60f; // ˳ʱ����Ȧ
		}

		transform.DOPath(path, duration, PathType.CatmullRom).SetEase(Ease.Linear).SetLoops(-1); // ��Catmull-Rom�������߷�ʽѭ����Ȧ
	}
}
