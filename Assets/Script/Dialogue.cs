using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Dialogue 
{
    public string name;
    public string name_ZH;
    [TextArea(3, 10)]
    public string[] sentences;
    public string[] sentences_ZH;
    public GameObject specificBlackScreen;

}