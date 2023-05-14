using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VolumeControl : MonoBehaviour
{
    // ����
    public static float BGM_Volume = 0.1f;
    public static float SE_Volume = 0.1f;

    /*--------------
        �Q�[�W
    --------------*/
    // �I�u�W�F�N�g�擾
    public RectTransform BGM_Gauge;
    public RectTransform SE_Gauge;
    // �f�t�H���g�̃T�C�Y�i�[�p
    private Vector2 BGM_DefaultSize;
    private Vector2 SE_DefaultSize;
    // �������ŃQ�[�W���i�݂����Ȃ��悤�ɂ���J�E���g
    private int Gauge_Cnt = 30;
    // ��񂾂�
    private bool once = true;

    /*--------------
        ����(TMP)
    --------------*/
    // �I�u�W�F�N�g�擾
    public TextMeshProUGUI BGM_Text;
    public TextMeshProUGUI SE_Text;

    /*--------------
        �I��_
    --------------*/
    // �I�u�W�F�N�g�擾
    public RectTransform Select_Bar;
    private bool up, down;

    // Start is called before the first frame update
    void Start()
    {
        /*--------------
            �Q�[�W
        --------------*/
        // �f�t�H���g�̃T�C�Y�擾
        BGM_DefaultSize = new Vector2(BGM_Gauge.sizeDelta.x, BGM_Gauge.sizeDelta.y);
        SE_DefaultSize = new Vector2(SE_Gauge.sizeDelta.x, SE_Gauge.sizeDelta.y);

        /*--------------
            �I��_
        --------------*/
        // �����ʒu
        // ���̃V�[�����u�^�C�g���v�Ȃ�
        if (SceneManager.GetActiveScene().name == "Title")
        {
            Select_Bar.transform.position = new Vector3(BGM_Gauge.position.x - 100.0f, BGM_Gauge.position.y, 0.0f);
        }
        // ���̃V�[�����u�Q�[���v�Ȃ�
        if (SceneManager.GetActiveScene().name == "Game")
        {
            Select_Bar.transform.position = new Vector3(BGM_Gauge.position.x - 1.0f, BGM_Gauge.position.y, 0.0f);
        }

        up = true;
        down = false;
    }

    // Update is called once per frame
    void Update()
    {
        // �I�v�V�����V�[�� or �|�[�Y���ŉ��ʒ���
        if (SceneManager.GetActiveScene().name == "Option" || Mathf.Approximately(Time.timeScale, 0f))
        {
            if (Gauge_Cnt >= 30)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (up)
                        BGM_Volume += 0.01f;
                    if (down)
                        SE_Volume += 0.01f;

                    Gauge_Cnt = 0;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (up)
                        BGM_Volume -= 0.01f;
                    if (down)
                        SE_Volume -= 0.01f;

                    Gauge_Cnt = 0;
                }
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (once)
                {
                    Gauge_Cnt = 30;
                    once = false;
                }
                Gauge_Cnt++;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                once = true;
            }

            // �l��'1'�𒴉߁A�܂���'0'�����ɂȂ�Ȃ��悤�ɂ���
            if (BGM_Volume < 0.0f) { BGM_Volume = 0.0f; }
            if (BGM_Volume > 1.0f) { BGM_Volume = 1.0f; }
            if (SE_Volume < 0.0f) { SE_Volume = 0.0f; }
            if (SE_Volume > 1.0f) { SE_Volume = 1.0f; }
        }


        /*--------------
            �Q�[�W
        --------------*/
        // �I�u�W�F�N�g�̑傫���𔽉f����
        BGM_Gauge.sizeDelta = new Vector2(BGM_Volume * BGM_DefaultSize.x, BGM_DefaultSize.y);
        SE_Gauge.sizeDelta = new Vector2(SE_Volume * SE_DefaultSize.x, SE_DefaultSize.y);

        /*--------------
            ����(TMP)
        --------------*/
        // �e�L�X�g�ɒl�𔽉f����
        BGM_Text.SetText("{0}%", Mathf.Floor(BGM_Volume * 100.0f));
        SE_Text.SetText("{0}%", Mathf.Floor(SE_Volume * 100.0f));

        /*--------------
            �I��_
        --------------*/
        // ��A���[�L�[���������Ƃ�
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ���̃V�[�����u�^�C�g���v�Ȃ�
            if (SceneManager.GetActiveScene().name == "Option")
            {
                // �ړ�
                Select_Bar.transform.position = new Vector3(BGM_Gauge.position.x - 100.0f, BGM_Gauge.position.y, 0.0f);
            }
            // ���̃V�[�����u�Q�[���v�Ȃ�
            if (SceneManager.GetActiveScene().name == "Game")
            {
                // �ړ�
                Select_Bar.transform.position = new Vector3(BGM_Gauge.position.x - 1.0f, BGM_Gauge.position.y, 0.0f);
            }

            up = true;
            down = false;
        }
        // ���A���[�L�[���������Ƃ�
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // ���̃V�[�����u�^�C�g���v�Ȃ�
            if (SceneManager.GetActiveScene().name == "Option")
            {
                // �ړ�
                Select_Bar.transform.position = new Vector3(SE_Gauge.position.x - 100.0f, SE_Gauge.position.y, 0.0f);
            }
            // ���̃V�[�����u�Q�[���v�Ȃ�
            if (SceneManager.GetActiveScene().name == "Game")
            {
                // �ړ�
                Select_Bar.transform.position = new Vector3(SE_Gauge.position.x - 1.0f, SE_Gauge.position.y, 0.0f);
            }

            up = false;
            down = true;
        }
    }
}