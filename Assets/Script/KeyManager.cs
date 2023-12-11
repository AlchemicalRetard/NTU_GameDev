using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import the TextMeshPro namespace

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;

    public int totalKeysCollected = 0;
    public GameObject gate; // Reference to the gate
    // public TextMeshProUGUI keyText; // Reference to the TextMeshProUGUI component
    public GameObject Necromancer;
    public GameObject[] keysIcon; // Reference to the key icons
    public AudioClip collectSound;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Necromancer.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        //make all key icons transparent
        foreach (GameObject keyIcon in keysIcon)
        {
            keyIcon.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    public void CollectKey()
    {
        totalKeysCollected++;
        // UpdateKeyText(); // Update the key count UI

        // change the key icon to be opaque
        for (int i = 0; i < totalKeysCollected; i++)
        {
            keysIcon[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        audioSource.PlayOneShot(collectSound);

        CheckKeys();
    }

    // void UpdateKeyText()
    // {
    //     if (keyText != null)
    //     {
    //         keyText.text = "Keys: " + totalKeysCollected;
    //     }
    //     else
    //     {
    //         Debug.LogError("Key TextMeshPro is not assigned in the inspector!");
    //     }
    // }

    void CheckKeys()
    {
        if (totalKeysCollected >= 2) // Replace 5 with the required number of keys
        {
            OpenGate();
            //Necromancer.SetActive(true);
        }
    }

    void OpenGate()
    {
        // Logic to open the gate
        gate.SetActive(false);
    }
}
