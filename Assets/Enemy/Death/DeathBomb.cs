using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBomb : MonoBehaviour
{
    /*  SE   */
    AudioSource audioSource;
    // “GŽ€–S
    public AudioClip DieSE;

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

    private void FinDestroy()
    {
        Destroy(this.gameObject);
    }
}
