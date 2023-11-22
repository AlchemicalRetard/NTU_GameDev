using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toLevel(){
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("toLevel");
    }

    public void toTitle(){
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("toTitle");
    }
}

