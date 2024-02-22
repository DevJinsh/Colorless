using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Battery : MonoBehaviour
{
    bool isClear = false;
    public GameObject[] pieces;
    float[] Correct = new float[8] { 180f, 0f, 0f, 0f, 0f, 37.63298f, 90f, 0f };
    public void Rotate_Pieces(GameObject obj)
    {
        if (!isClear)
        {
            obj.transform.Rotate(new Vector3(0, 0, -90));
            ClearCheck();
        }
    }

    void ClearCheck()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if(i != 1 && i != 2 && i != 4)
            {
                if (!pieces[i].GetComponent<RectTransform>().eulerAngles.z.Equals(Correct[i]))
                { 
                    return;
                }
            }            
        }
        isClear = true;
        Subtitles.Load_Subtitles("클리어");
    }
}
