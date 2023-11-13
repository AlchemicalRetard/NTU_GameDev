using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        CoreSystem.LoadLevel(sceneName);
    }
}
