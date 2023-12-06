using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject screen;
    public Dialogue dialogue;

    /* public void TriggerDialogue()
     {
         screen.SetActive(true);
         FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
     }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //screen.SetActive(true);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            gameObject.SetActive(false);
        }
    }
}