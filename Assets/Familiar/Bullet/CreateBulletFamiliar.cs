using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBulletFamiliar : MonoBehaviour
{
    //�@�e�I�u�W�F�N�g
    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Familiar.GoBullet)
        {
            Instantiate(Bullet,
                            new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                            Quaternion.identity);
        }
    }
}
