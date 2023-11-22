using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTime2r : MonoBehaviour
{
    public float timeLimit = 60;  // Set your time limit here
    private float timeRemaining;
    public TextMeshProUGUI timerText;
    public bool timerIsRunning = false;

    private void Start()
    {
        // Start the timer automatically when the game starts
        timeRemaining = timeLimit;
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
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
            PauseGame();
        }

        Debug.Log("Time's Up! Game Over!");
    }

    void PauseGame()
    {
        CoreSystem.setGameEndReason(CoreSystem.GameEndReason.TimeUp);
        CoreSystem.LoadLevel("GameEndScene");
        // You can display a message to the player or bring up a menu here
        Debug.Log("Game Paused because the level was not cleared in time.");
    }

    IEnumerator addTimeAnimation(int timeToAdd){
        while(timeToAdd > 0){
            yield return new WaitForSeconds(0.1f);
            timeRemaining++;
            timeToAdd--;
        }
    }
}
