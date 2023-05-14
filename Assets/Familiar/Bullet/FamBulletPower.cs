using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamBulletPower : MonoBehaviour
{
    [SerializeField] private float Black = 0.75f;
    [SerializeField] private float Red = 0.5f;
    [SerializeField] private float Blue = 0.25f;

    public static float BlackBulletPower;
    public static float RedBulletPower;
    public static float BlueBulletPower;

    // Start is called before the first frame update
    void Start()
    {
        BlackBulletPower = Black;
        RedBulletPower = Red;
        BlueBulletPower = Blue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
