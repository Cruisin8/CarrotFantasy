using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    // �ؿ���
    public string Name;

    // ��Ƭ
    public string CardImage;

    // ����ͼƬ
    public string Background;

    // ·��ͼƬ
    public string Road;

    // ��ʼ���
    public int InitScore;

    // �����ɷ��õ�λ��
    public List<Point> Holder = new List<Point>();

    // �����ж�·��
    public List<Point> Path = new List<Point>();

    // ���ֻغ���Ϣ
    public List<Round> Rounds = new List<Round>();
}
