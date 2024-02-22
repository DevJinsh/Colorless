using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Washer_Coll : MonoBehaviour
{
    public bool enableClick = false;
    public GameObject stain;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enableClick = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enableClick = false;
    }
}
