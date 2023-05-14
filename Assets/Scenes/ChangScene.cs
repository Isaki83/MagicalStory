using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ���̃V�[�����u�^�C�g���v�Ȃ�
        if (SceneManager.GetActiveScene().name == "Title")
        {
            // Enter�L�[��
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (UI_Color.Select == 0)
                    // �Q�[�� �V�[���Ɉړ�
                    SceneManager.LoadScene("Game");
                if (UI_Color.Select == 1)
                    // �����L���O �V�[���Ɉړ�
                    SceneManager.LoadScene("Ranking");
                if (UI_Color.Select == 2)
                    // �����L���O �V�[���Ɉړ�
                    SceneManager.LoadScene("Option");
                if (UI_Color.Select == 3)
                {
                    // �Q�[�������
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();//�Q�[���v���C�I��
#endif
                }
            }
        }

        // ���̃V�[�����u�����L���O�v�Ȃ�
        if (SceneManager.GetActiveScene().name == "Ranking")
        {
            // Enter�L�[��
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // �^�C�g�� �V�[���Ɉړ�
                SceneManager.LoadScene("Title");
            }
        }

        // ���̃V�[�����u�I�v�V�����v�Ȃ�
        if (SceneManager.GetActiveScene().name == "Option")
        {
            // Enter�L�[��
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // �^�C�g�� �V�[���Ɉړ�
                SceneManager.LoadScene("Title");
            }
        }

        // ���̃V�[�����u�Q�[���v�Ȃ�
        if (SceneManager.GetActiveScene().name == "Game")
        {
            // �v���C���[���˂񂾂�
            if (Player_Bullet.ChangeScene)
            {
                // 3�b��ɃV�[���J��
                Invoke("ChengeToResult", 3.0f);
            }

            // �|�[�Y��
            if (Mathf.Approximately(Time.timeScale, 0f))
            {
                // Enter�L�[��
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Time.timeScale = 1f;
                    // �Q�[�� �V�[���Ɉړ�
                    SceneManager.LoadScene("Title");
                }
            }

            // �f�o�b�O�p
#if UNITY_EDITOR
            // Space�L�[��
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // ���U���g �V�[���Ɉړ�
                SceneManager.LoadScene("Result");
            }
#endif
        }

        // ���̃V�[�����u���U���g�v�Ȃ�
        if (SceneManager.GetActiveScene().name == "Result")
        {
            // Enter�L�[��
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (UI_Color.right)
                    // �^�C�g�� �V�[���Ɉړ�
                    SceneManager.LoadScene("Title");
                if (UI_Color.left)
                    // �Q�[�� �V�[���Ɉړ�
                    SceneManager.LoadScene("Game");
            }
        }
    }

    // ���U���g��ʂɃV�[���J��
    void ChengeToResult()
    {
        SceneManager.LoadScene("Result");

        Player_Bullet.ChangeScene = false;
    }
}
