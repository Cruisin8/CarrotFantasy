using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����������
public class TileClickEventArgs: EventArgs
{
	public int MouseButton;	// 0��� 1�Ҽ�
	public Tile Tile;
	public TileClickEventArgs(int mouseButton, Tile tile)
	{
		this.MouseButton = mouseButton;
		this.Tile = tile;
	}
}

// ��������һ���ؿ���ͼ��״̬
public class Map : MonoBehaviour
{
	#region ����
	public const int RowCount = 8;   //����
	public const int ColumnCount = 12; //����
	#endregion

	#region �¼�
	public event EventHandler<TileClickEventArgs> OnTileClick;
	#endregion

	#region �ֶ�
	float MapWidth;//��ͼ��
	float MapHeight;//��ͼ��

	float TileWidth;//���ӿ�
	float TileHeight;//���Ӹ�

	List<Tile> m_grid = new List<Tile>(); //���Ӽ���
	List<Tile> m_road = new List<Tile>(); //·������

	Level m_level; //�ؿ�����

	public bool DrawGizmos = true; // �Ƿ���Ʊ��
	#endregion

	#region ����
	// �ؿ����ݣ��������ⲿ����
	public Level Level {
		get {
			return m_level;
		}
	}

	public string BackgroundImage {
		set {
			SpriteRenderer render = transform.Find("Background").GetComponent<SpriteRenderer>();
			StartCoroutine(Tools.LoadImage(value, render));
		}
	}

	public string RoadImage {
		set {
			SpriteRenderer render = transform.Find("Road").GetComponent<SpriteRenderer>();
			StartCoroutine(Tools.LoadImage(value, render));
		}
	}

	public Rect MapRect {
		get { return new Rect(-MapWidth / 2, -MapHeight / 2, MapWidth, MapHeight); }
	}

	public List<Tile> Grid {
		get { return m_grid; }
	}

	public List<Tile> Road {
		get { return m_road; }
	}

	//���˵�Ѱ··��
	public Vector3[] Path {
		get {
			List<Vector3> m_path = new List<Vector3>();
			for (int i = 0; i < m_road.Count; i++) {
				Tile t = m_road[i];
				Vector3 point = GetPosition(t);
				m_path.Add(point);
			}
			return m_path.ToArray();
		}
	}

	#endregion

	#region ����
	// ���عؿ�
	public void LoadLevel(Level level)
	{
		// ��յ�ǰ״̬
		Clear();

		//����
		this.m_level = level;

		//���ر�����·��ͼƬ
		this.BackgroundImage = "file://" + Consts.MapDir + "/" + level.Background;
		this.RoadImage = "file://" + Consts.MapDir + "/" + level.Road;

		//Ѱ·��
		for (int i = 0; i < level.Path.Count; i++) {
			Point p = level.Path[i];
			Tile t = GetTile(p.X, p.Y);
			m_road.Add(t);
		}

		//������
		for (int i = 0; i < level.Holder.Count; i++) {
			Point p = level.Holder[i];
			Tile t = GetTile(p.X, p.Y);
			t.CanHold = true;
		}
	}

	// ���������Ϣ
	public void ClearHolder()
	{
		foreach (Tile t in m_grid) {
			if (t.CanHold) {
				t.CanHold = false;
			}
		}
	}

	// ���Ѱ·���Ӽ���
	public void ClearRoad()
	{
		m_road.Clear();
	}

	// ���������Ϣ
	public void Clear()
	{
		m_level = null;
		ClearHolder();
		ClearRoad();
	}

	#endregion

	#region Unity�ص�
	//ֻ����Ϸ������������
	void Awake()
	{
		//�����ͼ�͸��Ӵ�С
		CalculateSize();

		//�������еĸ���
		for (int i = 0; i < RowCount; i++)
			for (int j = 0; j < ColumnCount; j++)
				m_grid.Add(new Tile(j, i));

		//����������¼�
		OnTileClick += Map_OnTileClick;
	}

	void Update()
	{
		// ���������
		if (Input.GetMouseButtonDown(0)) {
			Tile t = GetTileUnderMouse();
			if (t != null) {
				// ��������������¼�
				TileClickEventArgs e = new TileClickEventArgs(0, t);
				if (OnTileClick != null) {
					OnTileClick(this, e);
				}
			}
		}

		// ����Ҽ����
		if (Input.GetMouseButtonDown(1)) {
			Tile t = GetTileUnderMouse();
			if (t != null) {
				// ��������������¼�
				TileClickEventArgs e = new TileClickEventArgs(1, t);
				if (OnTileClick != null) {
					OnTileClick(this, e);
				}
			}
		}
	}

