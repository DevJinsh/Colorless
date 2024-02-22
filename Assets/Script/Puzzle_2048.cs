using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Puzzle_2048 : MonoBehaviour
{
    public List<GameObject> Pieces;
    
    float speed = 1000f;
    float Dis = 300f;
    Vector2 clickPos;
    Vector2 endPos;

    void Moving_Pieces()
    {
        Vector3 distence = endPos - clickPos;
        for (int i = 0; i < Pieces.Count; i++)
        {
            if (distence.x > Dis || distence.x < -1 * Dis)
            {
                if(distence.x > 0)
                {
                    Pieces[i].GetComponent<Rigidbody2D>().velocity = (new Vector2(speed, 0));
                }
                else
                {
                    Pieces[i].GetComponent<Rigidbody2D>().velocity = (new Vector2(-1 * speed, 0));
                }
            }
            else if (distence.y > Dis || distence.y < -1 * Dis)
            {
                if (distence.y > 0)
                {
                    Pieces[i].GetComponent<Rigidbody2D>().velocity = (new Vector2(0, speed));
                }
                else
                {
                    Pieces[i].GetComponent<Rigidbody2D>().velocity = (new Vector2(0, -1 * speed));
                }
            }
        }
    }

    public void DragStart()
    {
        //clickPos = Input.mousePosition;
        clickPos = Input.GetTouch(0).position;
    }

    public void DragEnd()
    {
        //endPos = Input.mousePosition;
        endPos = Input.GetTouch(0).position;
        Moving_Pieces();
    }
}