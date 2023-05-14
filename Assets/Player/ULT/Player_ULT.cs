using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_ULT : MonoBehaviour
{
    private RectTransform obj;
    public GameObject Ultimate;
    public GameObject UltLast;
    public GameObject FullChargeAnim;
    private bool once;
    public static bool DestroyObj = false;

    // 必殺技までのカウント
    static float ult_cnt = 0.0f;
    // 必殺技カウントの最大値
    public float ult_MaxCnt = 30.0f;
    // 必殺技を使ったか
    private bool play;
    // 汎用
    private float tmp;

    // 必殺技開始位置
    private Vector2 ultPos;
    // 必殺技を撃ってからのカウント
    private int CreateTimer;
    // 今何列目？
    private int BombNum;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<RectTransform>();

        once = true;

        ult_cnt = 0.0f;
        play = false;
        tmp = ult_cnt;

        CreateTimer = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (!play)
        {
            // tmpを今のカウントまで少しずつ値を増やす
            if (ult_cnt >= tmp)
            {
                tmp += 0.05f;
            }

            // カウントが最大値で止まるようにする
            if (ult_MaxCnt < tmp)
            {
                ult_cnt = ult_MaxCnt;
                tmp = ult_MaxCnt;
                if (once)
                {
                    DestroyObj = false;
                    Instantiate(FullChargeAnim,
                               new Vector3(-290.0f, 217.0f, 0.0f),
                               Quaternion.identity);
                    once = false;
                }
            }
        }
        else
        {
            // カウントを0に戻す
            ult_cnt = 0.0f;

            // tmpを今のカウントまで少しずつ値を減らす
            if (ult_cnt <= tmp)
            {
                tmp--;
            }

            // tmpが0以下になったら
            if (tmp <= 0)
            {
                tmp = 0.0f;
                CreateExplosion();
            }
        }

        // カウントがいっぱいだったら
        if (ult_MaxCnt <= ult_cnt)
        {
            // 停止中は押せないようにする
            if (Mathf.Approximately(Time.timeScale, 1f))
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Time.timeScale = 0.0f;
                    play = true;
                    ultPos = new Vector2(-7.5f, 4.5f);
                    CreateTimer = 30;
                    BombNum = 0;

                    DestroyObj = true;
                    once = true;
                }
            }
        }

        // オブジェクトの大きさを反映する
        obj.sizeDelta = new Vector2(tmp * (520.0f / ult_MaxCnt), obj.sizeDelta.y);
    }

    // 必殺技カウントを加算したい場所で呼び出す
    public static void AddUltCnt()
    {
        // 値を加算
        ult_cnt++;
    }

    // 必殺技の動き
    private void CreateExplosion()
    {
        if (BombNum < 6)
        {
            CreateTimer++;
            if (CreateTimer >= 30)
            {
                for (int i = 0; i < 4; i++)
                {
                    Instantiate(Ultimate, new Vector3(ultPos.x, ultPos.y, 0.0f), Quaternion.identity);
                    ultPos.y -= 3.0f;
                }
                ultPos.x += 3.0f;
                ultPos.y = 4.5f;
                BombNum++;
                CreateTimer = 0;
            }
        }
        else
        {
            Instantiate(UltLast, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            play = false;
        }
    }
}
