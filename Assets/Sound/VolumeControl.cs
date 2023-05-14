using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VolumeControl : MonoBehaviour
{
    // 音量
    public static float BGM_Volume = 0.1f;
    public static float SE_Volume = 0.1f;

    /*--------------
        ゲージ
    --------------*/
    // オブジェクト取得
    public RectTransform BGM_Gauge;
    public RectTransform SE_Gauge;
    // デフォルトのサイズ格納用
    private Vector2 BGM_DefaultSize;
    private Vector2 SE_DefaultSize;
    // 長押しでゲージが進みすぎないようにするカウント
    private int Gauge_Cnt = 30;
    // 一回だけ
    private bool once = true;

    /*--------------
        音量(TMP)
    --------------*/
    // オブジェクト取得
    public TextMeshProUGUI BGM_Text;
    public TextMeshProUGUI SE_Text;

    /*--------------
        選択棒
    --------------*/
    // オブジェクト取得
    public RectTransform Select_Bar;
    private bool up, down;

    // Start is called before the first frame update
    void Start()
    {
        /*--------------
            ゲージ
        --------------*/
        // デフォルトのサイズ取得
        BGM_DefaultSize = new Vector2(BGM_Gauge.sizeDelta.x, BGM_Gauge.sizeDelta.y);
        SE_DefaultSize = new Vector2(SE_Gauge.sizeDelta.x, SE_Gauge.sizeDelta.y);

        /*--------------
            選択棒
        --------------*/
        // 初期位置
        // 今のシーンが「タイトル」なら
        if (SceneManager.GetActiveScene().name == "Title")
        {
            Select_Bar.transform.position = new Vector3(BGM_Gauge.position.x - 100.0f, BGM_Gauge.position.y, 0.0f);
        }
        // 今のシーンが「ゲーム」なら
        if (SceneManager.GetActiveScene().name == "Game")
        {
            Select_Bar.transform.position = new Vector3(BGM_Gauge.position.x - 1.0f, BGM_Gauge.position.y, 0.0f);
        }

        up = true;
        down = false;
    }

    // Update is called once per frame
    void Update()
    {
        // オプションシーン or ポーズ中で音量調整
        if (SceneManager.GetActiveScene().name == "Option" || Mathf.Approximately(Time.timeScale, 0f))
        {
            if (Gauge_Cnt >= 30)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (up)
                        BGM_Volume += 0.01f;
                    if (down)
                        SE_Volume += 0.01f;

                    Gauge_Cnt = 0;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (up)
                        BGM_Volume -= 0.01f;
                    if (down)
                        SE_Volume -= 0.01f;

                    Gauge_Cnt = 0;
                }
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (once)
                {
                    Gauge_Cnt = 30;
                    once = false;
                }
                Gauge_Cnt++;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                once = true;
            }

            // 値が'1'を超過、または'0'未満にならないようにする
            if (BGM_Volume < 0.0f) { BGM_Volume = 0.0f; }
            if (BGM_Volume > 1.0f) { BGM_Volume = 1.0f; }
            if (SE_Volume < 0.0f) { SE_Volume = 0.0f; }
            if (SE_Volume > 1.0f) { SE_Volume = 1.0f; }
        }


        /*--------------
            ゲージ
        --------------*/
        // オブジェクトの大きさを反映する
        BGM_Gauge.sizeDelta = new Vector2(BGM_Volume * BGM_DefaultSize.x, BGM_DefaultSize.y);
        SE_Gauge.sizeDelta = new Vector2(SE_Volume * SE_DefaultSize.x, SE_DefaultSize.y);

        /*--------------
            音量(TMP)
        --------------*/
        // テキストに値を反映する
        BGM_Text.SetText("{0}%", Mathf.Floor(BGM_Volume * 100.0f));
        SE_Text.SetText("{0}%", Mathf.Floor(SE_Volume * 100.0f));

        /*--------------
            選択棒
        --------------*/
        // 上アローキーを押したとき
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 今のシーンが「タイトル」なら
            if (SceneManager.GetActiveScene().name == "Option")
            {
                // 移動
                Select_Bar.transform.position = new Vector3(BGM_Gauge.position.x - 100.0f, BGM_Gauge.position.y, 0.0f);
            }
            // 今のシーンが「ゲーム」なら
            if (SceneManager.GetActiveScene().name == "Game")
            {
                // 移動
                Select_Bar.transform.position = new Vector3(BGM_Gauge.position.x - 1.0f, BGM_Gauge.position.y, 0.0f);
            }

            up = true;
            down = false;
        }
        // 下アローキーを押したとき
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 今のシーンが「タイトル」なら
            if (SceneManager.GetActiveScene().name == "Option")
            {
                // 移動
                Select_Bar.transform.position = new Vector3(SE_Gauge.position.x - 100.0f, SE_Gauge.position.y, 0.0f);
            }
            // 今のシーンが「ゲーム」なら
            if (SceneManager.GetActiveScene().name == "Game")
            {
                // 移動
                Select_Bar.transform.position = new Vector3(SE_Gauge.position.x - 1.0f, SE_Gauge.position.y, 0.0f);
            }

            up = false;
            down = true;
        }
    }
}