using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_ClearMovingBlock : MonoBehaviour
{
    public static bool Clear = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("색블럭"))
        {
            if(!Clear)
            {
                GameObject.Find("용액").GetComponent<Animator>().SetBool("Clear", true);
                Subtitles.Load_Subtitles("색 추출에 성공했다. 용액을 모으자");
            }            
        }
    }
    public void Next()
    {
        GameObject.Find("용액").gameObject.SetActive(false);
        Player.ColorWater[Puzzle_MovingBlock.Current_State] = true;
        Puzzle_MovingBlock.NextStage();
    }
}
