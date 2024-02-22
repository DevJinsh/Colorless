using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Intro : MonoBehaviour
{
    public void Start_Button()
    {
        SceneManager.LoadScene("인트로영상&튜토리얼");
    }

    public void Exit_Button()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("게임플레이화면");
    }
}
