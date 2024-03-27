using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ��ͼ�༭��Map����չ���
[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
	[HideInInspector]
	public Map Map = null;

	// �ؿ��б�
	List<FileInfo> m_files = new List<FileInfo>();

	// ��ǰ���ڱ༭�Ĺؿ�������
	int m_selectIndex = -1;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		// ֻ�ڱ༭�����е�ʱ������ʹ��
		if (Application.isPlaying) {
			// ������Mono�ű����
			Map = target as Map;

			// ���ƹؿ��б����UI
			EditorGUILayout.BeginHorizontal();
			// �ؿ��б�
			int currentIndex = EditorGUILayout.Popup(m_selectIndex, GetNames(m_files));
			if (currentIndex != m_selectIndex) {
				m_selectIndex = currentIndex;
				LoadLevel();
			}
			if (GUILayout.Button("��ȡ�б�")) {
				LoadLevelFiles();
			}
			EditorGUILayout.EndHorizontal();

			// ���������ťUI
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("�������")) {
				Map.ClearHolder();
			}
			if (GUILayout.Button("�������")) {
				Map.ClearRoad();
			}
			EditorGUILayout.EndHorizontal();

			if (GUILayout.Button("��������")) {
				SaveLevel();
			}
		}

		// �޸ĺ�������UI
		if(GUI.changed) {
			EditorUtility.SetDirty(target);
		}
	}

	// ��ȡ�ؿ��б��ļ�
	void LoadLevelFiles()
	{
		// ���״̬
		Clear();

		// �����б�
		m_files = Tools.GetLevelFiles();

		// Ĭ�ϼ��ص�һ���ؿ�
		if(m_files.Count > 0) {
			m_selectIndex = 0;
			LoadLevel();
		}
	}

	// ���ص�ǰѡ��Ĺؿ�
	void LoadLevel()
	{
		FileInfo file = m_files[m_selectIndex];

		Level level = new Level();
		Tools.FillLevel(file.FullName, ref level);

		Map.LoadLevel(level);
	}

	// ����ؿ�
	void SaveLevel()
	{
		// ��ȡ��ǰ���صĹؿ�
		Level level = Map.Level;

		// ��ʱ������
		List<Point> points = new List<Point>();

		// �ռ�������
		for (int i = 0; i < Map.Grid.Count; i++) {
			Tile t = Map.Grid[i];
			if(t.CanHold) {
				Point p = new Point(t.X, t.Y);
				points.Add(p);
			}
		}
		level.Holder = points;

		// �ռ�Ѱ·��
		points = new List<Point>();
		for (int i = 0; i < Map.Road.Count; i++) {
			Tile t = Map.Road[i];
			Point p = new Point(t.X, t.Y);
			points.Add(p);
		}
		level.Path = points;

		// ���л�����ؿ�����
		string fileName = m_files[m_selectIndex].FullName;
		Tools.SaveLevel(fileName, level);

		// ������ʾ
		EditorUtility.DisplayDialog("����ؿ�����", "����ɹ�", "ȷ��");

	}

	// ���״̬
	void Clear()
	{
		m_files.Clear();
		m_selectIndex = -1;
	}

	// ��ȡ�ؿ������б�
	string[] GetNames(List<FileInfo> files)
	{
		List<string> names = new List<string>();
		foreach (FileInfo file in files) {
			names.Add(file.Name);
		}

		return names.ToArray();
	}
}
