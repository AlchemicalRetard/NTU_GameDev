using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleScreenChangeText : MonoBehaviour
{
    public TextMeshProUGUI startText;
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI languageText;

    public void Awake()
    {
        changeText();
    }

    public void changeText()
    {
        if (CoreSystem.getLanguage() == CoreSystem.Language.Chinese)
        {
            startText.text = "開始冒險！";
            tutorialText.text = "教程";
            languageText.text = "ENG";
        }
        else
        {
            startText.text = "Start!";
            tutorialText.text = "Tutorial";
            languageText.text = "中文";
        }
    }
}
