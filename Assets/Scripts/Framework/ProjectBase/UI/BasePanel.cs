using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 面板panel基类
/// 可以通过代码快速找到所有子控件，方便在子类中处理逻辑
/// </summary>
public class BasePanel : MonoBehaviour
{
	// 通过里式转换原则 来存储所有控件
	// 用List存储，是因为会有一个控件上同时存在Button和Image的情况
	private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

	protected virtual void Awake()
	{
		FindChildrenControl<Button>();
		FindChildrenControl<Image>();
		FindChildrenControl<Text>();
		FindChildrenControl<Toggle>();
		FindChildrenControl<Slider>();
		FindChildrenControl<ScrollRect>();
		FindChildrenControl<InputField>();
	}

	// 显示自己
	public virtual void ShowMe() { }

	// 隐藏自己
	public virtual void HideMe() { }

	// 面板上某个按钮点击的响应事件（权限为protected 只有子类可以调用）
	protected virtual void OnClick(string btnName) { }

	// 面板上某个单选框改变的响应事件
	protected virtual void OnValueChanged(string toggleName, bool value) { }

	// 得到对应名字的对应控件脚本
	//		返回该控件上T类型的控件
	//		比如：GetControl<Button>返回的就是该控件上的Button
	//			  GetControl<Image>返回的就是该控件上的Image
	protected T GetControl<T>(string controlName) where T : UIBehaviour
	{
		if (controlDic.ContainsKey(controlName)) {
			for (int i = 0; i < controlDic[controlName].Count; i++) {
				if (controlDic[controlName][i] is T) {
					return controlDic[controlName][i] as T;
				}
			}
		}

		return null;
	}

	// 找到子对象的对应控件
	private void FindChildrenControl<T>() where T : UIBehaviour
	{
		T[] controls = this.GetComponentsInChildren<T>();

		for (int i = 0; i < controls.Length; i++) {
			// 获取控件名
			string objName = controls[i].gameObject.name;
			if (controlDic.ContainsKey(objName)) {
				controlDic[objName].Add(controls[i]);
			}
			else {
				controlDic.Add(objName, new List<UIBehaviour>() { controls[i] });
			}

			// Base父类自动监听所有子类控件
			if (controls[i] is Button) {
				// 按钮控件
				(controls[i] as Button).onClick.AddListener(() => {
					OnClick(objName);
				});
			} else if (controls[i] is Toggle) {
				// 单选框控件
				(controls[i] as Toggle).onValueChanged.AddListener((value) => {
					OnValueChanged(objName, value);
				});
			}
		}
	}
}
