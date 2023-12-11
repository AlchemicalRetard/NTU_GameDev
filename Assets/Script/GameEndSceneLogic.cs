using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEndSceneLogic : MonoBehaviour
{
    public GameObject title;
    public GameObject meowknight;
    public GameObject titleScreenButton;
    public GameObject nextLevelButton;

    public TextMeshProUGUI titleScreenText;
    public TextMeshProUGUI againText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI clearTimeText;
    public TextMeshProUGUI heartLeftText;

    private TextMeshProUGUI titleText;
    private Animator meowknightAnimator;

    private string end = "goodEnd";


    private string[] randomAttack = {"Attack1", "Attack2", "Attack4"};

    // Start is called before the first frame update
    void Start()
    {
        meowknightAnimator = meowknight.GetComponent<Animator>();

        print(CoreSystem.getGameEndReason());
        print(CoreSystem.getLanguage());

        titleScreenButton.SetActive(CoreSystem.getGameEndReason() != CoreSystem.GameEndReason.TutorialClear);
        nextLevelButton.SetActive(CoreSystem.getGameEndReason() == CoreSystem.GameEndReason.TutorialClear);

        titleText = title.GetComponent<TextMeshProUGUI>();
        switch(CoreSystem.getGameEndReason()){
            case CoreSystem.GameEndReason.PlayerDead:
                titleText.text = "Game Over";
                end = "badEnd";
                break;
            case CoreSystem.GameEndReason.TimeUp:
                titleText.text = "Time's up!";
                end = "badEnd";
                break;
            case CoreSystem.GameEndReason.TutorialClear:
                titleText.text = "Tutorial clear!";
                end = "goodEnd";
                break;
            case CoreSystem.GameEndReason.GameClear:
                titleText.text = "Game clear!";
                end = "goodEnd";
                break;
            default:
                titleText.text = "Game end!";
                end = "goodEnd";
                break;
        }
        StartCoroutine(end);

        if(CoreSystem.getLanguage() == CoreSystem.Language.Chinese){
            titleScreenText.text = "返回主畫面";
            nextLevelText.text = "開始冒險！";
            againText.text = "再玩一次";
            if(end == "goodEnd")
                clearTimeText.text = "通關時間：" + CoreSystem.clearTime;
            else if(end == "badEnd")
                clearTimeText.text = "存活時間：" + CoreSystem.clearTime;
            heartLeftText.text = "剩餘生命：" + CoreSystem.heartLeft;
        }else{
            titleScreenText.text = "Return to Title";
            nextLevelText.text = "Next Level!";
            againText.text = "Play Again";
            if(end == "goodEnd")
                clearTimeText.text = "Clear Time: " + CoreSystem.clearTime;
            else if(end == "badEnd")
                clearTimeText.text = "Survive Time: " + CoreSystem.clearTime;
            heartLeftText.text = "Heart Left: " + CoreSystem.heartLeft;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator goodEnd(){
        while(true){
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            meowknightAnimator.SetTrigger(randomAttack[Random.Range(0, randomAttack.Length)]);
        }
    }

    IEnumerator badEnd(){
        yield return new WaitForSeconds(1f);
        meowknightAnimator.SetTrigger("Death");
    }

    public void returnToTitle(){
        CoreSystem.LoadLevel("TitleScreen");
    }

    public void playAgain(){
        CoreSystem.LoadLevel(CoreSystem.getLastSelectedLevelName());
    }

    public void nextLevel(){
        CoreSystem.LoadLevel("BackGround21");
    }
}
