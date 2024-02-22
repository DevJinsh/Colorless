using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public void Get_Flower(GameObject obj)
    {
        if (Player.Handle_Item == null)
        {
            if(!Player.Handle_Item.name.Equals("삽"))
            {
                Subtitles.Load_Subtitles("삽이 필요한 듯 하다.");
            }            
        }
        else
        {
            Subtitles.Load_Subtitles(obj.name + "을 흭득했다.");
            UI.Pick_Item(obj);
            obj.SetActive(false);
        }        
    }
}
