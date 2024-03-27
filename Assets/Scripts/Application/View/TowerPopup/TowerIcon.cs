using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIcon : MonoBehaviour
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	SpriteRenderer m_Render;		// 炮塔图片
	TowerInfo m_TowerInfo;			// 炮塔信息
	Vector3 m_Position;				// 建造位置
	bool m_IsGoldEnough = false;	// 金币是否足够购买
	#endregion

	#region 属性
	#endregion

	#region 方法
	// 加载
	// isUpSide 炮塔图标是否要显示在上方
	public void Load(GameModel gModel, TowerInfo towerInfo, Vector3 position, bool isUpSide)
	{
		// 保存数据
		m_TowerInfo = towerInfo;
		m_Position = position;

		// 判断金币是否足够
		//m_IsGoldEnough = gModel.Gold >= towerInfo.BasePrice;
		m_IsGoldEnough = true;	// 测试用，忽略金币

		// 加载炮塔图片
		string path = Consts.RolesDir + (m_IsGoldEnough ? towerInfo.NormalIcon : towerInfo.DisabledIcon);
		m_Render.sprite = ResMgr.GetInstance().Load<Sprite>(path);

		// 购买图标显示位置（在中线上方则显示在下面，下方则显示在上面）
		Vector3 localPos = transform.localPosition;
		localPos.y = isUpSide ? Mathf.Abs(localPos.y) : - Mathf.Abs(localPos.y);
		transform.localPosition = localPos;
	}
	#endregion

	#region Unity回调
	void Awake()
	{
		m_Render = GetComponent<SpriteRenderer>();
	}

	private void OnMouseDown()
	{
		// 金币是否足够
		if (!m_IsGoldEnough) {
			return;
		}

		// 创建塔的类型 TowerID
		int towerID = m_TowerInfo.ID;

		// 创建位置
		Vector3 position = m_Position;

		// 参数
		object[] args = { towerID, position };

		// 把消息冒泡向上传递给TowerPopup
		SendMessageUpwards("OnSpawnTower", args, SendMessageOptions.RequireReceiver);
	}
	#endregion

	#region 事件回调
	#endregion

	#region 帮助方法
	#endregion
}
