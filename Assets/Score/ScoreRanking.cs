using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreRanking : MonoBehaviour
{
    static public ScoreRanking instance;

    private int[] Score_Ranking = new int[6];

    // オブジェクト取得
    public GameObject obj;
    public TextMeshProUGUI First;
    public TextMeshProUGUI Second;
    public TextMeshProUGUI Third;
    public TextMeshProUGUI Fourth;
    public TextMeshProUGUI Fifth;

    // 前のシーン
    private string beforeScene = "";

    // 1回だけ
    private bool once;
    // 汎用
    private int tmp;

    // Start is called before the first frame update
    void Start()
    {
        // OnActiveSceneChanged関数を使うために
        SceneManager.activeSceneChanged += OnActiveSceneChanged;

        beforeScene = SceneManager.GetActiveScene().name;

        once = true;
        tmp = 0;

        obj.transform.position = new Vector3(-1000.0f, 740.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (once)
        {
            Score_Ranking[5] = Score.score;

            // ソート
            Array.Sort(Score_Ranking);
            Array.Reverse(Score_Ranking);

            once = false;
        }

        // テキストに値を反映する
        First.SetText(" 1I  {0000}", Score_Ranking[0]);
        Second.SetText("2I  {0000}", Score_Ranking[1]);
        Third.SetText("3I  {0000}", Score_Ranking[2]);
        Fourth.SetText("4I  {0000}", Score_Ranking[3]);
        Fifth.SetText("5I  {0000}", Score_Ranking[4]);
    }

    // シーンが変わった瞬間に呼び出される関数
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        if (nextScene.name == "Ranking")
        {
            obj.transform.position = new Vector3(1000.0f, 740.0f, 0.0f);
        }
        else
        {
            obj.transform.position = new Vector3(-1000.0f, 740.0f, 0.0f);
        }

        // ランキング更新のタイミング
        // ゲーム >> タイトル
        // リザルト >> タイトル
        // リザルト >> ゲーム
        if ((beforeScene == "Game" && nextScene.name == "Title")
            || (beforeScene == "Result" && nextScene.name == "Title")
            || (beforeScene == "Result" && nextScene.name == "Game"))
        {
            Debug.Log("ランキング更新しました");
            once = true;
        }

        beforeScene = nextScene.name;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("ScoreRankingを消しました");
        }
    }
}
