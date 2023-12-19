using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superJump : MonoBehaviour
{
    public float jumpDuration = 3f;
    private MeowMovement jump;
    public float jumpHeight;
    public GameObject playerCat;
    public UnityEngine.Rendering.Universal.Light2D light;

    static private bool enabled = true; //make it static, so every jump block will disabled at the same time
    private SpriteRenderer spriteRenderer;
    private float targetAlpha = 1f;
    private float lightIntensity = 0.3f;

    void Start()
    {
        jump = playerCat.GetComponent<MeowMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enabled){
            targetAlpha = 1f;
            lightIntensity = 0.3f;
        }else{
            targetAlpha = 0.3f;
            lightIntensity = 0.0f;
        }

        Color c = spriteRenderer.color;
        if((!enabled && (spriteRenderer.color.a > 0.301f || light.intensity > 0.001f)) || (enabled && (spriteRenderer.color.a < 0.999f || light.intensity < 0.299f)))
        {
            c = spriteRenderer.color;
            //lerp alpha to zero
            c.a = Mathf.Lerp(c.a, targetAlpha, 0.16f);
            spriteRenderer.color = c;
            light.intensity = Mathf.Lerp(light.intensity, lightIntensity, 0.16f);
        }else{
            c = spriteRenderer.color;
            c.a = targetAlpha;
            spriteRenderer.color = c;
            light.intensity = lightIntensity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && enabled)
        {
            StartCoroutine(JumpTime());
        }
    }
    
    IEnumerator JumpTime()
    {
        enabled = false;

        jump.jumpHeight = jumpHeight; // changing the jump height here
        Debug.Log("touhed: " + Time.time);
        yield return new WaitForSeconds(jumpDuration);  

        jump.jumpHeight = 3f; // return jump height to normal

        enabled = true;

    }

    IEnumerator Fade()
    {
        Color c = spriteRenderer.color;
        //yes, this is an infinite loop to always change the alpha value
        while(true){
            while((!enabled && spriteRenderer.color.a > 0.501f) || (enabled && spriteRenderer.color.a < 0.999f))
            {
                c = spriteRenderer.color;
                //lerp alpha to zero
                c.a = Mathf.Lerp(c.a, targetAlpha, 0.3f);
                spriteRenderer.color = c;
                yield return new WaitForSeconds(0.01f);
            }
            c = spriteRenderer.color;
            c.a = targetAlpha;
            spriteRenderer.color = c;
        }
    }
}
