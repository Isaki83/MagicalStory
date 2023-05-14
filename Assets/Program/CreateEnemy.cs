using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateEnemy : MonoBehaviour
{
    
    public GameObject No01;     // �C�ӂ̃I�u�W�F�N�g��Prefab������
    public GameObject No02;     // �C�ӂ̃I�u�W�F�N�g��Prefab������
    public GameObject No03;     // �C�ӂ̃I�u�W�F�N�g��Prefab������
    public GameObject No04;     // �C�ӂ̃I�u�W�F�N�g��Prefab������

    bool CsvOrRand = true;            // csv�ƃ����_���ǂ���Ő������邩
                                      // csv : true
                                      // �����_�� : false

    //==============================================
    //  csv�����p
    //==============================================
    private GameObject ObjectPut;    // �I�u�W�F�N�g��z�u����̂Ɏg��

    public string csvStage = "";    // �C���X�y�N�^�[�r���[�Ŏg������CSV�t�@�C���̃p�X������
    private TextAsset csvFile;      // CSV�t�@�C���ǂݍ��ނ̂Ɏg��

    string str = "";                // CSV�̑S�������ۑ�����
    string strget = "";             // ���o�����������ۑ�����

    string line = "";               // reader.ReadLine()�̊i�[��

    int row = 0;                    // CSV�f�[�^�̍s��
    int column = 0;                 // CSV�f�[�^�̗�

    int[,] map = new int[256, 256]; // �}�b�v�ԍ����i�[����}�b�v�p�ϐ�
    int[] iDat = new int[4];        // ���������p
    
    float posX = 0.0f;            // �}�b�v�u��X���W
    float posY = 0.0f;            // �}�b�v�u��Y���W

    float AddFlame = 0.0f;          // �t���[��
    int[] Second = new int[255];    // .csv����ǂݎ�������Ԋi�[�p
    bool[] bCreat = new bool[255];  // 1�b�̊ԂɈ�񂵂��������s�Ȃ�Ȃ��悤�ɂ���

    //==============================================
    //  �����_�������p
    //==============================================
    int SelectEnemy = 0;
    float EnemyPos;
    float CreaterTime;
    public float MaxCreaterTime = 5.0f;
    public float MinCreaterTime = 0.7f;

    public int Wave = 6000;
    float WaveCount = 0.0f;

    public int LaserWave = 9000;
    int LaserWaveCount = 3000;


    void Start()
    {
        AddFlame = 0.0f;
        CreaterTime = MaxCreaterTime;

        // csv
        Load();
        Creat();
    }

    void Update()
    {
        // �|�[�Y���͉������Ȃ�
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        // �t���[����i�߂�
        AddFlame += Time.deltaTime;
        int nowSecond = (int)AddFlame;

        switch (CsvOrRand)
        {
            case true:
                //==============================================
                //  csv�Ő���
                //==============================================
                for (int c = 0; c < column; c++)
                {
                    if (Second[c] == nowSecond && bCreat[c] == true)
                    {
                        posX = 9.0f;
                        posY = 4.0f;
                        for (int r = 1; r < row; r++)
                        {
                            if (map[r, c] == 1)
                            {
                                ObjectPut = Instantiate(No01) as GameObject;
                                ObjectPut.transform.position = new Vector3(posX, posY, 0.0f);
                            }

                            if (map[r, c] == 2)
                            {
                                ObjectPut = Instantiate(No02) as GameObject;
                                ObjectPut.transform.position = new Vector3(posX, posY, 0.0f);
                            }

                            if (map[r, c] == 3)
                            {
                                ObjectPut = Instantiate(No03) as GameObject;
                                ObjectPut.transform.position = new Vector3(posX, posY, 0.0f);
                            }

                            if (map[r, c] == 4)
                            {
                                ObjectPut = Instantiate(No04) as GameObject;
                                ObjectPut.transform.position = new Vector3(posX, posY, 0.0f);
                            }

                            posY -= 1.0f;
                        }
                        bCreat[c] = false;
                    }
                }
                // csv�̍Ō�̎��ԂɂȂ����烉���_�������ɐ؂�ւ���
                if (Second[column - 1] < nowSecond) { CsvOrRand = false; }
                break;

            case false:
                //==============================================
                //  �����_���Ő���
                //==============================================
                // ���������܂ł̎��Ԃ����炷
                WaveCount += Time.deltaTime;
                // wave�X�V���Ă悢��(�����܂ł̎��Ԃ��ŏ��l����Ȃ�������X�V���Ă悢)
                if (CreaterTime > MinCreaterTime)
                {
                    // wave�X�V�^�C�~���O
                    if (WaveCount >= Wave)
                    {
                        CreaterTime *= 0.9f;
                        Debug.Log("Wave�X�V : " + CreaterTime);
                        WaveCount = 0;
                    }
                }
                else
                {
                    CreaterTime = MinCreaterTime;
                    Debug.Log("Wave�X�V : STOP");
                }

                // ���[�U�[�̓G����
                LaserWaveCount++;
                if (LaserWaveCount >= LaserWave)
                {
                    LaserWaveCount = 0;
                    EnemyPos = Random.Range(-4.2f, 4.2f);
                    Instantiate(No04, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                }


                // �G����
                if (nowSecond >= CreaterTime)
                {
                    AddFlame = 0.0f;
                    EnemyPos = Random.Range(-4.2f, 4.2f);
                    // --- 3��ނ̓G���烉���_���ɐ��� ---
                    // �����ɐ������鐔(1�̂�2��)
                    int DoubleOrSingle = Random.Range(0, 10);
                    switch (DoubleOrSingle)
                    {
                        // --- 2�̓������� ---
                        case 0:
                            // 1�̖�
                            SelectEnemy = Random.Range(0, 3);
                            EnemyPos = Random.Range(-4.2f, 4.2f);
                            switch (SelectEnemy)
                            {
                                case 0:
                                    Instantiate(No02, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 1:
                                    Instantiate(No01, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 2:
                                    Instantiate(No03, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                default:
                                    break;
                            }
                            // 2�̖�
                            SelectEnemy = Random.Range(0, 3);
                            EnemyPos = Random.Range(-4.2f, 4.2f);
                            switch (SelectEnemy)
                            {
                                case 0:
                                    Instantiate(No02, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 1:
                                    Instantiate(No01, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 2:
                                    Instantiate(No03, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                default:
                                    break;
                            }
                            break;

                        // --- 1�̐��� ---
                        default:
                            SelectEnemy = Random.Range(0, 3);
                            switch (SelectEnemy)
                            {
                                case 0:
                                    Instantiate(No02, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 1:
                                    Instantiate(No01, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                case 2:
                                    Instantiate(No03, new Vector3(9.0f, EnemyPos, 0.0f), Quaternion.identity);
                                    break;

                                default:
                                    break;
                            }
                            break;
                    }
                }
                break;

        }
    }

    //==============================================
    // CSV�t�@�C���ǂݍ���
    //==============================================
    private void Load()
    {
        // CSV�f�[�^��str�ɕۑ�
        csvFile = Resources.Load(csvStage) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            line = reader.ReadLine();
            str = str + "," + line;
        }

        row = line.Length;
        column = CountChar(line, ',');

        str = str + ",";    // �Ō�Ɍ����������","��ǋL�B���ꂪ�Ȃ��ƍŌ�̕�������肱�ڂ��B


        // CSV�f�[�^���}�b�v�z��ϐ�map�ɕۑ�
        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < column; c++)
            {
                try { iDat[0] = str.IndexOf(",", iDat[0]); }            // ","������
                catch { break; }

                try { iDat[1] = str.IndexOf(",", iDat[0] + 1); }        // ����","������
                catch { break; }

                iDat[2] = iDat[1] - iDat[0] - 1;                        // ���������o��������

                try { strget = str.Substring(iDat[0] + 1, iDat[2]); }   // iDat[2]�����Ԃ񂾂����o��
                catch { break; }

                try { iDat[3] = int.Parse(strget); }                    // ���o����������𐔒l�^�ɕϊ�
                catch { break; }

                map[r, c] = iDat[3];   // �}�b�v�p�ϐ��ɕۑ��B�P�Ƃ��U�Ƃ�����������
                iDat[0]++;             // ���̃C���f�b�N�X��
            }
        }
    }

    //==============================================
    // �X�e�[�W����
    //==============================================
    private void Creat()
    {
        // .csv��1�s�ڂ̎��Ԃ��擾
        for (int c = 0; c < column; c++)
        {
            Second[c] = map[0, c];
            bCreat[c] = true;
        }
    }

    //==============================================
    // �񐔂��J�E���g����
    //==============================================
    public static int CountChar(string s, char c)
    {
        return s.Length - s.Replace(c.ToString(), "").Length + 1;
    }
}