using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 地图编辑器Map类扩展组件
[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
	[HideInInspector]
	public Map Map = null;

	// 关卡列表
	List<FileInfo> m_files = new List<FileInfo>();

	// 当前正在编辑的关卡索引号
	int m_selectIndex = -1;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		// 只在编辑器运行的时候允许使用
		if (Application.isPlaying) {
			// 关联的Mono脚本组件
			Map = target as Map;

			// 绘制关卡列表组件UI
			EditorGUILayout.BeginHorizontal();
			// 关卡列表
			int currentIndex = EditorGUILayout.Popup(m_selectIndex, GetNames(m_files));
			if (currentIndex != m_selectIndex) {
				m_selectIndex = currentIndex;
				LoadLevel();
			}
			if (GUILayout.Button("读取列表")) {
				LoadLevelFiles();
			}
			EditorGUILayout.EndHorizontal();

			// 绘制清除按钮UI
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("清除塔点")) {
				Map.ClearHolder();
			}
			if (GUILayout.Button("清除塔点")) {
				Map.ClearRoad();
			}
			EditorGUILayout.EndHorizontal();

			if (GUILayout.Button("保存数据")) {
				SaveLevel();
			}
		}

		// 修改后更新组件UI
		if(GUI.changed) {
			EditorUtility.SetDirty(target);
		}
	}

	// 读取关卡列表文件
	void LoadLevelFiles()
	{
		// 清除状态
		Clear();

		// 加载列表
		m_files = Tools.GetLevelFiles();

		// 默认加载第一个关卡
		if(m_files.Count > 0) {
			m_selectIndex = 0;
			LoadLevel();
		}
	}

	// 加载当前选择的关卡
	void LoadLevel()
	{
		FileInfo file = m_files[m_selectIndex];

		Level level = new Level();
		Tools.FillLevel(file.FullName, ref level);

		Map.LoadLevel(level);
	}

	// 保存关卡
	void SaveLevel()
	{
		// 获取当前加载的关卡
		Level level = Map.Level;

		// 临时索引点
		List<Point> points = new List<Point>();

		// 收集放塔点
		for (int i = 0; i < Map.Grid.Count; i++) {
			Tile t = Map.Grid[i];
			if(t.CanHold) {
				Point p = new Point(t.X, t.Y);
				points.Add(p);
			}
		}
		level.Holder = points;

		// 收集寻路点
		points = new List<Point>();
		for (int i = 0; i < Map.Road.Count; i++) {
			Tile t = Map.Road[i];
			Point p = new Point(t.X, t.Y);
			points.Add(p);
		}
		level.Path = points;

		// 序列化保存关卡数据
		string fileName = m_files[m_selectIndex].FullName;
		Tools.SaveLevel(fileName, level);

		// 弹框提示
		EditorUtility.DisplayDialog("保存关卡数据", "保存成功", "确定");

	}

	// 清除状态
	void Clear()
	{
		m_files.Clear();
		m_selectIndex = -1;
	}

	// 获取关卡名称列表
	string[] GetNames(List<FileInfo> files)
	{
		List<string> names = new List<string>();
		foreach (FileInfo file in files) {
			names.Add(file.Name);
		}

		return names.ToArray();
	}
}
