using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Familiar : MonoBehaviour
{
    // 列挙型宣言
    enum Status // 使い魔のステータス
    {
        Caught = 0, // 捕まっているとき
        Save = 1,   // 助け出したとき
        Follow = 2, // 追従時
    }

    // 変数宣言
    SpriteRenderer fami;                        // 使い魔のスプライトレンダラー
    public int BulletTime = 30;                 // 弾を打ち出す間隔
    public static bool GoBullet = false;        // 弾を打ち出してよいか
    GameObject Player;                          // プレイヤーオブジェクト
    Vector3 OldPlayerPos;                       // プレイヤーの1フレーム前の座標
    public int MaxFamiliar = 20;                // 開放した使い魔の最大数
    int time;                                   // プレイヤーが動いてからの経過時間
    bool Flg = true;                            // プレイヤーと同じ位置にいるか
    ManagerPosFamiliar m_PosFamiliar;           // 使い魔の位置を管理するスクリプトの呼び出し用
    Vector2 FixedPosition;                      // 使い魔の定位置
    Vector2 CalculationFPos;                    // 計算後の定位置
    int FamiliarNum = -1;                       // 使い魔の個体番号
    Createfamiliar createFamiliar;              // 使い魔生成スクリプト呼び出し用
    float[] MoveSpeed = { 0.05f, 0.2f, 0.08f }; // 移動速度管理配列
    Status status;                              // 使い魔のステータス管理
    bool[] SavePosFlg;                          // 使い魔の救出後の位置が定位置か調べるフラグ
    int SaveTimer;                              // 助けた際のタイマー

    public int CageHP = 5;                      // 檻の体力
    

    // 消滅エフェクト
    public GameObject DeathEffect;

    // ===============================
    // 初期化関数
    // ===============================
    void Start()
    {
        Debug.Log("使い魔が生成されました");
        // 使い魔のスプライトレンダラーを取得
        fami = GetComponent<SpriteRenderer>();
        // シーンからPlayerタグのオブジェクトを探してPlayer変数に代入
        Player = GameObject.FindWithTag("Player");
        m_PosFamiliar = GameObject.FindWithTag("Player").GetComponent<ManagerPosFamiliar>();
        // Playerタグのオブジェクトから割り当てられているCreateFamiliarを代入
        createFamiliar = GameObject.FindWithTag("Player").GetComponent<Createfamiliar>();
        // 生成した際のステータス,初期値は必ず捕まっている
        status = Status.Caught;
        // 変数の配列の数を指定
        SavePosFlg = new bool[4];
        // 変数の中を初期化
        for (int i = 0; i < 4; ++i)
        {
            SavePosFlg[i] = false;
        }
        // 開放した使い魔の数をリセット
        

        // 最初は透明
        fami.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // ===============================
    // 更新関数
    // ===============================
    void Update()
    {
        // *****ポーズ中は何もしない*****
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;
        //Debug.Log("現在の使い魔のステータスです" + status);
        // *****テータスに応じて処理を変える*****
        switch (status)
        {
            // 使い魔のステータスが「捕まっている」場合
            case Status.Caught:
                // 使い魔を定速で移動させる
                this.transform.Translate(-MoveSpeed[(int)Status.Caught], 0.0f, 0.0f);
                // カメラ外にオブジェクトがいくと
                if (!GetComponent<Renderer>().isVisible)
                {
                    // このオブジェクト削除
                    Destroy(this.gameObject);
                    createFamiliar.SetLimitFamiliar(false);
                }
                break;

            // 使い魔のステータスが「助けられた」場合
            case Status.Save:
                // 使い魔の目的地を計算
                CalculationFPos.x = Player.transform.position.x + FixedPosition.x;
                CalculationFPos.y = Player.transform.position.y + FixedPosition.y;

                // タイマー減算
                SaveTimer--;

                // 使い魔の位置がプレイヤーより右にいる場合
                if (this.transform.position.x >= CalculationFPos.x)
                {
                    this.transform.Translate(-MoveSpeed[(int)Status.Save], 0.0f, 0.0f);
                }
                else
                {
                    SavePosFlg[0] = true;
                }
                // 使い魔の位置がプレイヤーより左にいる場合
                if (this.transform.position.x <= CalculationFPos.x)
                {
                    this.transform.Translate(MoveSpeed[(int)Status.Save], 0.0f, 0.0f);
                }
                else
                {
                    SavePosFlg[1] = true;
                }
                // 使い魔の位置がプレイヤーより上にいる場合
                if (this.transform.position.y >= CalculationFPos.y)
                {
                    this.transform.Translate(0.0f, -MoveSpeed[(int)Status.Save], 0.0f);
                }
                else
                {
                    SavePosFlg[2] = true;
                }
                // 使い魔の位置がプレイヤーより下にいる場合
                if (this.transform.position.y <= CalculationFPos.y)
                {
                    this.transform.Translate(0.0f, MoveSpeed[(int)Status.Save], 0.0f);
                }
                else
                {
                    SavePosFlg[3] = true;
                }

                for (int i = 0, j = 0; i < 4; ++i)
                {
                    if (SavePosFlg[i])
                    {
                        j++;
                    }
                    if (j >= 2 && SaveTimer <= 0)
                    {
                        status = Status.Follow;
                    }
                }
                break;

            // 使い魔のステータスが追従のとき
            case Status.Follow:
                // 使い魔の向きを反転
                fami.flipX = false;

                BulletTime++; // 弾発射カウントをプラス

                // 一定時間以上になったら弾を発射
                if (BulletTime >= 90 && Input.GetKey(KeyCode.Z))
                {
                    BulletTime = 0; //カウントを0に
                    GoBullet = true;
                }
                else
                {
                    GoBullet = false;
                }

                // プレイヤーに追従
                // プレイヤーが動いた場合1フレーム前の位置を保存
                if (OldPlayerPos != Player.transform.position)
                {
                    OldPlayerPos = Player.transform.position;
                    CalculationFPos.x = Player.transform.position.x + FixedPosition.x;
                    CalculationFPos.y = Player.transform.position.y + FixedPosition.y;
                }
                // 動いていないかつ使い魔が所定位置についている場合はタイマーをリセット
                else if ((OldPlayerPos == Player.transform.position) && Flg)
                {
                    time = 0;
                }

                // タイマーが一定時間以上になる
                if (time >= 60)
                {
                    // 使い魔の位置がプレイヤーより右にいる場合
                    if (this.transform.position.x > CalculationFPos.x)
                    {
                        this.transform.Translate(-MoveSpeed[(int)Status.Follow], 0.0f, 0.0f);
                    }
                    // 使い魔の位置がプレイヤーより左にいる場合
                    if (this.transform.position.x < CalculationFPos.x)
                    {
                        this.transform.Translate(MoveSpeed[(int)Status.Follow], 0.0f, 0.0f);
                    }
                    // 使い魔の位置がプレイヤーより上にいる場合
                    if (this.transform.position.y > CalculationFPos.y)
                    {
                        this.transform.Translate(0.0f, -MoveSpeed[(int)Status.Follow], 0.0f);
                    }
                    // 使い魔の位置がプレイヤーより下にいる場合
                    if (this.transform.position.y < CalculationFPos.y)
                    {
                        this.transform.Translate(0.0f, MoveSpeed[(int)Status.Follow], 0.0f);
                    }
                }
                if ((this.transform.position.x == CalculationFPos.x) && (this.transform.position.y == CalculationFPos.y))
                {
                    Debug.Log("使い魔の現在地" + this.transform.position);
                    Debug.Log("プレイヤーの現在地" + Player.transform.position);
                    Debug.Log("使い魔の目的地" + CalculationFPos);
                    Flg = true;
                }
                else
                {
                    Flg = false;
                }

                // タイマー更新
                time++;
                break;
            default:
                break;
        }

        // 個体番号と現在の使い魔の数が同じだったら
        if (Player_Bullet.Hit_DeathFamiliar && (FamiliarNum == m_PosFamiliar.GetNowNumFamiliar()))
        {
            Destroy(this.gameObject);

            Instantiate(DeathEffect,
                    new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                    Quaternion.identity);

            m_PosFamiliar.FalseUseFlag(m_PosFamiliar.GetNowNumFamiliar());
            m_PosFamiliar.subNowNumFamiliar();
            Player_Bullet.Hit_DeathFamiliar = false;
        }

    }

    // ===============================
    // 生きてるか判定するためにフラグを渡す
    // ===============================
    public int GetAliveFlg()
    {
        return (int)status;
    }

    // ===============================
    // 開放した際にフラグを反転させる
    // ===============================
    public void SetAliveFlg()
    {
        // 使い魔のステータスをSaveに変える
        status = Status.Save;
        // 個体番号を取得する
        FamiliarNum = m_PosFamiliar.SetUseTrueFlg();
        // 個体番号から定位置を取得する
        FixedPosition = m_PosFamiliar.GetFamiliarPos(FamiliarNum);
        // 経過時間を61にして自動的に定位置に移動するようにする
        time = 61;
        // 助けた際のタイマーをセット
        SaveTimer = 30;
        createFamiliar.SetLimitFamiliar(false);
    }

    // ===============================
    // Bulletとの当たり判定
    // ===============================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 弾とあたったか
        if (collision.gameObject.tag == "Bullet")
        {
            // この使い魔のフラグがfalseなら
            if (GetAliveFlg() == (int)Status.Caught)
            {
                if (CageHP <= 1)
                {
                    Debug.Log("使い魔を救出しました");
                    // 使い魔のステータスをSaveに変える
                    SetAliveFlg();

                    // 子オブジェクト(檻)を消す
                    foreach (Transform child in gameObject.transform)
                    {
                        Destroy(child.gameObject);
                    }

                    // 透明解除
                    fami.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else
                {
                    foreach (Transform child in gameObject.transform)
                    {
                        Animator anim =  child.GetComponent<Animator>();
                        anim.SetBool("dmg", true);
                    }
                    

                    CageHP--;
                }
                // 弾オブジェクトを削除
                Destroy(collision.gameObject);
            }
        }
        // 必殺技にあたったか
        if (collision.gameObject.tag == "ULT_Bullet")
        {
            // この使い魔のフラグがfalseなら
            if (GetAliveFlg() == (int)Status.Caught)
            {
                Debug.Log("使い魔を救出しました");
                // 使い魔のステータスをSaveに変える
                SetAliveFlg();

                // 子オブジェクト(檻)を消す
                foreach (Transform child in gameObject.transform)
                {
                    Destroy(child.gameObject);
                }

                // 透明解除
                fami.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}