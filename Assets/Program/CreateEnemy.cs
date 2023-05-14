using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateEnemy : MonoBehaviour
{
    
    public GameObject No01;     // 任意のオブジェクトのPrefabを入れる
    public GameObject No02;     // 任意のオブジェクトのPrefabを入れる
    public GameObject No03;     // 任意のオブジェクトのPrefabを入れる
    public GameObject No04;     // 任意のオブジェクトのPrefabを入れる

    bool CsvOrRand = true;            // csvとランダムどちらで生成するか
                                      // csv : true
                                      // ランダム : false

    //==============================================
    //  csv生成用
    //==============================================
    private GameObject ObjectPut;    // オブジェクトを配置するのに使う

    public string csvStage = "";    // インスペクタービューで使いたいCSVファイルのパスをかく
    private TextAsset csvFile;      // CSVファイル読み込むのに使う

    string str = "";                // CSVの全文字列を保存する
    string strget = "";             // 取り出した文字列を保存する

    string line = "";               // reader.ReadLine()の格納先

    int row = 0;                    // CSVデータの行数
    int column = 0;                 // CSVデータの列数

    int[,] map = new int[256, 256]; // マップ番号を格納するマップ用変数
    int[] iDat = new int[4];        // 文字検索用
    
    float posX = 0.0f;            // マップ置くX座標
    float posY = 0.0f;            // マップ置くY座標

    float AddFlame = 0.0f;          // フレーム
    int[] Second = new int[255];    // .csvから読み取った時間格納用
    bool[] bCreat = new bool[255];  // 1秒の間に一回しか生成を行なわないようにする

    //==============================================
    //  ランダム生成用
    //==============================================
    int SelectEnemy = 0;
    float EnemyPos;
    float CreaterTime;
    public float MaxCreaterTime = 5.0f;
    public float MinCreaterTime = 0.7f;

    public int Wave = 6000;
    float WaveCount = 0.0f;

    public int LaserWave = 9000;
    int LaserWaveCount = 3000;


    void Start()
    {
        AddFlame = 0.0f;
        CreaterTime = MaxCreaterTime;

        // csv
        Load();
        Creat();
    }

    void Update()
    {
        // ポーズ中は何もしない
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        // フレームを進める
        AddFlame += Time.deltaTime;
        int nowSecond = (int)AddFlame;

        switch (CsvOrRand)
        {
            case true:
                //==============================================
                //  csvで生成
                //==============================================
                for (int c = 0; c < column; c++)
                {
                    if (Second[c] == nowSecond && bCreat[c] == true)
                    {
                        posX = 9.0f;
                        posY = 4.0f;
                        for (int r = 1; r < row; r++)
                        {
                            if (map[r, c] == 1)
                            {
                                ObjectPut = Instantiate(No01) as GameObject;
                                ObjectPut.transform.position = new Vector3(posX, posY, 0.0f);
                            }

                            if (map[r, c] == 2)
                            {
                                ObjectPut = Instantiate(No02) as GameObject;
                                ObjectPut.transform.position = new Vector3(posX, posY, 0.0f);
                            }

                            if (map[r, c] == 3)
                            {
                                ObjectPut = Instantiate(No03) as GameObject;
                                ObjectPut.transform.position = new Vector3(posX, posY, 0.0f);
                            }

                            if (map[r, c] == 4)
                            {
                                ObjectPut = Instantiate(No04) as GameObject;
                                ObjectPut.transform.position = new Vector3(posX, posY, 0.0f);
                            }

                            posY -= 1.0f;
                        }
                        bCreat[c] = false;
                    }
                }
                // csvの最後の時間になったらランダム生成に切り替える
                if (Second[column - 1] < nowSecond) { CsvOrRand = false; }
                break;

            case false:
                //==============================================
                //  ランダムで生成
                //==============================================
                // 生成されるまでの時間を減らす
                WaveCount += Time.deltaTime;
                // wave更新してよいか(生成までの時間が最小値じゃなかったら更新してよい)
                if (CreaterTime > MinCreaterTime)
                {
                    // wave更新タイミング
                    if (WaveCount >= Wave)
                    {
                        CreaterTime *= 0.9f;
                        Debug.Log("Wave更新 : " + CreaterTime);
                        WaveCount = 0;
                    }
                }
                else
                {
                    CreaterTime = MinCreaterTime;
                    Debug.Log("Wave更新 : STOP");
                }

                // レーザーの敵生成
                LaserWaveCount++;
                if (LaserWaveCount >= LaserWave)
                {
                    LaserWaveCount = 0;
                    EnemyPos = Random.Range(-4.2f, 4.2f);
                    Instantiate(No04, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                }


                // 敵生成
                if (nowSecond >= CreaterTime)
                {
                    AddFlame = 0.0f;
                    EnemyPos = Random.Range(-4.2f, 4.2f);
                    // --- 3種類の敵からランダムに生成 ---
                    // 同時に生成する数(1体か2体)
                    int DoubleOrSingle = Random.Range(0, 10);
                    switch (DoubleOrSingle)
                    {
                        // --- 2体同時生成 ---
                        case 0:
                            // 1体目
                            SelectEnemy = Random.Range(0, 3);
                            EnemyPos = Random.Range(-4.2f, 4.2f);
                            switch (SelectEnemy)
                            {
                                case 0:
                                    Instantiate(No02, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 1:
                                    Instantiate(No01, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 2:
                                    Instantiate(No03, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                default:
                                    break;
                            }
                            // 2体目
                            SelectEnemy = Random.Range(0, 3);
                            EnemyPos = Random.Range(-4.2f, 4.2f);
                            switch (SelectEnemy)
                            {
                                case 0:
                                    Instantiate(No02, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 1:
                                    Instantiate(No01, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 2:
                                    Instantiate(No03, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                default:
                                    break;
                            }
                            break;

                        // --- 1体生成 ---
                        default:
                            SelectEnemy = Random.Range(0, 3);
                            switch (SelectEnemy)
                            {
                                case 0:
                                    Instantiate(No02, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 1:
                                    Instantiate(No01, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 2:
                                    Instantiate(No03, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                default:
                                    break;
                            }
                            break;
                    }
                }
                break;

        }
    }

    //==============================================
    // CSVファイル読み込み
    //==============================================
    private void Load()
    {
        // CSVデータをstrに保存
        csvFile = Resources.Load(csvStage) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            line = reader.ReadLine();
            str = str + "," + line;
        }

        row = line.Length;
        column = CountChar(line, ',');

        str = str + ",";    // 最後に検索文字列の","を追記。これがないと最後の文字を取りこぼす。


        // CSVデータをマップ配列変数mapに保存
        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < column; c++)
            {
                try { iDat[0] = str.IndexOf(",", iDat[0]); }            // ","を検索
                catch { break; }

                try { iDat[1] = str.IndexOf(",", iDat[0] + 1); }        // 次の","を検索
                catch { break; }

                iDat[2] = iDat[1] - iDat[0] - 1;                        // 何文字取り出すか決定

                try { strget = str.Substring(iDat[0] + 1, iDat[2]); }   // iDat[2]文字ぶんだけ取り出す
                catch { break; }

                try { iDat[3] = int.Parse(strget); }                    // 取り出した文字列を数値型に変換
                catch { break; }

                map[r, c] = iDat[3];   // マップ用変数に保存。１とか６とか数字が入る
                iDat[0]++;             // 次のインデックスへ
            }
        }
    }

    //==============================================
    // ステージ生成
    //==============================================
    private void Creat()
    {
        // .csvの1行目の時間を取得
        for (int c = 0; c < column; c++)
        {
            Second[c] = map[0, c];
            bCreat[c] = true;
        }
    }

    //==============================================
    // 列数をカウントする
    //==============================================
    public static int CountChar(string s, char c)
    {
        return s.Length - s.Replace(c.ToString(), "").Length + 1;
    }
}