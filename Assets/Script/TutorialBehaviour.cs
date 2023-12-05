using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehaviour : MonoBehaviour
{
    public float fadeTime = 0.2f;

    private new SpriteRenderer renderer;
    private bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.material.color = new Color(1, 1, 1, 0);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            state = true;
            StartCoroutine(FadeTo(state, fadeTime));
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            state = false;
            StartCoroutine(FadeTo(state, fadeTime));
        }
    }

    IEnumerator FadeTo(bool targetState, float time)
    {
        float value = targetState ? 1.0f : 0.0f;
        float alpha = renderer.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            if(state != targetState)
            {
                yield break;
            }
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, value, t));
            renderer.material.color = newColor;
            yield return null;
        }
        renderer.material.color = new Color(1, 1, 1, value);
    }
}
