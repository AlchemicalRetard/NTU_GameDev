using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMovement : MonoBehaviour
{
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(x, 0, 0);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
