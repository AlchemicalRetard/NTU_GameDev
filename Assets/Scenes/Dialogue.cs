using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Dialogue 
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
    public GameObject specificBlackScreen;

}