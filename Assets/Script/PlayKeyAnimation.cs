using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayKeyAnimation : MonoBehaviour

{
    public Animator anim;
    public DuringDialogue duringDialogue; // Reference to the DuringDialogue script
    static bool hasPlayed = false;

    void Start()
    {
        // Optionally, find the Animator if it's not set in the Inspector
        if (anim == null)
        {
            anim = FindObjectOfType<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasPlayed)
        {
            // Pause gameplay elements
            if (duringDialogue != null)
            {
                duringDialogue.PauseGameplay();
            }

            // Play the animation when the player collides
            anim.SetTrigger("KeyClick");
            hasPlayed = true;

            // Resume gameplay elements after the animation
            StartCoroutine(ResumeAfterAnimation());
        }
    }

    IEnumerator ResumeAfterAnimation()
    {
        // Wait for the animation to finish (adjust the duration accordingly)
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        // Resume gameplay elements
        if (duringDialogue != null)
        {
            duringDialogue.ResumeGameplay();
        }
    }
}
