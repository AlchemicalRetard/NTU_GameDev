using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    private DuringDialogue otherScripts;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject blackScreen; // Reference to the black screen GameObject
    public Dialogue firstDialogue;  // Assign the first dialogue in the Inspector

    private Queue<string> sentences;

    void Start()
    {
        otherScripts=GetComponent<DuringDialogue>();
        sentences = new Queue<string>();
        StartDialogue(firstDialogue); // Automatically start the first dialogue
    }

    public void OnContinueButton()
    {
        DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        otherScripts.PauseGameplay();
        if (blackScreen != null)
            blackScreen.SetActive(true);

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        if (blackScreen != null)
            blackScreen.SetActive(false); // Deactivate black screen when dialogue ends
        otherScripts.ResumeGameplay();
        Debug.Log("End of conversation.");
    }
}
