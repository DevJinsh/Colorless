using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Temperature : MonoBehaviour
{
    public static bool isClear = false;
    int Temp;
    Text text;
    void Awake()
    {
        text = GameObject.Find("온도표시").GetComponent<Text>();        
    }

    private void OnEnable()
    {
        if(!isClear)
        {
            Temp = Random.Range(-19, 50);
        }        
    }

    void Update()
    {
        if(!isClear)
        {
            if (Temp.Equals(30))
            {
                isClear = true;
                Subtitles.Load_Subtitles("클리어");
            }
            text.text = Temp + "º";
        }        
    }

    public void UP_Temp()
    {
        Temp++;
    }

    public void Down_Temp()
    {
        Temp--;
    }
}
