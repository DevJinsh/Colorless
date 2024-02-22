using UnityEngine;
using System.Collections;

public class Washer_Process : MonoBehaviour
{
    int number = 1;
    Washer_Coll washer_Coll;
    Puzzle_Washer puzzle_Washer;
    private void Start()
    {
        puzzle_Washer = GameObject.Find(number + "번").GetComponent<Puzzle_Washer>();
        washer_Coll = GameObject.Find(number + "번표시").GetComponent<Washer_Coll>();
    }
    public void Click_Button()
    {
        if (washer_Coll.enableClick && number <= 6)
        {
            puzzle_Washer.enabled = false;
            Destroy(washer_Coll.stain);
            number++;
            if(number <= 6)
            {
                puzzle_Washer = GameObject.Find(number + "번").GetComponent<Puzzle_Washer>();
                washer_Coll = GameObject.Find(number + "번표시").GetComponent<Washer_Coll>();                
            }
            else
            {
                Clear();
            }
        }
    }

    void Clear()
    {
        Subtitles.Load_Subtitles("클리어");
    }

    public void Get_Tape(GameObject target)
    {
        if(number > 6)
        {
            UI.Pick_Item(target);
        }
        else
        {
            Subtitles.Load_Subtitles("얼룩을 다 닦아내야 할 것 같다.");
        }
    }
}
