using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : ApplicationBase<Game>
{
    // ȫ�ַ���
    public void LoadScene(int level)
    {
        // �˳��ɳ���
        // �¼�����
        SceneArgs e = new SceneArgs();
        e.SceneIndex = SceneManager.GetActiveScene().buildIndex;

		// �����¼�
		SendEvent(Consts.E_ExitScene, e);

		// �����³���
		ScenesMgr.GetInstance().LoadScene(level, LoadSceneMode.Single);

	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("OnSceneLoaded: " + scene.name + " (Build Index: " + scene.buildIndex + ")");

		// �¼�����
		SceneArgs e = new SceneArgs();
		e.SceneIndex = scene.buildIndex;

		// �����¼�
		SendEvent(Consts.E_EnterScene, e);
	}

    // ��Ϸ���
    void Start()
    {
		//ȷ��Game����һֱ����
		Object.DontDestroyOnLoad(this.gameObject);

		// ��ӳ�����������¼��ļ���
		SceneManager.sceneLoaded += OnSceneLoaded;

		// ȫ�ֵ�����ֵ
		StaticData.GetInstance();

		// ע����������
		RegisterController(Consts.E_StartUp, typeof(StartUpCommand));

		// ������Ϸ
		SendEvent(Consts.E_StartUp);
    }
}
