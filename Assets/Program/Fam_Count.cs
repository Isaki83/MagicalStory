using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fam_Count : MonoBehaviour
{
    GameObject[] tagObjects;

    float timer = 0.0f;
    float interval = 1.0f;

    public static float MaxVolum;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MaxVolum = 0.05f;
        timer += Time.deltaTime;
        if (timer > interval)
        {
            Check("Familiar");
            timer = 0;
        }
        if (tagObjects.Length > 0)
        {
            MaxVolum = MaxVolum / tagObjects.Length;
        }
    }

    void Check(string tagname)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(tagname);
        if (tagObjects.Length == 0)
        {
            Debug.Log("Fam = 0");
        }
    }
}
