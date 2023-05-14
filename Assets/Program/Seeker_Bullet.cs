using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker_Bullet : MonoBehaviour
{
    //プレイヤーオブジェクト
    private GameObject player;
    //弾のプレハブオブジェクト
    public GameObject seeker;

    //弾を発射するためのもの
    private float targetTime;
    private float currentTime = 0;

    Vector3 pos;
    Vector2 vec;

    private Animator anime = null;

    private bool bullet;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        anime = GetComponent<Animator>();

        targetTime = Random.Range(0, 3);

        pos = new Vector3(0.0f, 0.0f, 0.0f);
        vec = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // ポーズ中は何もしない
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        //一秒経つごとに弾を発射する
        currentTime += Time.deltaTime;

        if (this.gameObject.tag == "Enemy")
        {
            // エネミーが撃った場合
            if (targetTime < currentTime)
            {
                currentTime = 0;
                
                anime.SetBool("attack", true);

                targetTime = Random.Range(0, 3);
            }

            if (bullet)
            {
                //敵の座標を変数posに保存
                pos = this.gameObject.transform.position;
                var rot = this.gameObject.transform.rotation;
                //弾のプレハブを作成
                var t = Instantiate(seeker) as GameObject;
                t.name = "Prefab_Seeker";
                //弾のプレハブの位置を敵の位置にする
                t.transform.position = pos;
                //敵からプレイヤーに向かうベクトルをつくる
                //プレイヤーの位置から敵の位置（弾の位置）を引く
                if (player)
                {
                    vec = (player.transform.position - pos);
                }
                else
                {
                    vec = new Vector2(-10.0f, 0.0f);
                }
                //弾のRigidBody2Dコンポネントのvelocityに先程求めたベクトルを入れて力を加える
                t.GetComponent<Rigidbody2D>().velocity = vec;

                bullet = false;
            }
        }
        else
        {
            // 使い魔が撃った場合
            //if (targetTime < currentTime)
            //{
            //    currentTime = 0;
            //    敵の座標を変数posに保存
            //    pos = this.gameObject.transform.position;
            //    var rot = this.gameObject.transform.rotation;

            //    弾のプレハブを作成
            //    var t = Instantiate(seeker) as GameObject;
            //    t.name = "Prefab_Seeker";
            //    弾のプレハブの位置を敵の位置にする
            //    t.transform.position = pos;
            //    敵からプレイヤーに向かうベクトルをつくる
            //    プレイヤーの位置から敵の位置（弾の位置）を引く
            //    vec = (player.transform.position - pos);
            //    弾のRigidBody2Dコンポネントのvelocityに先程求めたベクトルを入れて力を加える
            //    t.GetComponent<Rigidbody2D>().velocity = vec;
            //}
        }
    }

    private void AttackFin()
    {
        anime.SetBool("attack", false);
    }

    public void ShotBullet()
    {
        bullet = true;
    }
}