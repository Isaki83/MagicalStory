using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Color : MonoBehaviour
{
    // アルファ値
    private float alpha;

    private float AddSub;
    
    public static bool left, right;

    public static int Select;

    public Image GoGameStart = null;
    public Image GoRanking = null;
    public Image GoOption = null;
    public Image GoTitle = null;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 1.0f;
        AddSub = 0.01f;
        
        left = true;
        right = false;

        Select = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(alpha > 1.0f || 0.0f > alpha)
        {
            AddSub *= -1.0f;
        }
        alpha += AddSub;

        if (SceneManager.GetActiveScene().name == "Title")
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
                Select--;
            if (Input.GetKeyDown(KeyCode.DownArrow))
                Select++;
            if (Select > 3)
                Select = 3;
            if (Select < 0)
                Select = 0;

            if(Select == 0)
            {
                GoGameStart.color = new Color(1.0f, 1.0f, 1.0f, alpha);
                GoRanking.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                GoOption.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else if (Select == 1)
            {
                GoGameStart.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                GoRanking.color = new Color(1.0f, 1.0f, 1.0f, alpha);
                GoOption.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else if (Select == 2)
            {
                GoGameStart.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                GoRanking.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                GoOption.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            }
            else if (Select == 3)
            {
                GoGameStart.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                GoRanking.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                GoOption.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }


        if (SceneManager.GetActiveScene().name == "Game"
            || SceneManager.GetActiveScene().name == "Option"
            || SceneManager.GetActiveScene().name == "Ranking")
        {
            GoTitle.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }


        if (SceneManager.GetActiveScene().name == "Result")
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                left = true;
                right = false;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                left = false;
                right = true;
            }

            if(right)
            {
                GoGameStart.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                GoTitle.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            }
            if(left)
            {
                GoGameStart.color = new Color(1.0f, 1.0f, 1.0f, alpha);
                GoTitle.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}
