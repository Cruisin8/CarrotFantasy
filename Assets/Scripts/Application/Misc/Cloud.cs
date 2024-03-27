using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	public float duration = 20f; // ��������ʱ��
	public float Offset = 1300f; // ��X���ƶ�����

	void Start()
	{
		CloudStraightMove();
	}

	// �ƶ�ֱ���ƶ�����
	private void CloudStraightMove()
	{
		Vector3 endPosition = transform.position + new Vector3(Offset, 0f, 0f); // �����յ��λ��

		transform.DOMove(endPosition, duration).SetLoops(-1, LoopType.Restart);
	}
}
