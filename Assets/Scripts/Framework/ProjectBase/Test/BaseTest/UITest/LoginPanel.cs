using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
	//public Button btnStart;
	//public Button btnQuit;

	protected override void Awake()
	{
		// �������Awakeʱ��Ҫ��ִ�и����Awake���󶨿ؼ��Ȳ���...
		base.Awake();
	}

	private void Start()
	{
		// 1.ԭʼд��
		//btnStart.onClick.AddListener(ClickStart);
		//btnQuit.onClick.AddListener(ClickQuit);

		// 2.�����Լ������ؼ�д��
		//GetControl<Button>("btnStart").onClick.AddListener(ClickStart);
		//GetControl<Button>("btnQuit").onClick.AddListener(ClickQuit);


		// ʹ��EventTrigger����һЩ�Զ����¼�
		// ����ť����һ��������
		EventTrigger trigger = GetControl<Button>("btnStart").gameObject.AddComponent<EventTrigger>();
		 
		// ��btnStart��ť���һ���Զ������ק�¼�������
		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.Drag;
		entry1.callback.AddListener(Drag);
		// ��btnStart��ť�����һ��ָ�밴���¼�
		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry2.eventID = EventTriggerType.PointerDown;
		entry2.callback.AddListener(PointerDown);
		  
		trigger.triggers.Add(entry1);
		trigger.triggers.Add(entry2);

		// ʹ��UIMgr����е�д���������Զ����¼�
		UIMgr.AddCustomEventListener(GetControl<Button>("btnStart"), EventTriggerType.PointerEnter, (data) =>{
			// �Զ����һ�����ָ�������¼�
			Debug.Log("�������˰�ť");
		});

	}

	// �Զ����һ����ק�¼�
	private void Drag(BaseEventData data)
	{
		Debug.Log("��ť����ק");
	}

	// �Զ����һ�����ָ�밴���¼�
	private void PointerDown(BaseEventData data)
	{
		Debug.Log("��ť������");
	}

	// ����ʾ���ʱ ��Ҫִ�е��߼���UI�������л��Զ����øú���
	public override void ShowMe()
	{
		base.ShowMe();

        // �����ʾ��Ҫ�����£����米���е���Ʒ����
        Debug.Log("�����ʾ��Ҫ������...");
	}

	// 3.�����Զ��������пؼ�д�����Ƽ���
	// ��ť����
	protected override void OnClick(string btnName)
	{
		switch(btnName) {
            case "btnStart":
                ClickStart();
                break;
			case "btnQuit":
				ClickQuit();
				break;
		}
	}

	// ��ѡ�����
	protected override void OnValueChanged(string toggleName, bool value)
	{
		// �߼��Ͱ�ť�ؼ��߼�����

	}

	public void InitInfo()
    {
        Debug.Log("��ʼ������");
    }

	// �����ʼ��ť�Ĵ���
	public void ClickStart()
    {
        Debug.Log("��ʼ");

        // ��ʾloading���ؽ���
        //UIMgr.GetInstance().ShowPanel<LoadingPanel>("LoadingPanel");
    }

    // ����˳���ť�Ĵ���
    public void ClickQuit()
    {
		Debug.Log("�˳�");
	}
}
