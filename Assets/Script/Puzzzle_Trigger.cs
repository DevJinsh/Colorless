using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzzle_Trigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("벽"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }        
    }
}
