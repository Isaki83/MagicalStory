using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nWay_Bullet_Launcher : MonoBehaviour
{
    //　弾オブジェクト
    public GameObject Bullet;
    // ベクトルの向き、拡散角度、一度に発射する弾の数
    public float _Velocity_0, Degree, Angle_Split;
    // 弾が飛んでいく角度
    float _theta;
    float PI = Mathf.PI;
    // 発射間隔
    private float targetTime;
    private float currentTime = 0.0f;

    private Animator anime = null;

    private bool bullet;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();

        targetTime = Random.Range(0, 3);
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (this.gameObject.tag == "Enemy")
        {
            // エネミーが撃った場合
            if (targetTime < currentTime)
            {
                anime.SetBool("attack", true);

                currentTime = 0.0f;

                targetTime = Random.Range(0, 3);
            }

            if (bullet)
            {
                for (int i = 0; i <= (Angle_Split - 1); i++)
                {

                    //n-way弾の端から端までの角度
                    float AngleRange = PI * (Degree / 180);

                    //弾インスタンスに渡す角度の計算
                    if (Angle_Split > 1) _theta = (AngleRange / (Angle_Split - 1)) * i - 0.5f * AngleRange;
                    else _theta = 0;

                    //弾インスタンスを取得し、初速と発射角度を与える
                    GameObject Bullet_obj = (GameObject)Instantiate(Bullet, transform.position, transform.rotation);
                    Bullet_obj.name = "nWay_Bullet";
                    Bullet_nWays bullet_cs = Bullet_obj.GetComponent<Bullet_nWays>();
                    bullet_cs.theta = _theta;
                    bullet_cs.Velocity_0 = -_Velocity_0;
                }

                bullet = false;
            }
        }
        else
        {
            // 使い魔が撃った場合
            //if (targetTime < currentTime)
            //{
            //    currentTime = 0.0f;
            //    for (int i = 0; i <= (Angle_Split - 1); i++)
            //    {

            //        //n-way弾の端から端までの角度
            //        float AngleRange = PI * (Degree / 180);

            //        //弾インスタンスに渡す角度の計算
            //        if (Angle_Split > 1) _theta = (AngleRange / (Angle_Split - 1)) * i - 0.5f * AngleRange;
            //        else _theta = 0;

            //        //弾インスタンスを取得し、初速と発射角度を与える
            //        GameObject Bullet_obj = (GameObject)Instantiate(Bullet, transform.position, transform.rotation);
            //        Bullet_obj.name = "nWay_Bullet";
            //        Bullet_nWays bullet_cs = Bullet_obj.GetComponent<Bullet_nWays>();
            //        bullet_cs.theta = _theta;
            //        bullet_cs.Velocity_0 = -_Velocity_0;
            //    }
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