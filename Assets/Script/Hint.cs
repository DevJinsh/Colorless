using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public static GameObject[] Hint_button;
    // Start is called before the first frame update
    void Awake()
    {
        Hint_button = GameObject.FindGameObjectsWithTag("hint");
    }
    void Start()
    {
        for (int i = 0; i < Hint_button.Length; i++)
        {
            Hint_button[i].SetActive(false);
        }
    }
    public void Hint_Button()
    {
        for (int i = 0; i < Hint_button.Length; i++)
        {
            Hint_button[i].SetActive(!Hint_button[i].activeSelf);
        }
    }
}

