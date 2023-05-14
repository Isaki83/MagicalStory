using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    // Ž€–S‚ÌSE
    public AudioClip DieSE;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(DieSE, VolumeControl.SE_Volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
