using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    // �X�R�A�i�[�p
    public static int score = 0;

    // �V�[���̏��߂ɃX�R�A��'0'�ɖ߂����̃t���O
    public bool ScoreReset = true;

    // Start is called before the first frame update
    void Start()
    {
        // true �Ȃ�V�[���̂͂��߂�'0'�ɖ߂�
        if (ScoreReset)
            score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreText == null) return;

        ScoreText.SetText("{0}", score);
    }

    // �X�R�A�����Z�������ꏊ�ŌĂяo��
    // ���� : �P��ő��₵�����l
    public static void AddScore(int num)
    {
        // �����ɓ��ꂽ�l�����Z
        Debug.Log("���Z����O");
        score += num;
        Debug.Log("���Z�����");
    }
}