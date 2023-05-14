using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    // スコア格納用
    public static int score = 0;

    // シーンの初めにスコアを'0'に戻すかのフラグ
    public bool ScoreReset = true;

    // Start is called before the first frame update
    void Start()
    {
        // true ならシーンのはじめに'0'に戻す
        if (ScoreReset)
            score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreText == null) return;

        ScoreText.SetText("{0}", score);
    }

    // スコアを加算したい場所で呼び出す
    // 引数 : １回で増やしたい値
    public static void AddScore(int num)
    {
        // 引数に入れた値を加算
        Debug.Log("加算する前");
        score += num;
        Debug.Log("加算する後");
    }
}