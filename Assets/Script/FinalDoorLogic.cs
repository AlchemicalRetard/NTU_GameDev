using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorLogic : MonoBehaviour
{
    public GameObject foodCounter;
    public GameObject doorCamera;

    private FoodCounter foodCounterScript;
    private bool allFoodCollected = false;
    private static bool cameraMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        foodCounterScript = foodCounter.GetComponent<FoodCounter>();
        doorCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!allFoodCollected && foodCounterScript.AllFoodCollected())
        {
            allFoodCollected = true;
            Debug.Log("All food collected!");
            // Open the door
            StartCoroutine("CameraAnimation");
        }
    }

    IEnumerator CameraAnimation()
    {
        cameraMoving = true;
        doorCamera.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        GetComponent<Animator>().SetTrigger("OpenDoor");
        yield return new WaitForSeconds(1.5f);
        doorCamera.SetActive(false);
        cameraMoving = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && allFoodCollected)
        {
            Debug.Log("Player entered the door!");
            CoreSystem.setGameEndReason(CoreSystem.GameEndReason.TutorialClear);
            CoreSystem.LoadLevel("GameEndScene");
        }
    }

    public static bool isCameraMoving(){
        return cameraMoving;
    }
}
