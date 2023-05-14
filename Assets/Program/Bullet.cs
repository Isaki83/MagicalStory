using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 弾の移動速度
    public float MoveSpeed = 0.01f;
    public Player_Bullet player_bullet;
    public static bool ScoreFlag;

    // 弾の威力
    public static float Bullet_Power;
    // 弾が使い魔のものかどうかを判定するための文字列
    private string Familiar_Name = "Familiar_Bullet";

    // 弾の威力
    public float Player_Power = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ポーズ中は何もしない
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        if (this.gameObject.name == "Player_Bullet")
        {
            // 弾を移動
            this.transform.Translate(MoveSpeed, 0.0f, 0.0f);
            // 威力設定
            if(gameObject.name == "Player_Bullet")
            {
                Bullet_Power = Player_Power;
            }
        }

        // カメラ外に出たら削除
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }
}
