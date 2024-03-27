using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoundModel : Model
{
	#region ����
	public const float ROUND_INTERVAL = 3f; // �غϼ��ʱ��
	public const float SPAWN_INTERVAL = 1f; // ���ּ��ʱ��
	#endregion

	#region �¼�
	#endregion

	#region �ֶ�
	List<Round> m_Rounds = new List<Round>();	// �ùؿ����еĳ�����Ϣ
	int m_RoundIndex = -1;						// ��ǰ�غϵ�����		
	bool m_IsAllRoundsComplete = false;         // �Ƿ����еĹֶ��Ѿ�������
	Coroutine m_Coroutine;
	#endregion

	#region ����
	public override string Name {
		get { return Consts.M_RoundModel; }
	}

	public int RoundIndex { 
		get { return m_RoundIndex; } 
	}

	public int RoundTotal {
		get { return m_Rounds.Count;}
	}

	public bool IsAllRoundComplete {
		get { return m_IsAllRoundsComplete; }
	}
	#endregion

	#region ����
	// ���عؿ�����
	public void LoadLevel(Level level)
	{
		m_Rounds = level.Rounds;
		m_RoundIndex = -1;
		m_IsAllRoundsComplete = false;
	}

	// ��ʼ�غ�
	public void StartRound()
	{
		// ����Э��
		m_Coroutine = Game.GetInstance().StartCoroutine(RunRound());
	}

	// ֹͣ�غ�
	public void StopRound()
	{
		Game.GetInstance().StopCoroutine(m_Coroutine);
	}

	IEnumerator RunRound()
	{
		for (int i = 0; i < m_Rounds.Count; i++) {
			// ���ûغ�
			m_RoundIndex = i;

			// �غϿ�ʼ�¼�
			StartRoundArgs e1 = new StartRoundArgs();
			e1.RoundIndex = i;
			e1.RoundTotal = RoundTotal;
			SendEvent(Consts.E_StartRound, e1);

			Round round = m_Rounds[i];

			for(int k = 0; k < round.Count; k++) {
				// ���ּ�϶
				yield return new WaitForSeconds(SPAWN_INTERVAL);

				// �����¼�
				SpawnMonsterArgs e2 = new SpawnMonsterArgs();
				e2.MonsterType = round.Monster;
				SendEvent(Consts.E_SpawnMonster, e2);

				// ���һ���������
				if((i == m_Rounds.Count - 1) && (k == round.Count - 1)) {
					// �������
					m_IsAllRoundsComplete = true;
				}
			}

			// �غϼ�϶
			if (!m_IsAllRoundsComplete) {
				yield return new WaitForSeconds(ROUND_INTERVAL);
			}
		}
	}

	#endregion

	#region Unity�ص�
	#endregion

	#region �¼��ص�
	#endregion

	#region ��������
	#endregion
}
