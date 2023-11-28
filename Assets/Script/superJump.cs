using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superJump : MonoBehaviour
{
    private MeowMovement jump;
    public GameObject playerCat;
    void Start()
    {
        jump = playerCat.GetComponent<MeowMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            jump.jumpHeight = 7f;
            Debug.Log("touhed");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        jump.jumpHeight = 3f;
    }

}
