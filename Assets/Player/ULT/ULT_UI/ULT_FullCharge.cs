using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULT_FullCharge : MonoBehaviour
{
    private Animator anime = null;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player_ULT.DestroyObj)
        {
            Destroy(this.gameObject);
        }
    }

    private void Once()
    {
        anime.SetBool("once", false);
    }
}
