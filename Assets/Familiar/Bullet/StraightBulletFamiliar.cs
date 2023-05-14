using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBulletFamiliar : MonoBehaviour
{
    // �e�̈ړ����x
    public float MoveSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Fam_Black_Bullet";
    }

    // Update is called once per frame
    void Update()
    {
        // �e���ړ�
        this.transform.Translate(MoveSpeed, 0.0f, 0.0f);

        // �J�����O�ɏo����폜
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }
}
