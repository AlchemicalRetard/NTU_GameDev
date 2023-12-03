using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBehavior : MonoBehaviour
{
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D (Collider2D other){
        if(other.gameObject.tag != "Necromancer"){
            animator.SetTrigger("Explode");
            if(other.gameObject.tag == "Player"){
                CoreSystem.instance.PlayerAttacked();
            }
        }
    }
}
