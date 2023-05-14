using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    // スクロール速度
    [SerializeField] float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //下方向にスクロール
        transform.position -= new Vector3(Time.deltaTime * speed, 0);

        if (transform.position.x <= -38.35f)
        {
            transform.position = new Vector2(38.35f, 0);
        }
    }
}
