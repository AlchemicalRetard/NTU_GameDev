using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEndSceneLogic : MonoBehaviour
{
    public GameObject title;
    public GameObject meowknight;

    private TextMeshProUGUI titleText;
    private Animator meowknightAnimator;

    private string[] randomAttack = {"Attack1", "Attack2", "Attack4"};

    // Start is called before the first frame update
    void Start()
    {
        meowknightAnimator = meowknight.GetComponent<Animator>();

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
            default:
                titleText.text = "Game end!";
                StartCoroutine("goodEnd");
                break;
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
}
