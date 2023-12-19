using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainLevelChangeText : MonoBehaviour
{
    public TextMeshProUGUI collectKeyHintText;
    public TextMeshProUGUI necromancerAppearText;

    void Awake()
    {
        if(CoreSystem.getLanguage() == CoreSystem.Language.Chinese){
            collectKeyHintText.text = "收集所有鑰匙，撃敗暗影惡魔！";
            necromancerAppearText.text = "終...終於！是暗影惡魔！";
        }
        else{
            collectKeyHintText.text = "To Enter New Boss Fight Collect All Keys!!";
            necromancerAppearText.text = "Ahh!! It's the dark shadow Saya!!";
        }

        CoreSystem.setLastSelectedLevelName("BackGround21");
    }
}
