using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Delete : MonoBehaviour
{
    // プレイヤー捜索
    GameObject Player;

    // 被弾のSE
    public AudioClip DieSE;
    AudioSource audioSource;
    private bool Once; 
    // Start is called before the first frame update
    void Start()
    {
        // コンポーネント取得　
        audioSource = GetComponent<AudioSource>();
        Once = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Player = GameObject.Find("Prefab_Player")) && Once)
        {
            audioSource.PlayOneShot(DieSE);
            Once = false;
        }
    }
}
