using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeAndScore : MonoBehaviour
{
    // �̗͂ƃX�R�A
    [SerializeField] private float life = 1.0f;
    [SerializeField] private int score = 10;

    // ���ŃG�t�F�N�g
    [SerializeField] private GameObject DeathEffect;

    /*  SE   */
    AudioSource audioSource;
    // ��e
    public AudioClip HitSE;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ULT_Attack.UltHit)
        {
            // ���ŃG�t�F�N�g����
            Instantiate(DeathEffect,
                    new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                    Quaternion.identity);
            // �X�R�A���Z
            Score.AddScore(score);
            // �K�E�Z�J�E���g���Z
            Player_ULT.AddUltCnt();
            // �G�l�~�[�폜
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // �e�폜
            Destroy(collision.gameObject);
            // �̗͌��炷
            if(collision.name == "Player_Bullet")
                life = life - Bullet.Bullet_Power;
            if (collision.name == "Fam_Black_Bullet")
                life = life - FamBulletPower.BlackBulletPower;
            if (collision.name == "Fam_Red_Bullet")
                life = life - FamBulletPower.BlueBulletPower;
            if (collision.name == "Fam_Blue_Bullet")
                life = life - FamBulletPower.RedBulletPower;
            // �̗͂�0�Ȃ�
            if (life <= 0.0f)
            {
                // ���ŃG�t�F�N�g����
                Instantiate(DeathEffect,
                        new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                        Quaternion.identity);
                // �X�R�A���Z
                Score.AddScore(score);
                // �K�E�Z�J�E���g���Z
                Player_ULT.AddUltCnt();
                // �G�l�~�[�폜
                Destroy(gameObject);
            }
            // �̗͂��c���Ă�����
            else
            {
                // �q�b�gSE��炷
                audioSource.PlayOneShot(HitSE, VolumeControl.SE_Volume);
            }
        }
    }
}
