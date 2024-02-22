using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slide_puzzle : MonoBehaviour
{
    Vector3 ClickPoint;
    GameObject Block;
    public GameObject Award;
    public static int clear_count=0;
    bool Clear = false;

    void Start()
    {
        Award.SetActive(false);
    }

    void Update()
    {

        if (!Clear)
        {
            if (Block != null)
            {
                Vector3 distence = Input.mousePosition - ClickPoint;
                ClickPoint += distence;
                Block.GetComponent<Rigidbody2D>().AddForce(distence * 50);
            }
        }
        if (clear_count == 6)
        {
            Clear = true;
            Subtitles.Load_Subtitles("클리어");
            Award.SetActive(true);
        }
    }

    public void Set_Block(GameObject obj)
    {
        Block = obj;
        ClickPoint = Input.mousePosition;
    }

    public void Del_Block()
    {
        Block = null;
    }

}
