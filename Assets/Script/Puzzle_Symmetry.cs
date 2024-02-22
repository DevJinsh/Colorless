using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Puzzle_Symmetry : MonoBehaviour
{
    public Button[] buttons;
    Color handle_color;
    public Color[] Correct;    
    bool isClear = false;

    GameObject Cleartext;
    private void Awake()
    {
        Cleartext = GameObject.Find("대칭퍼즐글씨");
    }
    private void Update()
    {
        if(!isClear)
        {
            Check_Clear();
        }
        else
        {
            Display.Interaction_target = "온도조절기";
        }
    }
    public void Click_Beaker(Button btn)
    {
        if(Player.ColorWater[Int32.Parse(btn.name) - 1])
        {
            Subtitles.Load_Subtitles("용액을 선택했습니다.");
            handle_color = btn.colors.normalColor;
        }
        else
        {
            Subtitles.Load_Subtitles("용액을 아직 얻지 못했습니다.");
        }
    }
    public void Click_Blank(Button btn)
    {
        if(!isClear)
        {
            btn.GetComponent<Image>().color = handle_color;            
        }
    }
    void Check_Clear()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            if(buttons[i].GetComponent<Image>().color != Correct[i])
            {
                return;
            }
        }
        isClear = true;
        Subtitles.Load_Subtitles("클리어");
        InvokeRepeating("Puzzle_Clear", 0.5f, 0.5f);
        Invoke("InvokeCancel", 2f);
    }

    void Puzzle_Clear()
    {
        Cleartext.GetComponent<Image>().enabled = !Cleartext.GetComponent<Image>().enabled;
    }

    void InvokeCancel()
    {
        CancelInvoke("Puzzle_Clear");        
    }
}
