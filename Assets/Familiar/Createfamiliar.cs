using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Createfamiliar : MonoBehaviour
{
    // �ϐ��錾
    public int CreateFamiliarTime = 0; // �����J�E���g�ϐ�
    public int MaxFamiliarTime = 360;  // ���̎��ԂɂȂ�����g�����𐶐�����
    public GameObject FamiliarObject1; // �g��������1
    public GameObject FamiliarObject2; // �g��������2
    public GameObject FamiliarObject3; // �g��������3
    ManagerPosFamiliar m_posFamiliar;  // �g�����̃|�W�V�����Ǘ��X�N���v�g�Ăяo���p
    bool LimitFamiliarFlg = false;     // �t���O��True�̊Ԃ͐V�K�g�����𐶐����Ȃ�

    // Start is called before the first frame update
    void Start()
    {
        m_posFamiliar = GameObject.FindWithTag("Player").GetComponent<ManagerPosFamiliar>();
    }

    // Update is called once per frame
    void Update()
    {
        // �|�[�Y���͉������Ȃ�
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        // �g�����𐶐�����^�C�}�[���X�V
        CreateFamiliarTime++;
        // �^�C�}�[�ȏ�ɂȂ�����
        if(CreateFamiliarTime >= MaxFamiliarTime && m_posFamiliar.GetNumFamiliar() < 9 && !LimitFamiliarFlg)
        {
            // 0�ȏ�3�����̐����������_������
            int i = Random.Range(0, 3);
            // �g�����I�u�W�F�N�g�̔z���錾
            GameObject[] FamiliarObject = { FamiliarObject1, FamiliarObject2, FamiliarObject3 };
            // �G�̈ʒu�������������邽�߂̃����_������
            float j = Random.Range(-3, 3);
            // ��ʉE�Ɏg�����𐶐�
            var Familiar = Instantiate(FamiliarObject[i],
                new Vector3(9.0f, j, 0.0f), Quaternion.identity);
            Familiar.name = "Prefab_�g����" + (i + 1);
            CreateFamiliarTime = 0;
            SetLimitFamiliar(true);
        }
    }

    // ���ݎ����̎g���������邩�m�F���邽�߂�
    public void SetLimitFamiliar(bool Flg)
    {
        LimitFamiliarFlg = Flg;
    }
}
