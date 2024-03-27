using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ���panel����
/// ����ͨ����������ҵ������ӿؼ��������������д����߼�
/// </summary>
public class BasePanel : MonoBehaviour
{
	// ͨ����ʽת��ԭ�� ���洢���пؼ�
	// ��List�洢������Ϊ����һ���ؼ���ͬʱ����Button��Image�����
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

	// ��ʾ�Լ�
	public virtual void ShowMe() { }

	// �����Լ�
	public virtual void HideMe() { }

	// �����ĳ����ť�������Ӧ�¼���Ȩ��Ϊprotected ֻ��������Ե��ã�
	protected virtual void OnClick(string btnName) { }

	// �����ĳ����ѡ��ı����Ӧ�¼�
	protected virtual void OnValueChanged(string toggleName, bool value) { }

	// �õ���Ӧ���ֵĶ�Ӧ�ؼ��ű�
	//		���ظÿؼ���T���͵Ŀؼ�
	//		���磺GetControl<Button>���صľ��Ǹÿؼ��ϵ�Button
	//			  GetControl<Image>���صľ��Ǹÿؼ��ϵ�Image
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

	// �ҵ��Ӷ���Ķ�Ӧ�ؼ�
	private void FindChildrenControl<T>() where T : UIBehaviour
	{
		T[] controls = this.GetComponentsInChildren<T>();

		for (int i = 0; i < controls.Length; i++) {
			// ��ȡ�ؼ���
			string objName = controls[i].gameObject.name;
			if (controlDic.ContainsKey(objName)) {
				controlDic[objName].Add(controls[i]);
			}
			else {
				controlDic.Add(objName, new List<UIBehaviour>() { controls[i] });
			}

			// Base�����Զ�������������ؼ�
			if (controls[i] is Button) {
				// ��ť�ؼ�
				(controls[i] as Button).onClick.AddListener(() => {
					OnClick(objName);
				});
			} else if (controls[i] is Toggle) {
				// ��ѡ��ؼ�
				(controls[i] as Toggle).onValueChanged.AddListener((value) => {
					OnValueChanged(objName, value);
				});
			}
		}
	}
}
