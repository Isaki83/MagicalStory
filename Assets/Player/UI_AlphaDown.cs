using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AlphaDown : MonoBehaviour
{
    public CanvasGroup canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.alpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canvas.alpha = 0.3f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canvas.alpha = 1.0f;
        }
    }
}
