using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;

    public int totalKeysCollected = 0;
    public GameObject gate; // Reference to the gate
    public TextMeshProUGUI keyText; // Reference to the TextMeshProUGUI component
    public GameObject Necromancer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Necromancer.SetActive(false);
    }

    public void CollectKey()
    {
        totalKeysCollected++;
        UpdateKeyText(); // Update the key count UI
        CheckKeys();
    }

    void UpdateKeyText()
    {
        if (keyText != null)
        {
            keyText.text = "Keys: " + totalKeysCollected;
        }
        else
        {
            Debug.LogError("Key TextMeshPro is not assigned in the inspector!");
        }
    }

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
