using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    // 关卡名
    public string Name;

    // 卡片
    public string CardImage;

    // 背景图片
    public string Background;

    // 路径图片
    public string Road;

    // 初始金币
    public int InitScore;

    // 炮塔可放置的位置
    public List<Point> Holder = new List<Point>();

    // 敌人行动路径
    public List<Point> Path = new List<Point>();

    // 出怪回合信息
    public List<Round> Rounds = new List<Round>();
}
