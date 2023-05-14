using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM_Manager : MonoBehaviour
{
    static public BGM_Manager instance;

    // �O�̃V�[��
    private string beforeScene = "";

    // ���������I�[�f�B�I�\�[�X
    public AudioSource TitleBGM;
    public AudioSource GameBGM;
    public AudioSource ResultBGM;

    // Start is called before the first frame update
    void Start()
    {
        // OnActiveSceneChanged�֐����g�����߂�
        SceneManager.activeSceneChanged += OnActiveSceneChanged;

        Debug.Log(SceneManager.GetActiveScene().name);
        beforeScene = SceneManager.GetActiveScene().name;

        // ���s�������̍ŏ��̃V�[���ŗ����Ȃ�ς���
        // �^�C�g�� �V�[��
        if (SceneManager.GetActiveScene().name == "Title")
        {
            TitleBGM.Play();
        }
        // �Q�[�� �V�[��
        if (SceneManager.GetActiveScene().name == "Game")
        {
            GameBGM.Play();
        }
        // ���U���g �V�[��
        if (SceneManager.GetActiveScene().name == "Result")
        {
            ResultBGM.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���ʍX�V
        TitleBGM.volume = VolumeControl.BGM_Volume;
        GameBGM.volume = VolumeControl.BGM_Volume;
        ResultBGM.volume = VolumeControl.BGM_Volume;
    }

    // �V�[�����ς�����u�ԂɌĂяo�����֐�
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        // �^�C�g�� -> �Q�[��
        if (beforeScene == "Title" && nextScene.name == "Game")
        {
            Debug.Log(beforeScene + "->" + nextScene.name);
            TitleBGM.Stop();
            GameBGM.Play();
        }
        // �Q�[�� -> ���U���g
        if (beforeScene == "Game" && nextScene.name == "Result")
        {
            Debug.Log(beforeScene + "->" + nextScene.name);
            GameBGM.Stop();
            ResultBGM.Play();
        }
        // �Q�[���@-> �^�C�g��
        if (beforeScene == "Game" && nextScene.name == "Title")
        {
            Debug.Log(beforeScene + "->" + nextScene.name);
            GameBGM.Stop();
            TitleBGM.Play();
        }
        // ���U���g -> �Q�[��
        if (beforeScene == "Result" && nextScene.name == "Game")
        {
            Debug.Log(beforeScene + "->" + nextScene.name);
            ResultBGM.Stop();
            GameBGM.Play();
        }
        // ���U���g -> �^�C�g��
        if (beforeScene == "Result" && nextScene.name == "Title")
        {
            Debug.Log(beforeScene + "->" + nextScene.name);
            ResultBGM.Stop();
            TitleBGM.Play();
        }


        beforeScene = nextScene.name;
    }

    private void Awake()
    {
        // ���݂��Ă��Ȃ�������C���X�^���X��
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        // ���݂��Ă��������
        else
        {
            Destroy(this.gameObject);
            Debug.Log("BGM_Manager�������܂���");
        }
    }
}
