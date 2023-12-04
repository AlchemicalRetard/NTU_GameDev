using UnityEngine;

public class KeyPickups : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KeyManager.instance.CollectKey();
            Destroy(gameObject);  // Destroy the key object
        }
    }
}
