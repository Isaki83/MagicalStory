using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingBulletFamiliar : MonoBehaviour
{
    public float speed = 0.5f;
    private GameObject[] targets;
    private GameObject closeEnemy;

    Vector2 vec;

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Fam_Blue_Bullet";

        // ��ʏ�̑S�Ă̓G�̏����擾
        targets = GameObject.FindGameObjectsWithTag("Enemy");

        // �����l
        float closeDist = 1000;

        if (!(targets.Length == 0))
        {
            foreach (GameObject t in targets)
            {
                // �e�ƓG�܂ł̋������v��
                float tDist = Vector3.Distance(this.transform.position, t.transform.position);

                // ���������l�����v�������G�܂ł̋����̕����߂�������
                if (closeDist > tDist)
                {
                    // closeDist��tDist�ɒu��������
                    closeDist = tDist;

                    // ��ԋ߂��G�̏���closeEnemy�Ɋi�[����
                    closeEnemy = t;
                }
            }

            vec = closeEnemy.transform.position - this.transform.position;
        }
        else
        {
            vec = new Vector2(10.0f, 0.0f);
        }
        this.GetComponent<Rigidbody2D>().velocity = vec;
    }

    // Update is called once per frame
    void Update()
    {
        // �J�����O�ɏo����폜
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }
}
