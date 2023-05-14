using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreRanking : MonoBehaviour
{
    static public ScoreRanking instance;

    private int[] Score_Ranking = new int[6];

    // �I�u�W�F�N�g�擾
    public GameObject obj;
    public TextMeshProUGUI First;
    public TextMeshProUGUI Second;
    public TextMeshProUGUI Third;
    public TextMeshProUGUI Fourth;
    public TextMeshProUGUI Fifth;

    // �O�̃V�[��
    private string beforeScene = "";

    // 1�񂾂�
    private bool once;
    // �ėp
    private int tmp;

    // Start is called before the first frame update
    void Start()
    {
        // OnActiveSceneChanged�֐����g�����߂�
        SceneManager.activeSceneChanged += OnActiveSceneChanged;

        beforeScene = SceneManager.GetActiveScene().name;

        once = true;
        tmp = 0;

        obj.transform.position = new Vector3(-1000.0f, 740.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (once)
        {
            Score_Ranking[5] = Score.score;

            // �\�[�g
            Array.Sort(Score_Ranking);
            Array.Reverse(Score_Ranking);

            once = false;
        }

        // �e�L�X�g�ɒl�𔽉f����
        First.SetText(" 1I  {0000}", Score_Ranking[0]);
        Second.SetText("2I  {0000}", Score_Ranking[1]);
        Third.SetText("3I  {0000}", Score_Ranking[2]);
        Fourth.SetText("4I  {0000}", Score_Ranking[3]);
        Fifth.SetText("5I  {0000}", Score_Ranking[4]);
    }

    // �V�[�����ς�����u�ԂɌĂяo�����֐�
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        if (nextScene.name == "Ranking")
        {
            obj.transform.position = new Vector3(1000.0f, 740.0f, 0.0f);
        }
        else
        {
            obj.transform.position = new Vector3(-1000.0f, 740.0f, 0.0f);
        }

        // �����L���O�X�V�̃^�C�~���O
        // �Q�[�� >> �^�C�g��
        // ���U���g >> �^�C�g��
        // ���U���g >> �Q�[��
        if ((beforeScene == "Game" && nextScene.name == "Title")
            || (beforeScene == "Result" && nextScene.name == "Title")
            || (beforeScene == "Result" && nextScene.name == "Game"))
        {
            Debug.Log("�����L���O�X�V���܂���");
            once = true;
        }

        beforeScene = nextScene.name;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("ScoreRanking�������܂���");
        }
    }
}
