using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于测试InputMgr 输入控制功能
/// </summary>
public class InputTest : MonoBehaviour
{
	private void Start()
	{
		InputMgr.GetInstance().EnableInputCheck();

		EventCenter.GetInstance().AddEventListener<KeyCode>("某键按下", CheckInputDown);
		EventCenter.GetInstance().AddEventListener<KeyCode>("某键抬起", CheckInputUp);
	}

	// 按下键位
	private void CheckInputDown(KeyCode key)
	{
		KeyCode keyCode = (KeyCode)key;
		switch(keyCode) {
			case KeyCode.W:
				Debug.Log("前进");
				break;
			case KeyCode.A:
				Debug.Log("左转");
				break;
			case KeyCode.S:
				Debug.Log("后退");
				break;
			case KeyCode.D:
				Debug.Log("右转");
				break;
			case KeyCode.Space:
				// 未在InputMgr中分发，不会触发
				Debug.Log("跳跃");
				break;
		}
	}

	// 抬起键位
	private void CheckInputUp(KeyCode key)
	{
		KeyCode keyCode = (KeyCode)key;
		switch (keyCode) {
			case KeyCode.W:
				Debug.Log("停止前进");
				break;
			case KeyCode.A:
				Debug.Log("停止左转");
				break;
			case KeyCode.S:
				Debug.Log("停止后退");
				break;
			case KeyCode.D:
				Debug.Log("停止右转");
				break;
		}
	}
}
