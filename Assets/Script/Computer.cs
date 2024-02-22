using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    public Text text;
    private string Correct_PW = "9431";
    public GameObject[] ComputerScene;
    int Current_Scene = 0;
    private void Start()
    {
        text.text = "";
    }
    private void Update()
    {
        for(int i = 0; i < 3; i++)
        {
            if(Current_Scene.Equals(i))
            {
                ComputerScene[i].SetActive(true);
            }
            else
            {
                ComputerScene[i].SetActive(false);
            }
        }
    }
    public void OnClick(GameObject btn)
    {
        Current_Scene += 1;
    }
    public void Passwording(int obj)
    {
        text.text += obj.ToString();
    }
    public void Clear_Input()
    {
        text.text = "";
    }
    public void Check_Password()
    {
        if (Correct_PW.Equals(text.text))
        {
            Subtitles.Load_Subtitles("로그인 되었습니다.");
            Current_Scene += 1;
        }
        else
        {
            Subtitles.Load_Subtitles("비밀번호가 틀립니다.");
        }
    }
}
