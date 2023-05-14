using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker_Bullet : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g
    private GameObject player;
    //�e�̃v���n�u�I�u�W�F�N�g
    public GameObject seeker;

    //�e�𔭎˂��邽�߂̂���
    private float targetTime;
    private float currentTime = 0;

    Vector3 pos;
    Vector2 vec;

    private Animator anime = null;

    private bool bullet;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        anime = GetComponent<Animator>();

        targetTime = Random.Range(0, 3);

        pos = new Vector3(0.0f, 0.0f, 0.0f);
        vec = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // �|�[�Y���͉������Ȃ�
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        //��b�o���Ƃɒe�𔭎˂���
        currentTime += Time.deltaTime;

        if (this.gameObject.tag == "Enemy")
        {
            // �G�l�~�[���������ꍇ
            if (targetTime < currentTime)
            {
                currentTime = 0;
                
                anime.SetBool("attack", true);

                targetTime = Random.Range(0, 3);
            }

            if (bullet)
            {
                //�G�̍��W��ϐ�pos�ɕۑ�
                pos = this.gameObject.transform.position;
                var rot = this.gameObject.transform.rotation;
                //�e�̃v���n�u���쐬
                var t = Instantiate(seeker) as GameObject;
                t.name = "Prefab_Seeker";
                //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
                t.transform.position = pos;
                //�G����v���C���[�Ɍ������x�N�g��������
                //�v���C���[�̈ʒu����G�̈ʒu�i�e�̈ʒu�j������
                if (player)
                {
                    vec = (player.transform.position - pos);
                }
                else
                {
                    vec = new Vector2(-10.0f, 0.0f);
                }
                //�e��RigidBody2D�R���|�l���g��velocity�ɐ�����߂��x�N�g�������ė͂�������
                t.GetComponent<Rigidbody2D>().velocity = vec;

                bullet = false;
            }
        }
        else
        {
            // �g�������������ꍇ
            //if (targetTime < currentTime)
            //{
            //    currentTime = 0;
            //    �G�̍��W��ϐ�pos�ɕۑ�
            //    pos = this.gameObject.transform.position;
            //    var rot = this.gameObject.transform.rotation;

            //    �e�̃v���n�u���쐬
            //    var t = Instantiate(seeker) as GameObject;
            //    t.name = "Prefab_Seeker";
            //    �e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
            //    t.transform.position = pos;
            //    �G����v���C���[�Ɍ������x�N�g��������
            //    �v���C���[�̈ʒu����G�̈ʒu�i�e�̈ʒu�j������
            //    vec = (player.transform.position - pos);
            //    �e��RigidBody2D�R���|�l���g��velocity�ɐ�����߂��x�N�g�������ė͂�������
            //    t.GetComponent<Rigidbody2D>().velocity = vec;
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