using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULT_Attack : MonoBehaviour
{
    public Enemy enemy;

    // ��������Ă���̃t���[�����J�E���g
    private int cnt;
    // �I�u�W�F�N�g���Ŏ��Ɏ��ԓ������Ă����H
    public bool TimeMove = false;

    public static bool UltHit = false;

    // Start is called before the first frame update
    void Start()
    {
        cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cnt++;

        if (cnt >= 50)
        {
            if(TimeMove)
                Time.timeScale = 1.0f;
        }
        if (cnt >= 60)
        {
            UltHit = false;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (cnt >= 30)
        {
            UltHit = true;
        }
    }
}
