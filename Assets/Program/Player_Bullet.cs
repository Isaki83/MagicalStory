using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    // 使い魔の個体番号を管理するスクリプトの呼び出し用
    ManagerPosFamiliar m_NuFamiliar;
    // プレイヤー移動の変数生成
    public float MoveSpeed = 0.5f;
    // プレイヤーの移動範囲制限
    public float MaxPosX = 8.0f;
    public float MinPosX = -8.0f;
    public float MaxPosY = 4.0f;
    public float MinPosY = -4.0f;
    // 生成する弾を選択
    public GameObject Bulletobj;
    // 弾を生成するタイマー
    int BulletTimer = 0;
    // プレイヤーの体力
    public int HP = 1;
    // 敵の弾に当たったか
    private bool Hit;
    public static bool Hit_DeathFamiliar;
    /* 点滅 */
    // スプライトレンダラー
    SpriteRenderer sp;
    // 周期
    public int FlashingCycle = 30;
    // カウント
    private int FlashingCnt = 0;
    // 無敵時間
    public int InvincibleTime = 120;
    // ↑のカウント用
    private int InvincibleCnt;
    // 無敵(デバッグ用)
    private bool Invincible;

    // ヒットストップ時間
    private int HitStop = 0;

    // 消滅エフェクト
    public GameObject DeathEffect;

    // シーン遷移してよいか
    public static bool ChangeScene = false;

    // 射撃のSE
    public AudioClip ShotSE;
    // 被弾のSE
    public AudioClip HitSE;
    AudioSource audioSource;
    private float Volum;

    // アニメーション用
    private Animator anime = null;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        ChangeScene = false;
        Hit = false;
        Hit_DeathFamiliar = false;

        InvincibleCnt = InvincibleTime;
        Invincible = false;

        m_NuFamiliar = GameObject.FindWithTag("Player").GetComponent<ManagerPosFamiliar>();

        // コンポーネント取得　
        audioSource = GetComponent<AudioSource>();
        // 音量変更
        Volum = audioSource.volume;
        anime = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeScene)
        {
            Debug.Log("ヒットストップ");
            HitStop++;
            if (HitStop > 60)
            {
                Time.timeScale = 1.0f;

                Destroy(this.gameObject);

                Instantiate(DeathEffect,
                            new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                            Quaternion.identity);
            }
        }

        // ポーズ中は何もしない
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        // 弾生成タイマー更新
        BulletTimer++;
        // プレイヤー移動
        // 上
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, MoveSpeed, 0.0f);
        }
        // 下
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, -MoveSpeed, 0.0f);
        }
        // 左
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-MoveSpeed, 0.0f, 0.0f);
            anime.SetBool("front", false);
            anime.SetBool("back", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anime.SetBool("back", false);
        }
        // 右
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(MoveSpeed, 0.0f, 0.0f);
            anime.SetBool("front", true);
            anime.SetBool("back", false);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anime.SetBool("front", false);
        }

        // 移動範囲に制限をかける
        var limitPos = transform.position;
        limitPos.x = Mathf.Clamp(limitPos.x, MinPosX, MaxPosX);
        limitPos.y = Mathf.Clamp(limitPos.y, MinPosY, MaxPosY);
        transform.position = limitPos;

        // 弾を生成
        if (Input.GetKey(KeyCode.Z) && BulletTimer >= 30)
        {
            //audioSource.PlayOneShot(ShotSE, VolumeControl.SE_Volume);
            BulletTimer = 0;
            Vector2 pos = this.transform.position;
            pos.x += 1;
            pos.y -= 0.4f;

            var PlayerBullet = Instantiate(Bulletobj,
                new Vector3(pos.x, pos.y, this.transform.position.z),
                Quaternion.identity);
            PlayerBullet.name = "Player_Bullet";
            Debug.Log("弾を生成しました");
            //Volum = Fam_Count.MaxVolum;
            //audioSource.volume = Volum;
            audioSource.PlayOneShot(ShotSE, VolumeControl.SE_Volume);
        }

        // 弾に当たったら点滅
        if (Hit)
        {
            InvincibleCnt++;
            if (InvincibleCnt < InvincibleTime)
            {
                FlashingCnt++;
                if (FlashingCnt >= FlashingCycle)
                {
                    sp.enabled = !sp.enabled;
                    FlashingCnt = 0;
                }
            }
            else
            {
                sp.enabled = true;
                Hit = false;
            }
        }

        // 無敵(デバッグ用)
        if (Input.GetKeyDown(KeyCode.Slash))
            Invincible = !Invincible;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "E_Bullet")
        {
            if (!Hit && !Invincible)
            {
                if (m_NuFamiliar.GetNowNumFamiliar() > 0)
                {
                    Debug.Log("身代わり");
                    InvincibleCnt = 0;
                    Hit = true;
                    Hit_DeathFamiliar = true;

                    audioSource.PlayOneShot(HitSE, VolumeControl.SE_Volume);
                }
                else
                {
                    HP -= 1;
                }

                if (HP <= 0)
                {
                    ChangeScene = true;
                    Time.timeScale = 0.0f;
                }

                if (!(other.name == "Enemy_Laser"))
                    Destroy(other.gameObject);
            }
        }
    }
}