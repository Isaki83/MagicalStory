using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPosFamiliar : MonoBehaviour
{
    /* �g�����̈ʒu���Ǘ�����X�N���v�g*/
    // �ϐ��錾
    Vector2 Pos1 = new Vector2( -1.00f,  1.5f);  // 1,4,7...�v���C���[�̍���
    Vector2 Pos2 = new Vector2( -1.00f, -1.5f);  // 2,5,8...�v���C���[�̍���
    Vector2 Pos3 = new Vector2( -1.5f, 0.0f);    // 3,6,9...�v���C���[�̌��
    int MaxFamiliar = 10;                        // �ő�g������
    bool[] UseFlg;                              // �J�����Ă��邩�̃t���O�Ǘ�
    int NowNumFamiliar = 0;                     // ���݂���g�����̐�


    // Start is called before the first frame update
    void Start()
    {
        UseFlg = new bool[MaxFamiliar];
        UseFlg[0] = true;
        for(int i = 1; i < MaxFamiliar; ++i)
        {
            UseFlg[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �g�p�t���O���i�[���Č̔ԍ���Ԃ�
    public int SetUseTrueFlg()
    {
        Debug.Log("�ʉ߂��܂���");
        int Num = -1; // �̔ԍ���������
        int i = 1;
        while(true) // �������[�v
        {
            if (UseFlg[i]==false)
            {
                UseFlg[i] = true; // �g�p�t���O���X�V
                Num = i; //�̔ԍ���ݒ�
                NowNumFamiliar = Num;
                break;
            }
            i++; // �J�E���g���X�V
        }

        return Num; // �̔ԍ���Ԃ�
    }

    // ���݂̎g�����̐���Ԃ�
    public int GetNowNumFamiliar()
    {
        return NowNumFamiliar;
    }

    // �v���C���[�̑���Ɏg���������ʂƂ��Ɏg��
    public void subNowNumFamiliar()
    {
        NowNumFamiliar--;
    }
    public void FalseUseFlag(int num)
    {
        UseFlg[num] = false;
    }

    // �̔ԍ������ʒu�����蓖��
    public Vector2 GetFamiliarPos(int Num)
    {
        if (Num % 3 == 0)
        {
            return Pos3;
        }
        else if (Num % 2 == 0)
        {
            return Pos2;
        }
        else if (Num % 1 == 0)
        {
            return Pos1;
        }
        else
        {
            return new Vector2(0.0f, 0.0f);
        }
    }

    public int GetNumFamiliar()
    {
        return NowNumFamiliar;
    }
}
