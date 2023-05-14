using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingBulletFamiliar : MonoBehaviour
{
    public float speed = 0.5f;
    private GameObject[] targets;
    private GameObject closeEnemy;

    Vector2 vec;

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Fam_Blue_Bullet";

        // 画面上の全ての敵の情報を取得
        targets = GameObject.FindGameObjectsWithTag("Enemy");

        // 初期値
        float closeDist = 1000;

        if (!(targets.Length == 0))
        {
            foreach (GameObject t in targets)
            {
                // 弾と敵までの距離を計測
                float tDist = Vector3.Distance(this.transform.position, t.transform.position);

                // もし初期値よりも計測した敵までの距離の方が近かったら
                if (closeDist > tDist)
                {
                    // closeDistをtDistに置き換える
                    closeDist = tDist;

                    // 一番近い敵の情報をcloseEnemyに格納する
                    closeEnemy = t;
                }
            }

            vec = closeEnemy.transform.position - this.transform.position;
        }
        else
        {
            vec = new Vector2(10.0f, 0.0f);
        }
        this.GetComponent<Rigidbody2D>().velocity = vec;
    }

    // Update is called once per frame
    void Update()
    {
        // カメラ外に出たら削除
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }
}
