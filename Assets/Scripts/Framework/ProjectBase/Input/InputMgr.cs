using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 输入控制中心模块
/// </summary>
public class InputMgr : BaseManager<InputMgr>
{
	// 输入检测是否开启
	private bool isInputCheckEnable = false;

	// 键盘映射关系 可以用于提供给玩家修改键位
	private Dictionary<string, KeyCode> dicKey = new Dictionary<string, KeyCode>();

    // 构造函数中，添加Update监听
    public InputMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(MyUpdate);
    }

	// 开启输入检测
	public void EnableInputCheck()
	{
		isInputCheckEnable = true;
	}

	// 关闭输入检测
	public void DisableInputCheck()
	{
		isInputCheckEnable = false;
	}

	// 查询输入检测开启状态
	public bool IsInputCheckEnabled()
	{
		return isInputCheckEnable;
	}

	// 检测按键抬起按下分发事件
	private void CheckKeyCode(KeyCode key)
	{
		if (Input.GetKeyDown(key)) {
			// 事件中心模块分发按下事件
			EventCenter.GetInstance().EventTrigger("某键按下", key);
		}

		if (Input.GetKeyUp(key)) {
			// 事件中心模块分发抬起事件
			EventCenter.GetInstance().EventTrigger("某键抬起", key);
		}
	}

    private void MyUpdate()
    {
		// 没有开启输入检测，则直接返回
		if(!IsInputCheckEnabled()) {
			return;
		}

		CheckKeyCode(KeyCode.W);
		CheckKeyCode(KeyCode.S);
		CheckKeyCode(KeyCode.A);
		CheckKeyCode(KeyCode.D);
	}
}
