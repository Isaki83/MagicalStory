using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    //　生成タイマ
    private float targetTime;
    private float currentTime = 0.0f;

    public GameObject obj;
    private Animator anime = null;

    private bool bullet;
    

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();

        targetTime = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        // ポーズ中は何もしない
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        currentTime += Time.deltaTime;
        if (targetTime < currentTime)
        {
            anime.SetBool("attack", true);

            currentTime = 0.0f;

            targetTime = Random.Range(0, 3);
        }

        if (bullet)
        {
            var Bullet = Instantiate(obj,
                            new Vector3(this.transform.position.x - 1.0f, this.transform.position.y + 0.3f, this.transform.position.z),
                            Quaternion.identity);
            Bullet.name = "Enemy_Bullet";

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
