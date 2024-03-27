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
		// 子类调用Awake时，要先执行父类的Awake，绑定控件等操作...
		base.Awake();
	}

	private void Start()
	{
		// 1.原始写法
		//btnStart.onClick.AddListener(ClickStart);
		//btnQuit.onClick.AddListener(ClickQuit);

		// 2.子类自己监听控件写法
		//GetControl<Button>("btnStart").onClick.AddListener(ClickStart);
		//GetControl<Button>("btnQuit").onClick.AddListener(ClickQuit);


		// 使用EventTrigger监听一些自定义事件
		// 给按钮挂载一个触发器
		EventTrigger trigger = GetControl<Button>("btnStart").gameObject.AddComponent<EventTrigger>();
		 
		// 给btnStart按钮添加一个自定义的拖拽事件并监听
		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.Drag;
		entry1.callback.AddListener(Drag);
		// 给btnStart按钮再添加一个指针按下事件
		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry2.eventID = EventTriggerType.PointerDown;
		entry2.callback.AddListener(PointerDown);
		  
		trigger.triggers.Add(entry1);
		trigger.triggers.Add(entry2);

		// 使用UIMgr框架中的写法，监听自定义事件
		UIMgr.AddCustomEventListener(GetControl<Button>("btnStart"), EventTriggerType.PointerEnter, (data) =>{
			// 自定义的一个鼠标指针进入的事件
			Debug.Log("鼠标进入了按钮");
		});

	}

	// 自定义的一个拖拽事件
	private void Drag(BaseEventData data)
	{
		Debug.Log("按钮被拖拽");
	}

	// 自定义的一个鼠标指针按下事件
	private void PointerDown(BaseEventData data)
	{
		Debug.Log("按钮被按下");
	}

	// 在显示面板时 想要执行的逻辑，UI管理器中会自动调用该函数
	public override void ShowMe()
	{
		base.ShowMe();

        // 面板显示后要做的事，比如背包中的物品加载
        Debug.Log("面板显示后要做的事...");
	}

	// 3.父类自动监听所有控件写法（推荐）
	// 按钮监听
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

	// 单选框监听
	protected override void OnValueChanged(string toggleName, bool value)
	{
		// 逻辑和按钮控件逻辑类似

	}

	public void InitInfo()
    {
        Debug.Log("初始化数据");
    }

	// 点击开始按钮的处理
	public void ClickStart()
    {
        Debug.Log("开始");

        // 显示loading加载界面
        //UIMgr.GetInstance().ShowPanel<LoadingPanel>("LoadingPanel");
    }

    // 点击退出按钮的处理
    public void ClickQuit()
    {
		Debug.Log("退出");
	}
}
