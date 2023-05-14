using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Familiar : MonoBehaviour
{
    // �񋓌^�錾
    enum Status // �g�����̃X�e�[�^�X
    {
        Caught = 0, // �߂܂��Ă���Ƃ�
        Save = 1,   // �����o�����Ƃ�
        Follow = 2, // �Ǐ]��
    }

    // �ϐ��錾
    SpriteRenderer fami;                        // �g�����̃X�v���C�g�����_���[
    public int BulletTime = 30;                 // �e��ł��o���Ԋu
    public static bool GoBullet = false;        // �e��ł��o���Ă悢��
    GameObject Player;                          // �v���C���[�I�u�W�F�N�g
    Vector3 OldPlayerPos;                       // �v���C���[��1�t���[���O�̍��W
    public int MaxFamiliar = 20;                // �J�������g�����̍ő吔
    int time;                                   // �v���C���[�������Ă���̌o�ߎ���
    bool Flg = true;                            // �v���C���[�Ɠ����ʒu�ɂ��邩
    ManagerPosFamiliar m_PosFamiliar;           // �g�����̈ʒu���Ǘ�����X�N���v�g�̌Ăяo���p
    Vector2 FixedPosition;                      // �g�����̒�ʒu
    Vector2 CalculationFPos;                    // �v�Z��̒�ʒu
    int FamiliarNum = -1;                       // �g�����̌̔ԍ�
    Createfamiliar createFamiliar;              // �g���������X�N���v�g�Ăяo���p
    float[] MoveSpeed = { 0.05f, 0.2f, 0.08f }; // �ړ����x�Ǘ��z��
    Status status;                              // �g�����̃X�e�[�^�X�Ǘ�
    bool[] SavePosFlg;                          // �g�����̋~�o��̈ʒu����ʒu�����ׂ�t���O
    int SaveTimer;                              // �������ۂ̃^�C�}�[

    public int CageHP = 5;                      // �B�̗̑�
    

    // ���ŃG�t�F�N�g
    public GameObject DeathEffect;

    // ===============================
    // �������֐�
    // ===============================
    void Start()
    {
        Debug.Log("�g��������������܂���");
        // �g�����̃X�v���C�g�����_���[���擾
        fami = GetComponent<SpriteRenderer>();
        // �V�[������Player�^�O�̃I�u�W�F�N�g��T����Player�ϐ��ɑ��
        Player = GameObject.FindWithTag("Player");
        m_PosFamiliar = GameObject.FindWithTag("Player").GetComponent<ManagerPosFamiliar>();
        // Player�^�O�̃I�u�W�F�N�g���犄�蓖�Ă��Ă���CreateFamiliar����
        createFamiliar = GameObject.FindWithTag("Player").GetComponent<Createfamiliar>();
        // ���������ۂ̃X�e�[�^�X,�����l�͕K���߂܂��Ă���
        status = Status.Caught;
        // �ϐ��̔z��̐����w��
        SavePosFlg = new bool[4];
        // �ϐ��̒���������
        for (int i = 0; i < 4; ++i)
        {
            SavePosFlg[i] = false;
        }
        // �J�������g�����̐������Z�b�g
        

        // �ŏ��͓���
        fami.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // ===============================
    // �X�V�֐�
    // ===============================
    void Update()
    {
        // *****�|�[�Y���͉������Ȃ�*****
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;
        //Debug.Log("���݂̎g�����̃X�e�[�^�X�ł�" + status);
        // *****�e�[�^�X�ɉ����ď�����ς���*****
        switch (status)
        {
            // �g�����̃X�e�[�^�X���u�߂܂��Ă���v�ꍇ
            case Status.Caught:
                // �g������葬�ňړ�������
                this.transform.Translate(-MoveSpeed[(int)Status.Caught], 0.0f, 0.0f);
                // �J�����O�ɃI�u�W�F�N�g��������
                if (!GetComponent<Renderer>().isVisible)
                {
                    // ���̃I�u�W�F�N�g�폜
                    Destroy(this.gameObject);
                    createFamiliar.SetLimitFamiliar(false);
                }
                break;

            // �g�����̃X�e�[�^�X���u������ꂽ�v�ꍇ
            case Status.Save:
                // �g�����̖ړI�n���v�Z
                CalculationFPos.x = Player.transform.position.x + FixedPosition.x;
                CalculationFPos.y = Player.transform.position.y + FixedPosition.y;

                // �^�C�}�[���Z
                SaveTimer--;

                // �g�����̈ʒu���v���C���[���E�ɂ���ꍇ
                if (this.transform.position.x >= CalculationFPos.x)
                {
                    this.transform.Translate(-MoveSpeed[(int)Status.Save], 0.0f, 0.0f);
                }
                else
                {
                    SavePosFlg[0] = true;
                }
                // �g�����̈ʒu���v���C���[��荶�ɂ���ꍇ
                if (this.transform.position.x <= CalculationFPos.x)
                {
                    this.transform.Translate(MoveSpeed[(int)Status.Save], 0.0f, 0.0f);
                }
                else
                {
                    SavePosFlg[1] = true;
                }
                // �g�����̈ʒu���v���C���[����ɂ���ꍇ
                if (this.transform.position.y >= CalculationFPos.y)
                {
                    this.transform.Translate(0.0f, -MoveSpeed[(int)Status.Save], 0.0f);
                }
                else
                {
                    SavePosFlg[2] = true;
                }
                // �g�����̈ʒu���v���C���[��艺�ɂ���ꍇ
                if (this.transform.position.y <= CalculationFPos.y)
                {
                    this.transform.Translate(0.0f, MoveSpeed[(int)Status.Save], 0.0f);
                }
                else
                {
                    SavePosFlg[3] = true;
                }

                for (int i = 0, j = 0; i < 4; ++i)
                {
                    if (SavePosFlg[i])
                    {
                        j++;
                    }
                    if (j >= 2 && SaveTimer <= 0)
                    {
                        status = Status.Follow;
                    }
                }
                break;

            // �g�����̃X�e�[�^�X���Ǐ]�̂Ƃ�
            case Status.Follow:
                // �g�����̌����𔽓]
                fami.flipX = false;

                BulletTime++; // �e���˃J�E���g���v���X

                // ��莞�Ԉȏ�ɂȂ�����e�𔭎�
                if (BulletTime >= 90 && Input.GetKey(KeyCode.Z))
                {
                    BulletTime = 0; //�J�E���g��0��
                    GoBullet = true;
                }
                else
                {
                    GoBullet = false;
                }

                // �v���C���[�ɒǏ]
                // �v���C���[���������ꍇ1�t���[���O�̈ʒu��ۑ�
                if (OldPlayerPos != Player.transform.position)
                {
                    OldPlayerPos = Player.transform.position;
                    CalculationFPos.x = Player.transform.position.x + FixedPosition.x;
                    CalculationFPos.y = Player.transform.position.y + FixedPosition.y;
                }
                // �����Ă��Ȃ����g����������ʒu�ɂ��Ă���ꍇ�̓^�C�}�[�����Z�b�g
                else if ((OldPlayerPos == Player.transform.position) && Flg)
                {
                    time = 0;
                }

                // �^�C�}�[����莞�Ԉȏ�ɂȂ�
                if (time >= 60)
                {
                    // �g�����̈ʒu���v���C���[���E�ɂ���ꍇ
                    if (this.transform.position.x > CalculationFPos.x)
                    {
                        this.transform.Translate(-MoveSpeed[(int)Status.Follow], 0.0f, 0.0f);
                    }
                    // �g�����̈ʒu���v���C���[��荶�ɂ���ꍇ
                    if (this.transform.position.x < CalculationFPos.x)
                    {
                        this.transform.Translate(MoveSpeed[(int)Status.Follow], 0.0f, 0.0f);
                    }
                    // �g�����̈ʒu���v���C���[����ɂ���ꍇ
                    if (this.transform.position.y > CalculationFPos.y)
                    {
                        this.transform.Translate(0.0f, -MoveSpeed[(int)Status.Follow], 0.0f);
                    }
                    // �g�����̈ʒu���v���C���[��艺�ɂ���ꍇ
                    if (this.transform.position.y < CalculationFPos.y)
                    {
                        this.transform.Translate(0.0f, MoveSpeed[(int)Status.Follow], 0.0f);
                    }
                }
                if ((this.transform.position.x == CalculationFPos.x) && (this.transform.position.y == CalculationFPos.y))
                {
                    Debug.Log("�g�����̌��ݒn" + this.transform.position);
                    Debug.Log("�v���C���[�̌��ݒn" + Player.transform.position);
                    Debug.Log("�g�����̖ړI�n" + CalculationFPos);
                    Flg = true;
                }
                else
                {
                    Flg = false;
                }

                // �^�C�}�[�X�V
                time++;
                break;
            default:
                break;
        }

        // �̔ԍ��ƌ��݂̎g�����̐���������������
        if (Player_Bullet.Hit_DeathFamiliar && (FamiliarNum == m_PosFamiliar.GetNowNumFamiliar()))
        {
            Destroy(this.gameObject);

            Instantiate(DeathEffect,
                    new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                    Quaternion.identity);

            m_PosFamiliar.FalseUseFlag(m_PosFamiliar.GetNowNumFamiliar());
            m_PosFamiliar.subNowNumFamiliar();
            Player_Bullet.Hit_DeathFamiliar = false;
        }

    }

    // ===============================
    // �����Ă邩���肷�邽�߂Ƀt���O��n��
    // ===============================
    public int GetAliveFlg()
    {
        return (int)status;
    }

    // ===============================
    // �J�������ۂɃt���O�𔽓]������
    // ===============================
    public void SetAliveFlg()
    {
        // �g�����̃X�e�[�^�X��Save�ɕς���
        status = Status.Save;
        // �̔ԍ����擾����
        FamiliarNum = m_PosFamiliar.SetUseTrueFlg();
        // �̔ԍ������ʒu���擾����
        FixedPosition = m_PosFamiliar.GetFamiliarPos(FamiliarNum);
        // �o�ߎ��Ԃ�61�ɂ��Ď����I�ɒ�ʒu�Ɉړ�����悤�ɂ���
        time = 61;
        // �������ۂ̃^�C�}�[���Z�b�g
        SaveTimer = 30;
        createFamiliar.SetLimitFamiliar(false);
    }

    // ===============================
    // Bullet�Ƃ̓����蔻��
    // ===============================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �e�Ƃ���������
        if (collision.gameObject.tag == "Bullet")
        {
            // ���̎g�����̃t���O��false�Ȃ�
            if (GetAliveFlg() == (int)Status.Caught)
            {
                if (CageHP <= 1)
                {
                    Debug.Log("�g�������~�o���܂���");
                    // �g�����̃X�e�[�^�X��Save�ɕς���
                    SetAliveFlg();

                    // �q�I�u�W�F�N�g(�B)������
                    foreach (Transform child in gameObject.transform)
                    {
                        Destroy(child.gameObject);
                    }

                    // ��������
                    fami.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else
                {
                    foreach (Transform child in gameObject.transform)
                    {
                        Animator anim =  child.GetComponent<Animator>();
                        anim.SetBool("dmg", true);
                    }
                    

                    CageHP--;
                }
                // �e�I�u�W�F�N�g���폜
                Destroy(collision.gameObject);
            }
        }
        // �K�E�Z�ɂ���������
        if (collision.gameObject.tag == "ULT_Bullet")
        {
            // ���̎g�����̃t���O��false�Ȃ�
            if (GetAliveFlg() == (int)Status.Caught)
            {
                Debug.Log("�g�������~�o���܂���");
                // �g�����̃X�e�[�^�X��Save�ɕς���
                SetAliveFlg();

                // �q�I�u�W�F�N�g(�B)������
                foreach (Transform child in gameObject.transform)
                {
                    Destroy(child.gameObject);
                }

                // ��������
                fami.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}