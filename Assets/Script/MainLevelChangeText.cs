using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainLevelChangeText : MonoBehaviour
{
    public TextMeshProUGUI goDownText;
    public TextMeshProUGUI jumpHintText;

    void Awake()
    {
        if(CoreSystem.getLanguage() == CoreSystem.Language.Chinese){
            goDownText.text = "收集所有鑰匙即可通過！";
            jumpHintText.text = "獲得四秒跳高高的能力！";
        }
        else{
            goDownText.text = "Collect All the keys to go Down!!!!!";
            jumpHintText.text = "Touch This block to get SuperJump Power for 4 seconds!!!";
        }

        CoreSystem.setLastSelectedLevelName("BackGround21");
    }
}
