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

    private TextMeshProUGUI titleText;
    private Animator meowknightAnimator;


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
                StartCoroutine("badEnd");
                break;
            case CoreSystem.GameEndReason.TimeUp:
                titleText.text = "Time's up!";
                StartCoroutine("badEnd");
                break;
            case CoreSystem.GameEndReason.TutorialClear:
                titleText.text = "Tutorial clear!";
                StartCoroutine("goodEnd");
                break;
            case CoreSystem.GameEndReason.GameClear:
                titleText.text = "Game clear!";
                StartCoroutine("goodEnd");
                break;
            default:
                titleText.text = "Game end!";
                StartCoroutine("goodEnd");
                break;
        }

        if(CoreSystem.getLanguage() == CoreSystem.Language.Chinese){
            titleScreenText.text = "返回主畫面";
            nextLevelText.text = "開始冒險！";
            againText.text = "再玩一次";
        }else{
            titleScreenText.text = "Return to Title";
            nextLevelText.text = "Next Level!";
            againText.text = "Play Again";
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
