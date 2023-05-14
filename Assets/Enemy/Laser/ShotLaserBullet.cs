using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLaserBullet : MonoBehaviour
{
    //　生成タイマ
    private float targetTime;
    private float currentTime = 0.0f;

    public GameObject obj;
    public GameObject Predict;
    private Animator anime = null;

    private bool bullet;


    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();

        targetTime = 120.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // ポーズ中は何もしない
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        currentTime++;
        if (!anime.GetBool("attack"))
        {
            if (targetTime < currentTime)
            {
                Instantiate(Predict,
                    new Vector3(this.transform.position.x - 2.3f, this.transform.position.y + 0.1f, this.transform.position.z),
                    Quaternion.identity);

                anime.SetBool("attack", true);

                targetTime = Random.Range(0.3f, 0.5f);
                Debug.Log("targetTime " + targetTime);
            }
        }
        else
        {
            currentTime = 0.0f;
        }

        if (bullet)
        {
            var Bullet = Instantiate(obj,
                            new Vector3(this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z),
                            Quaternion.identity);
            Bullet.name = "Enemy_Laser";

            bullet = false;
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
