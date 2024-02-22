using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Hole : MonoBehaviour
{
    int Count = 0;
    GameObject Reward;
    public GameObject Pan;
    public GameObject Piece;
    void Awake()
    {
        Reward = GameObject.Find("보상");
    }

    void Start()
    {
        Reward.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "W")
        {
            collision.gameObject.SetActive(false);
            Process(++Count);
        }
        if (collision.gameObject.name == "H")
        {
            collision.gameObject.SetActive(false);
            Process(++Count);
        }
        if (collision.gameObject.name == "I")
        {
            collision.gameObject.SetActive(false);
            Process(++Count);
        }
        if (collision.gameObject.name == "T")
        {
            collision.gameObject.SetActive(false);
            Process(++Count);
        }
        if (collision.gameObject.name == "E")
        {
            collision.gameObject.SetActive(false);
            Process(++Count);
        }
    }

    void Process(int n)
    {
        if(n >= 5)
        {
            Subtitles.Load_Subtitles("클리어");
            Reward.SetActive(true);
            Pan.SetActive(false);
            Piece.SetActive(false);
        }
    }
}
