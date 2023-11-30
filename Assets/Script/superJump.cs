using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superJump : MonoBehaviour
{
    public float jumpDuration = 3f;
    private MeowMovement jump;
    public float jumpHeight;
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
            StartCoroutine(JumpTime());
        }
    }
    
    IEnumerator JumpTime()
    {
        jump.jumpHeight = jumpHeight; // changing the jump height here
        Debug.Log("touhed");
        yield return new WaitForSeconds(jumpDuration);  

        jump.jumpHeight = 3.5f; // return jump height to normal

    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        jump.jumpHeight = 3f;
    }*/

}
