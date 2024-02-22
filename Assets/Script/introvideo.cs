using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Introvideo : MonoBehaviour
{
    public void Start()
    {
        SceneManager.LoadScene("게임플레이화면");
    }
}