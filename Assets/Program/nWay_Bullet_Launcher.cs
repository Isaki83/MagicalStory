using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nWay_Bullet_Launcher : MonoBehaviour
{
    //�@�e�I�u�W�F�N�g
    public GameObject Bullet;
    // �x�N�g���̌����A�g�U�p�x�A��x�ɔ��˂���e�̐�
    public float _Velocity_0, Degree, Angle_Split;
    // �e�����ł����p�x
    float _theta;
    float PI = Mathf.PI;
    // ���ˊԊu
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
            // �G�l�~�[���������ꍇ
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

                    //n-way�e�̒[����[�܂ł̊p�x
                    float AngleRange = PI * (Degree / 180);

                    //�e�C���X�^���X�ɓn���p�x�̌v�Z
                    if (Angle_Split > 1) _theta = (AngleRange / (Angle_Split - 1)) * i - 0.5f * AngleRange;
                    else _theta = 0;

                    //�e�C���X�^���X���擾���A�����Ɣ��ˊp�x��^����
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
            // �g�������������ꍇ
            //if (targetTime < currentTime)
            //{
            //    currentTime = 0.0f;
            //    for (int i = 0; i <= (Angle_Split - 1); i++)
            //    {

            //        //n-way�e�̒[����[�܂ł̊p�x
            //        float AngleRange = PI * (Degree / 180);

            //        //�e�C���X�^���X�ɓn���p�x�̌v�Z
            //        if (Angle_Split > 1) _theta = (AngleRange / (Angle_Split - 1)) * i - 0.5f * AngleRange;
            //        else _theta = 0;

            //        //�e�C���X�^���X���擾���A�����Ɣ��ˊp�x��^����
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