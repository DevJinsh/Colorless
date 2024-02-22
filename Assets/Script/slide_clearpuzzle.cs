using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slide_clearpuzzle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(this.gameObject.name))
        {
            slide_puzzle.clear_count++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(this.gameObject.name))
        {
            slide_puzzle.clear_count--;
        }
    }

}
