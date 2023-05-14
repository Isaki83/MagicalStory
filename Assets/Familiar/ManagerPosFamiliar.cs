using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPosFamiliar : MonoBehaviour
{
    /* 使い魔の位置を管理するスクリプト*/
    // 変数宣言
    Vector2 Pos1 = new Vector2( -1.00f,  1.5f);  // 1,4,7...プレイヤーの左上
    Vector2 Pos2 = new Vector2( -1.00f, -1.5f);  // 2,5,8...プレイヤーの左下
    Vector2 Pos3 = new Vector2( -1.5f, 0.0f);    // 3,6,9...プレイヤーの後ろ
    int MaxFamiliar = 10;                        // 最大使い魔数
    bool[] UseFlg;                              // 開放しているかのフラグ管理
    int NowNumFamiliar = 0;                     // 現在いる使い魔の数


    // Start is called before the first frame update
    void Start()
    {
        UseFlg = new bool[MaxFamiliar];
        UseFlg[0] = true;
        for(int i = 1; i < MaxFamiliar; ++i)
        {
            UseFlg[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 使用フラグを格納して個体番号を返す
    public int SetUseTrueFlg()
    {
        Debug.Log("通過しました");
        int Num = -1; // 個体番号を初期化
        int i = 1;
        while(true) // 無限ループ
        {
            if (UseFlg[i]==false)
            {
                UseFlg[i] = true; // 使用フラグを更新
                Num = i; //個体番号を設定
                NowNumFamiliar = Num;
                break;
            }
            i++; // カウントを更新
        }

        return Num; // 個体番号を返す
    }

    // 現在の使い魔の数を返す
    public int GetNowNumFamiliar()
    {
        return NowNumFamiliar;
    }

    // プレイヤーの代わりに使い魔が死ぬときに使う
    public void subNowNumFamiliar()
    {
        NowNumFamiliar--;
    }
    public void FalseUseFlag(int num)
    {
        UseFlg[num] = false;
    }

    // 個体番号から定位置を割り当て
    public Vector2 GetFamiliarPos(int Num)
    {
        if (Num % 3 == 0)
        {
            return Pos3;
        }
        else if (Num % 2 == 0)
        {
            return Pos2;
        }
        else if (Num % 1 == 0)
        {
            return Pos1;
        }
        else
        {
            return new Vector2(0.0f, 0.0f);
        }
    }

    public int GetNumFamiliar()
    {
        return NowNumFamiliar;
    }
}
