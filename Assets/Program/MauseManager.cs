using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // �J�[�\����\��
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // �J�[�\���\���ؑ�
            Cursor.visible = !Cursor.visible;
        }
    }
}
