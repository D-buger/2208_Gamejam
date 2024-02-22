using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datas
{
    //게임 전반
    private int gameStartNum = 0;
    private float gameTotalPlayTime = 0;
    private int totalUsedGuard = 0;
    //인 게임
    private float crabDraggedTime = 0;
    private int seagullTouchNum = 0;
    private int beachBallDraggedNum = 0;
    private int surfBoardNum = 0;

    //프로퍼티
    public int GameStartNum
    {
        set => gameStartNum = value;
    }
    public float GameTotalPlayTime
    {
        get => gameTotalPlayTime;
        set => gameTotalPlayTime = value;
    }
    public int TotalUsedGuard
    {
        set => totalUsedGuard = value;
    }

    public float CrabDraggedTime
    {
        get => crabDraggedTime;
        set => crabDraggedTime = value;
    }
    public int SeagullTouchNum
    {
        get => seagullTouchNum;
        set => seagullTouchNum = value;
    }
    public int BeachBallDraggedNum
    {
        get => beachBallDraggedNum;
        set => beachBallDraggedNum = value;
    }
    public int SurfBoardNum
    {
        get => surfBoardNum;
        set => surfBoardNum = value;
    }

    public static Datas operator +(Datas data0, Datas data1)
    {
        Datas d = new Datas();
        d.gameStartNum = data0.gameStartNum + data1.gameStartNum;
        d.gameTotalPlayTime = data0.gameTotalPlayTime + data1.gameTotalPlayTime;
        d.totalUsedGuard = data0.totalUsedGuard + data1.totalUsedGuard;
        d.crabDraggedTime = data0.crabDraggedTime + data1.crabDraggedTime;
        d.seagullTouchNum = data0.seagullTouchNum + data1.seagullTouchNum;
        d.beachBallDraggedNum = data0.beachBallDraggedNum + data1.beachBallDraggedNum;
        d.surfBoardNum = data0.surfBoardNum + data1.surfBoardNum;

        d.SaveData();
        return d;
    }
}
