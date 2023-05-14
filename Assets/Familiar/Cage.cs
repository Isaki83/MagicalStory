using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    // アニメーション用
    private Animator anime = null;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Fin()
    {
        anime.SetBool("dmg", false);
    }
}
