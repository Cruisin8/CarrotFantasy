using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountDown : View
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	public Image Count;
	public Sprite[] Numbers;
	#endregion

	#region 属性
	public override string Name {
		get { return Consts.V_CountDown; }
	}
	#endregion

	#region 方法
	public void Show()
	{
		this.gameObject.SetActive(true);
	}

	public void Hide()
	{
		this.gameObject.SetActive(false);
	}

	public void StartCountDown()
	{
		Show();
		StartCoroutine("DisplayCount");
	}

	// 倒计时动画
	IEnumerator DisplayCount()
	{
		int count = 3;
		while (count > 0) {
			// 显示
			Count.sprite = Numbers[count - 1];

			count--;

			yield return new WaitForSeconds(1f);

			if(count <= 0) {
				break;
			}
		}

		// 隐藏倒计时界面
		Hide();

		// 倒计时结束
		SendEvent(Consts.E_CountDownComplete);

	}
	#endregion

	#region Unity回调

	#endregion

	#region 事件回调
	public override void RegisterEvents()
	{
		// 关注E_EnterScene进入场景事件
		this.AttentionEvents.Add(Consts.E_EnterScene);
	}

	public override void HandleEvent(string eventName, object data)
	{
		// 进入场景3时开启倒计时
		switch (eventName) {
			case Consts.E_EnterScene:
				SceneArgs e = (SceneArgs)data;
				if (e.SceneIndex == 3) {
					StartCountDown();
				}
				break;
		}
	}
	#endregion

	#region 帮助方法
	#endregion

}
