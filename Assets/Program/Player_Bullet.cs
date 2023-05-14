using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    // �g�����̌̔ԍ����Ǘ�����X�N���v�g�̌Ăяo���p
    ManagerPosFamiliar m_NuFamiliar;
    // �v���C���[�ړ��̕ϐ�����
    public float MoveSpeed = 0.5f;
    // �v���C���[�̈ړ��͈͐���
    public float MaxPosX = 8.0f;
    public float MinPosX = -8.0f;
    public float MaxPosY = 4.0f;
    public float MinPosY = -4.0f;
    // ��������e��I��
    public GameObject Bulletobj;
    // �e�𐶐�����^�C�}�[
    int BulletTimer = 0;
    // �v���C���[�̗̑�
    public int HP = 1;
    // �G�̒e�ɓ���������
    private bool Hit;
    public static bool Hit_DeathFamiliar;
    /* �_�� */
    // �X�v���C�g�����_���[
    SpriteRenderer sp;
    // ����
    public int FlashingCycle = 30;
    // �J�E���g
    private int FlashingCnt = 0;
    // ���G����
    public int InvincibleTime = 120;
    // ���̃J�E���g�p
    private int InvincibleCnt;
    // ���G(�f�o�b�O�p)
    private bool Invincible;

    // �q�b�g�X�g�b�v����
    private int HitStop = 0;

    // ���ŃG�t�F�N�g
    public GameObject DeathEffect;

    // �V�[���J�ڂ��Ă悢��
    public static bool ChangeScene = false;

    // �ˌ���SE
    public AudioClip ShotSE;
    // ��e��SE
    public AudioClip HitSE;
    AudioSource audioSource;
    private float Volum;

    // �A�j���[�V�����p
    private Animator anime = null;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        ChangeScene = false;
        Hit = false;
        Hit_DeathFamiliar = false;

        InvincibleCnt = InvincibleTime;
        Invincible = false;

        m_NuFamiliar = GameObject.FindWithTag("Player").GetComponent<ManagerPosFamiliar>();

        // �R���|�[�l���g�擾�@
        audioSource = GetComponent<AudioSource>();
        // ���ʕύX
        Volum = audioSource.volume;
        anime = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeScene)
        {
            Debug.Log("�q�b�g�X�g�b�v");
            HitStop++;
            if (HitStop > 60)
            {
                Time.timeScale = 1.0f;

                Destroy(this.gameObject);

                Instantiate(DeathEffect,
                            new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                            Quaternion.identity);
            }
        }

        // �|�[�Y���͉������Ȃ�
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        // �e�����^�C�}�[�X�V
        BulletTimer++;
        // �v���C���[�ړ�
        // ��
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, MoveSpeed, 0.0f);
        }
        // ��
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, -MoveSpeed, 0.0f);
        }
        // ��
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-MoveSpeed, 0.0f, 0.0f);
            anime.SetBool("front", false);
            anime.SetBool("back", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anime.SetBool("back", false);
        }
        // �E
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(MoveSpeed, 0.0f, 0.0f);
            anime.SetBool("front", true);
            anime.SetBool("back", false);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anime.SetBool("front", false);
        }

        // �ړ��͈͂ɐ�����������
        var limitPos = transform.position;
        limitPos.x = Mathf.Clamp(limitPos.x, MinPosX, MaxPosX);
        limitPos.y = Mathf.Clamp(limitPos.y, MinPosY, MaxPosY);
        transform.position = limitPos;

        // �e�𐶐�
        if (Input.GetKey(KeyCode.Z) && BulletTimer >= 30)
        {
            //audioSource.PlayOneShot(ShotSE, VolumeControl.SE_Volume);
            BulletTimer = 0;
            Vector2 pos = this.transform.position;
            pos.x += 1;
            pos.y -= 0.4f;

            var PlayerBullet = Instantiate(Bulletobj,
                new Vector3(pos.x, pos.y, this.transform.position.z),
                Quaternion.identity);
            PlayerBullet.name = "Player_Bullet";
            Debug.Log("�e�𐶐����܂���");
            //Volum = Fam_Count.MaxVolum;
            //audioSource.volume = Volum;
            audioSource.PlayOneShot(ShotSE, VolumeControl.SE_Volume);
        }

        // �e�ɓ���������_��
        if (Hit)
        {
            InvincibleCnt++;
            if (InvincibleCnt < InvincibleTime)
            {
                FlashingCnt++;
                if (FlashingCnt >= FlashingCycle)
                {
                    sp.enabled = !sp.enabled;
                    FlashingCnt = 0;
                }
            }
            else
            {
                sp.enabled = true;
                Hit = false;
            }
        }

        // ���G(�f�o�b�O�p)
        if (Input.GetKeyDown(KeyCode.Slash))
            Invincible = !Invincible;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "E_Bullet")
        {
            if (!Hit && !Invincible)
            {
                if (m_NuFamiliar.GetNowNumFamiliar() > 0)
                {
                    Debug.Log("�g����");
                    InvincibleCnt = 0;
                    Hit = true;
                    Hit_DeathFamiliar = true;

                    audioSource.PlayOneShot(HitSE, VolumeControl.SE_Volume);
                }
                else
                {
                    HP -= 1;
                }

                if (HP <= 0)
                {
                    ChangeScene = true;
                    Time.timeScale = 0.0f;
                }

                if (!(other.name == "Enemy_Laser"))
                    Destroy(other.gameObject);
            }
        }
    }
}