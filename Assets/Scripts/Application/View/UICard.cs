using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 选关地图卡片UI
public class UICard : MonoBehaviour, IPointerDownHandler
{
	#region 常量
	#endregion

	#region 事件
	// 关卡图片点击事件
	public event Action<Card> OnClick;
	#endregion

	#region 字段
	// 地图图片
	public Image ImgCard;
	// 锁定图片
	public Image ImgLock;

	// 地图卡片属性
	Card m_Card = null;

	// 是否半透明（选中地图高亮）
	bool m_IsTransparent = false;
	#endregion

	#region 属性
	#endregion

	#region 方法
	// 是否半透明
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

	// 绑定关卡图片
	public void DataBind(Card card)
	{
		//保存
		m_Card = card;

		//加载关卡图片
		string cardFile = "file://" + Consts.CardDir + "\\" + m_Card.CardImage;
		StartCoroutine(Tools.LoadImage(cardFile, ImgCard));

		//是否锁定
		ImgLock.gameObject.SetActive(card.IsLocked);
	}

	// 点击关卡图片切换关卡
	public void OnPointerDown(PointerEventData eventData)
	{
		if (OnClick != null) {
			OnClick(m_Card);
		}
	}
	#endregion

	#region Unity回调
	// 切换场景销毁时取消监听事件
	void OnDestroy()
	{
		while (OnClick != null) {
			OnClick -= OnClick;
		}
	}
	#endregion

	#region 事件回调
	#endregion

	#region 帮助方法
	#endregion
}
