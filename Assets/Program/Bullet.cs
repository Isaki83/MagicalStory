using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �e�̈ړ����x
    public float MoveSpeed = 0.01f;
    public Player_Bullet player_bullet;
    public static bool ScoreFlag;

    // �e�̈З�
    public static float Bullet_Power;
    // �e���g�����̂��̂��ǂ����𔻒肷�邽�߂̕�����
    private string Familiar_Name = "Familiar_Bullet";

    // �e�̈З�
    public float Player_Power = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �|�[�Y���͉������Ȃ�
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        if (this.gameObject.name == "Player_Bullet")
        {
            // �e���ړ�
            this.transform.Translate(MoveSpeed, 0.0f, 0.0f);
            // �З͐ݒ�
            if(gameObject.name == "Player_Bullet")
            {
                Bullet_Power = Player_Power;
            }
        }

        // �J�����O�ɏo����폜
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }
}
