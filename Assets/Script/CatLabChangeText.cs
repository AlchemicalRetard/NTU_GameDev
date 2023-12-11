using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatLabChangeText : MonoBehaviour
{
    public TextMeshProUGUI foxText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI superJumpText;

    void Awake()
    {
        if(CoreSystem.getLanguage() == CoreSystem.Language.Chinese){
            foxText.text = "※請放心，沒有狐狸會在本教程受到傷害。";
            foodText.text = "食物 = 時間！！！";
            superJumpText.text = "跳高方塊跳高高！";
        }
        else{
            foxText.text = "※No fox will be hurt during this tutorial.";
            foodText.text = "FOOD = TIME!!!";
            superJumpText.text = "Super Jump!!";
        }

        CoreSystem.setLastSelectedLevelName("CatLab");
    }
}
