using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Washer : MonoBehaviour
{
    float Speed = 2f;
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, Speed * -1));
    }
}
