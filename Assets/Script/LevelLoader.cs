using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadLevelAnim(levelName));
    }

    IEnumerator LoadLevelAnim(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}
