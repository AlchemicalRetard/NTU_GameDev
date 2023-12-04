using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBehavior : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    void Start(){
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D (Collider2D other){
        if(other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("_groundLayer")){
            animator.SetTrigger("Explode");
            if(other.gameObject.tag == "Player"){
                CoreSystem.instance.PlayerAttacked();
            }
            rb.velocity = Vector2.zero;
            Destroy(gameObject, 0.5f);
        }
    }
}
