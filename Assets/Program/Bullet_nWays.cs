using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_nWays : MonoBehaviour
{
    public float Velocity_0, theta;
    public float Speed = 0.05f;

    Rigidbody2D rid2d;
    void Start()
    {
        if (this.tag == "Bullet")
            this.name = "Fam_Red_Bullet";

        //Rigidbody�擾
        rid2d = GetComponent<Rigidbody2D>();
        //�p�x���l�����Ēe�̑��x�v�Z
        Vector2 bulletV = rid2d.velocity;
        bulletV.x = Velocity_0 * Mathf.Cos(theta) * Speed;
        bulletV.y = Velocity_0 * Mathf.Sin(theta) * Speed;
        rid2d.velocity = bulletV;
    }

    void Update()
    {
        // �J�����O�ɏo����폜
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }
}