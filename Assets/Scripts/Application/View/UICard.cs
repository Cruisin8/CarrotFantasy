using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ѡ�ص�ͼ��ƬUI
public class UICard : MonoBehaviour, IPointerDownHandler
{
	#region ����
	#endregion

	#region �¼�
	// �ؿ�ͼƬ����¼�
	public event Action<Card> OnClick;
	#endregion

	#region �ֶ�
	// ��ͼͼƬ
	public Image ImgCard;
	// ����ͼƬ
	public Image ImgLock;

	// ��ͼ��Ƭ����
	Card m_Card = null;

	// �Ƿ��͸����ѡ�е�ͼ������
	bool m_IsTransparent = false;
	#endregion

	#region ����
	#endregion

	#region ����
	// �Ƿ��͸��
	public bool IsTransparent {
		get { return m_IsTransparent; }
		set {
			m_IsTransparent = value;

			Image[] images = new Image[] { ImgCard, ImgLock };
			foreach (Image img in images) {
				Color c = img.color;
				c.a = value ? 0.5f : 1f;
				img.color = c;
			}
		}
	}

	// �󶨹ؿ�ͼƬ
	public void DataBind(Card card)
	{
		//����
		m_Card = card;

		//���عؿ�ͼƬ
		string cardFile = "file://" + Consts.CardDir + "\\" + m_Card.CardImage;
		StartCoroutine(Tools.LoadImage(cardFile, ImgCard));

		//�Ƿ�����
		ImgLock.gameObject.SetActive(card.IsLocked);
	}

	// ����ؿ�ͼƬ�л��ؿ�
	public void OnPointerDown(PointerEventData eventData)
	{
		if (OnClick != null) {
			OnClick(m_Card);
		}
	}
	#endregion

	#region Unity�ص�
	// �л���������ʱȡ�������¼�
	void OnDestroy()
	{
		while (OnClick != null) {
			OnClick -= OnClick;
		}
	}
	#endregion

	#region �¼��ص�
	#endregion

	#region ��������
	#endregion
}
