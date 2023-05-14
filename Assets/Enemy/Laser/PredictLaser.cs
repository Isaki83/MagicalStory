using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictLaser : MonoBehaviour
{
    public Enemy speed;

    private float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = speed.GetMoveSpeed();

        Invoke("DestroyThis", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // *****É|Å[ÉYíÜÇÕâΩÇ‡ÇµÇ»Ç¢*****
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        this.transform.Translate(-MoveSpeed, 0.0f, 0.0f);
    }

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
