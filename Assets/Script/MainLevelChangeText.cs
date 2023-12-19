using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainLevelChangeText : MonoBehaviour
{
    public TextMeshProUGUI collectKeyHintText;

    void Awake()
    {
        if(CoreSystem.getLanguage() == CoreSystem.Language.Chinese){
            collectKeyHintText.text = "收集所有鑰匙，撃敗暗影惡魔！";
        }
        else{
            collectKeyHintText.text = "To Enter New Boss Fight Collect All Keys!!";
        }

        CoreSystem.setLastSelectedLevelName("BackGround21");
    }
}
