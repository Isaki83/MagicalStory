using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 今のシーンが「タイトル」なら
        if (SceneManager.GetActiveScene().name == "Title")
        {
            // Enterキーで
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (UI_Color.Select == 0)
                    // ゲーム シーンに移動
                    SceneManager.LoadScene("Game");
                if (UI_Color.Select == 1)
                    // ランキング シーンに移動
                    SceneManager.LoadScene("Ranking");
                if (UI_Color.Select == 2)
                    // ランキング シーンに移動
                    SceneManager.LoadScene("Option");
                if (UI_Color.Select == 3)
                {
                    // ゲームを閉じる
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();//ゲームプレイ終了
#endif
                }
            }
        }

        // 今のシーンが「ランキング」なら
        if (SceneManager.GetActiveScene().name == "Ranking")
        {
            // Enterキーで
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // タイトル シーンに移動
                SceneManager.LoadScene("Title");
            }
        }

        // 今のシーンが「オプション」なら
        if (SceneManager.GetActiveScene().name == "Option")
        {
            // Enterキーで
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // タイトル シーンに移動
                SceneManager.LoadScene("Title");
            }
        }

        // 今のシーンが「ゲーム」なら
        if (SceneManager.GetActiveScene().name == "Game")
        {
            // プレイヤーがﾀﾋんだら
            if (Player_Bullet.ChangeScene)
            {
                // 3秒後にシーン遷移
                Invoke("ChengeToResult", 3.0f);
            }

            // ポーズ中
            if (Mathf.Approximately(Time.timeScale, 0f))
            {
                // Enterキーで
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Time.timeScale = 1f;
                    // ゲーム シーンに移動
                    SceneManager.LoadScene("Title");
                }
            }

            // デバッグ用
#if UNITY_EDITOR
            // Spaceキーで
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // リザルト シーンに移動
                SceneManager.LoadScene("Result");
            }
#endif
        }

        // 今のシーンが「リザルト」なら
        if (SceneManager.GetActiveScene().name == "Result")
        {
            // Enterキーで
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (UI_Color.right)
                    // タイトル シーンに移動
                    SceneManager.LoadScene("Title");
                if (UI_Color.left)
                    // ゲーム シーンに移動
                    SceneManager.LoadScene("Game");
            }
        }
    }

    // リザルト画面にシーン遷移
    void ChengeToResult()
    {
        SceneManager.LoadScene("Result");

        Player_Bullet.ChangeScene = false;
    }
}
