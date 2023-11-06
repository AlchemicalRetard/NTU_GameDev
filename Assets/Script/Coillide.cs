using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coillide : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("collided");
    }
}
