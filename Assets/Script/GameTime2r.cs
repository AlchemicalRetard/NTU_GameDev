using System.Collections;
using TMPro;
using UnityEngine;

public class GameTime2r : MonoBehaviour
{
    public float timeLimit = 60;  // Set your time limit here
    private float timeRemaining;
    public TextMeshProUGUI timerText;
    public bool timerIsRunning = false;
    public bool pauseTimerForDialogue = false;  // New variable to control timer during dialogues

    private void Start()
    {
        timeRemaining = timeLimit;
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning && !pauseTimerForDialogue)  // Check if timer should be paused for dialogues
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                EndGame();
            }
        }
    }
    public void SetPauseTimerForDialogue(bool pause)
    {
        pauseTimerForDialogue = pause;
    }

    public void addTime(int timeToAdd)
    {
        StartCoroutine("addTimeAnimation", timeToAdd);
    }

    public bool IsTimeUp()
    {
        return timeRemaining <= 0;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}：{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        // If time's up and the level is not cleared, pause the game
        if (!FoodCounter.Instance.AllFoodCollected())
        {
            CoreSystem.setGameEndReason(CoreSystem.GameEndReason.TimeUp);
            CoreSystem.LoadLevel("GameEndScene");
            // PauseGame();
        }

        Debug.Log("Time's Up! Game Over!");
    }

    // void PauseGame()
    // {
    //     Time.timeScale = 0;  // This will pause the game
    //     // You can display a message to the player or bring up a menu here
    //     Debug.Log("Game Paused because the level was not cleared in time.");
    // }

    IEnumerator addTimeAnimation(int timeToAdd){
        while(timeToAdd > 0){
            yield return new WaitForSeconds(0.1f);
            timeRemaining++;
            timeToAdd--;
        }
    }
}
