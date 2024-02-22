using UnityEngine;

public class Puzzle_MovingBlock : MonoBehaviour
{
    public Canvas canvas;
    public GameObject[] Stage;
    public static int Current_State = 0;
    Vector3 ClickPoint;
    GameObject Block;

    void Update()
    {
        if(!Puzzle_ClearMovingBlock.Clear)
        {
            if (Block != null)
            {
                Vector3 distence = Input.mousePosition - ClickPoint;
                ClickPoint += distence;
                Block.GetComponent<Rigidbody2D>().AddForce(distence * 100);
            }
            if (Current_State < 4)
            {
                for (int i = 0; i < Stage.Length; i++)
                {
                    if (Current_State != i)
                    {
                        Stage[i].SetActive(false);
                    }
                    else
                    {
                        if (Stage[i].activeSelf != true)
                        {
                            Stage[i].SetActive(true);
                        }
                    }
                }
            }
            else
            {
                Puzzle_ClearMovingBlock.Clear = true;
                Player.isInteraction = false;
            }
        }        
        else
        {
            Subtitles.Load_Subtitles("이미 클리어한 퍼즐이다.");
        }
    }

    public void Set_Block(GameObject obj)
    {
        Block = obj;
        ClickPoint = Input.GetTouch(0).position;
    }

    public void Del_Block()
    {
        Block = null;
    }

    public static void NextStage()
    {
        Current_State++;
    }
}
