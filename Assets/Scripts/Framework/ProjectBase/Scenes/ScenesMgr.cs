using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// �����л�ģ��
/// </summary>
public class ScenesMgr : BaseManager<ScenesMgr> 
{
	// ͬ�����س���(ʹ�ó���������)
	public void LoadScene(string sceneName, UnityAction fun)
    {
        // ����ͬ������
        SceneManager.LoadScene(sceneName);
        // �����������֮�󣬲Ż�ִ��fun
        fun();
    }

	// ͬ�����س���(ʹ�ó����ż���)
	// LoadSceneMode.Single ʹ�ó����ż��ص�һ��������ر�����������ֻ���ظó�����
	// LoadSceneMode.Additive ʹ�ó����ż��ظ��ӳ������Ὣ������ӵ���ǰ���صĳ����У�
	public void LoadScene(int sceneBuildIndex, LoadSceneMode mode)
	{
		// ����ͬ������
		SceneManager.LoadScene(sceneBuildIndex, mode);
	}

	// �첽���س��� �ṩ���ⲿ�Ľӿ�
	public void LoadSceneAsyn(string sceneName, UnityAction fun)
    {
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadSceneAsyn(sceneName, fun));
    }

    // ����Э�� �첽���س���
    private IEnumerator ReallyLoadSceneAsyn(string sceneName, UnityAction fun)
	{
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);

        // ��ȡ�������ؽ���
        while(!ao.isDone) {
            // ������
            // ���������¼���������ַ��������������������ʹ��
            EventCenter.GetInstance().EventTrigger("���������µĺ���", ao.progress);
            // ������������½�����
            yield return ao.progress;
        }

        yield return ao;
        // �����������֮�󣬲Ż�ִ��fun
        fun();
    }
}
