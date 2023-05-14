using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_BGAPP : MonoBehaviour
{
    //Å@êÿÇËë÷Ç¶ÇÈÇ◊Ç´îwåi
    GameObject sugoi;
    GameObject noon;
    GameObject evening;
    GameObject night;

    int i = 0; // êÿÇËë÷Ç¶
    // Start is called before the first frame update
    void Start()
    {
        sugoi = GameObject.Find("BG_SUGOI");
        noon = GameObject.Find("BG_NOON");
        evening = GameObject.Find("BG_EVE");
        night = GameObject.Find("BG_NGT");

        sugoi.SetActive(true);
        noon.SetActive(false);
        evening.SetActive(false);
        night.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (false)
        {
            if (i < 3)
            {
                i += 1;
            }
            else
            {
                i = 0;
            }
            switch(i)
            {
                case 0:
                    sugoi.SetActive(true);
                    noon.SetActive(false);
                    evening.SetActive(false);
                    night.SetActive(false);
                    break;
                case 1:
                    sugoi.SetActive(false);
                    noon.SetActive(true);
                    evening.SetActive(false);
                    night.SetActive(false);
                    break;
                case 2:
                    sugoi.SetActive(false);
                    noon.SetActive(false);
                    evening.SetActive(true);
                    night.SetActive(false);
                    break;
                case 3:
                    sugoi.SetActive(false);
                    noon.SetActive(false);
                    evening.SetActive(false);
                    night.SetActive(true);
                    break;
            }

        }
    }
}
