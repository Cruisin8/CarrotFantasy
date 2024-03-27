using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����������ģ��
/// </summary>
public class InputMgr : BaseManager<InputMgr>
{
	// �������Ƿ���
	private bool isInputCheckEnable = false;

	// ����ӳ���ϵ ���������ṩ������޸ļ�λ
	private Dictionary<string, KeyCode> dicKey = new Dictionary<string, KeyCode>();

    // ���캯���У����Update����
    public InputMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(MyUpdate);
    }

	// ����������
	public void EnableInputCheck()
	{
		isInputCheckEnable = true;
	}

	// �ر�������
	public void DisableInputCheck()
	{
		isInputCheckEnable = false;
	}

	// ��ѯ�����⿪��״̬
	public bool IsInputCheckEnabled()
	{
		return isInputCheckEnable;
	}

	// ��ⰴ��̧���·ַ��¼�
	private void CheckKeyCode(KeyCode key)
	{
		if (Input.GetKeyDown(key)) {
			// �¼�����ģ��ַ������¼�
			EventCenter.GetInstance().EventTrigger("ĳ������", key);
		}

		if (Input.GetKeyUp(key)) {
			// �¼�����ģ��ַ�̧���¼�
			EventCenter.GetInstance().EventTrigger("ĳ��̧��", key);
		}
	}

    private void MyUpdate()
    {
		// û�п��������⣬��ֱ�ӷ���
		if(!IsInputCheckEnabled()) {
			return;
		}

		CheckKeyCode(KeyCode.W);
		CheckKeyCode(KeyCode.S);
		CheckKeyCode(KeyCode.A);
		CheckKeyCode(KeyCode.D);
	}
}