	// ֻ�ڵ�ͼ�༭����������
	// ���Ƶ�ͼ
	void OnDrawGizmos()
	{
		if (!DrawGizmos)
			return;

		//�����ͼ�͸��Ӵ�С
		CalculateSize();

		//���Ƹ���
		Gizmos.color = Color.green;

		//������
		for (int row = 0; row <= RowCount; row++) {
			Vector2 from = new Vector2(-MapWidth / 2, -MapHeight / 2 + row * TileHeight);
			Vector2 to = new Vector2(-MapWidth / 2 + MapWidth, -MapHeight / 2 + row * TileHeight);
			Gizmos.DrawLine(from, to);
		}

		//������
		for (int col = 0; col <= ColumnCount; col++) {
			Vector2 from = new Vector2(-MapWidth / 2 + col * TileWidth, MapHeight / 2);
			Vector2 to = new Vector2(-MapWidth / 2 + col * TileWidth, -MapHeight / 2);
			Gizmos.DrawLine(from, to);
		}


		foreach (Tile t in m_grid) {
			if (t.CanHold) {
				Vector3 pos = GetPosition(t);
				Gizmos.DrawIcon(pos, "holder.png", true);
			}
		}

		Gizmos.color = Color.red;
		for (int i = 0; i < m_road.Count; i++) {
			//���
			if (i == 0) {
				Gizmos.DrawIcon(GetPosition(m_road[i]), "start.png", true);
			}

			//�յ�
			if (m_road.Count > 1 && i == m_road.Count - 1) {
				Gizmos.DrawIcon(GetPosition(m_road[i]), "end.png", true);
			}

			//��ɫ������
			if (m_road.Count > 1 && i != 0) {
				Vector3 from = GetPosition(m_road[i - 1]);
				Vector3 to = GetPosition(m_road[i]);
				Gizmos.DrawLine(from, to);
			}
		}
	}
	#endregion

	#region �¼��ص�
	void Map_OnTileClick(object sender, TileClickEventArgs e)
	{
		// ��ǰ�������� �ؿ��༭������ ���ܱ༭
		if(gameObject.scene.name != "LevelBuilder") {
			return;
		}

		if (Level == null) {
			return;
		}

		// �����������
		if (e.MouseButton == 0 && !m_road.Contains(e.Tile)) {
			e.Tile.CanHold = !e.Tile.CanHold;
		}
		// ����Ѱ·�����
		if (e.MouseButton == 1 && !e.Tile.CanHold) {
			if (m_road.Contains(e.Tile)) {
				m_road.Remove(e.Tile);
			}
			else {
				m_road.Add(e.Tile);
			}
		}
	}

		#endregion

		#region ��������
		//�����ͼ��С�����Ӵ�С
		void CalculateSize()
	{
		Vector3 leftDown = new Vector3(0, 0);
		Vector3 rightUp = new Vector3(1, 1);

		// ���ӿ�����ת��Ϊ�����������
		Vector3 p1 = Camera.main.ViewportToWorldPoint(leftDown);
		Vector3 p2 = Camera.main.ViewportToWorldPoint(rightUp);

		MapWidth = (p2.x - p1.x);
		MapHeight = (p2.y - p1.y);

		TileWidth = MapWidth / ColumnCount;
		TileHeight = MapHeight / RowCount;
	}

	//��ȡ�������ĵ����ڵ���������
	public Vector3 GetPosition(Tile t)
	{
		return new Vector3(
				-MapWidth / 2 + (t.X + 0.5f) * TileWidth,
				-MapHeight / 2 + (t.Y + 0.5f) * TileHeight,
				0
			);
	}

	//���ݸ��������Ż�ø���
	Tile GetTile(int tileX, int tileY)
	{
		int index = tileX + tileY * ColumnCount;

		if (index < 0 || index >= m_grid.Count)
			return null;

		return m_grid[index];
	}

	// �������������ȡ����
	public Tile GetTile(Vector3 worldPos)
	{
		int col = (int)((worldPos.x + MapWidth / 2) / TileWidth);
		int row = (int)((worldPos.y + MapHeight / 2) / TileHeight);
		return GetTile(col, row);
	}

	//��ȡ�������ĸ���
	Tile GetTileUnderMouse()
	{
		Vector2 worldPos = GetWorldPosition();
		int col = (int)((worldPos.x + MapWidth / 2) / TileWidth);
		int row = (int)((worldPos.y + MapHeight / 2) / TileHeight);
		return GetTile(col, row);
	}

	//��ȡ�������λ�õ���������
	Vector3 GetWorldPosition()
	{
		Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
		return worldPos;
	}
	#endregion

}
