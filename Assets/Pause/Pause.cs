using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;

    // �����SE
    public AudioClip Des_SE;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.transform.position = new Vector3(0.0f, 100.0f, -1.0f);

        // �R���|�[�l���g�擾�@
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 'P'�L�[�Ń|�[�Y��ʐؑ�
        if (Input.GetKeyDown(KeyCode.P))
        {
            audioSource.PlayOneShot(Des_SE, VolumeControl.SE_Volume);

            if (Time.timeScale == 0f)
            {
                // �|�[�YUI���J�����O�Ɉړ�����
                pauseUI.transform.position = new Vector3(0.0f, 100.0f, -1.0f);
                // �i�s
                Time.timeScale = 1f;
            }
            else if (Time.timeScale == 1f)
            {
                // �|�[�YUI���J�������Ɉړ�����
                pauseUI.transform.position = new Vector3(0.0f, 0.0f, -1.0f);
                // ��~
                Time.timeScale = 0f;
            }
        }
    }
}
