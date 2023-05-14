using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // カーソル非表示
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // カーソル表示切替
            Cursor.visible = !Cursor.visible;
        }
    }
}
