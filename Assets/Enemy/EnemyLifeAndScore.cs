using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeAndScore : MonoBehaviour
{
    // 体力とスコア
    [SerializeField] private float life = 1.0f;
    [SerializeField] private int score = 10;

    // 消滅エフェクト
    [SerializeField] private GameObject DeathEffect;

    /*  SE   */
    AudioSource audioSource;
    // 被弾
    public AudioClip HitSE;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ULT_Attack.UltHit)
        {
            // 消滅エフェクト生成
            Instantiate(DeathEffect,
                    new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                    Quaternion.identity);
            // スコア加算
            Score.AddScore(score);
            // 必殺技カウント加算
            Player_ULT.AddUltCnt();
            // エネミー削除
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // 弾削除
            Destroy(collision.gameObject);
            // 体力減らす
            if(collision.name == "Player_Bullet")
                life = life - Bullet.Bullet_Power;
            if (collision.name == "Fam_Black_Bullet")
                life = life - FamBulletPower.BlackBulletPower;
            if (collision.name == "Fam_Red_Bullet")
                life = life - FamBulletPower.BlueBulletPower;
            if (collision.name == "Fam_Blue_Bullet")
                life = life - FamBulletPower.RedBulletPower;
            // 体力が0なら
            if (life <= 0.0f)
            {
                // 消滅エフェクト生成
                Instantiate(DeathEffect,
                        new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                        Quaternion.identity);
                // スコア加算
                Score.AddScore(score);
                // 必殺技カウント加算
                Player_ULT.AddUltCnt();
                // エネミー削除
                Destroy(gameObject);
            }
            // 体力が残っていたら
            else
            {
                // ヒットSEを鳴らす
                audioSource.PlayOneShot(HitSE, VolumeControl.SE_Volume);
            }
        }
    }
}
