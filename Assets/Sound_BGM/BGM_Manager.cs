using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM_Manager : MonoBehaviour
{
    static public BGM_Manager instance;

    // 前のシーン
    private string beforeScene = "";

    // 流したいオーディオソース
    public AudioSource TitleBGM;
    public AudioSource GameBGM;
    public AudioSource ResultBGM;

    // Start is called before the first frame update
    void Start()
    {
        // OnActiveSceneChanged関数を使うために
        SceneManager.activeSceneChanged += OnActiveSceneChanged;

        Debug.Log(SceneManager.GetActiveScene().name);
        beforeScene = SceneManager.GetActiveScene().name;

        // 実行した時の最初のシーンで流す曲を変える
        // タイトル シーン
        if (SceneManager.GetActiveScene().name == "Title")
        {
            TitleBGM.Play();
        }
        // ゲーム シーン
        if (SceneManager.GetActiveScene().name == "Game")
        {
            GameBGM.Play();
        }
        // リザルト シーン
        if (SceneManager.GetActiveScene().name == "Result")
        {
            ResultBGM.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 音量更新
        TitleBGM.volume = VolumeControl.BGM_Volume;
        GameBGM.volume = VolumeControl.BGM_Volume;
        ResultBGM.volume = VolumeControl.BGM_Volume;
    }

    // シーンが変わった瞬間に呼び出される関数
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        // タイトル -> ゲーム
        if (beforeScene == "Title" && nextScene.name == "Game")
        {
            Debug.Log(beforeScene + "->" + nextScene.name);
            TitleBGM.Stop();
            GameBGM.Play();
        }
        // ゲーム -> リザルト
        if (beforeScene == "Game" && nextScene.name == "Result")
        {
            Debug.Log(beforeScene + "->" + nextScene.name);
            GameBGM.Stop();
            ResultBGM.Play();
        }
        // ゲーム　-> タイトル
        if (beforeScene == "Game" && nextScene.name == "Title")
        {
            Debug.Log(beforeScene + "->" + nextScene.name);
            GameBGM.Stop();
            TitleBGM.Play();
        }
        // リザルト -> ゲーム
        if (beforeScene == "Result" && nextScene.name == "Game")
        {
            Debug.Log(beforeScene + "->" + nextScene.name);
            ResultBGM.Stop();
            GameBGM.Play();
        }
        // リザルト -> タイトル
        if (beforeScene == "Result" && nextScene.name == "Title")
        {
            Debug.Log(beforeScene + "->" + nextScene.name);
            ResultBGM.Stop();
            TitleBGM.Play();
        }


        beforeScene = nextScene.name;
    }

    private void Awake()
    {
        // 存在していなかったらインスタンス化
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        // 存在していたら消す
        else
        {
            Destroy(this.gameObject);
            Debug.Log("BGM_Managerを消しました");
        }
    }
}
