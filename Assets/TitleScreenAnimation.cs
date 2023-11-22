using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenAnimation : MonoBehaviour
{
    public void toLevel(){
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("toLevel");
    }

    public void toTitle(){
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("toTitle");
    }
}
