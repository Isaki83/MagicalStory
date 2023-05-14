using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;

    // 決定のSE
    public AudioClip Des_SE;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.transform.position = new Vector3(0.0f, 100.0f, -1.0f);

        // コンポーネント取得　
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 'P'キーでポーズ画面切替
        if (Input.GetKeyDown(KeyCode.P))
        {
            audioSource.PlayOneShot(Des_SE, VolumeControl.SE_Volume);

            if (Time.timeScale == 0f)
            {
                // ポーズUIをカメラ外に移動する
                pauseUI.transform.position = new Vector3(0.0f, 100.0f, -1.0f);
                // 進行
                Time.timeScale = 1f;
            }
            else if (Time.timeScale == 1f)
            {
                // ポーズUIをカメラ内に移動する
                pauseUI.transform.position = new Vector3(0.0f, 0.0f, -1.0f);
                // 停止
                Time.timeScale = 0f;
            }
        }
    }
}
