using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public GameObject heart1, heart2, heart3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeart(int amount)
    {
        heart1.SetActive(amount >= 1);
        heart2.SetActive(amount >= 2);
        heart3.SetActive(amount >= 3);
    }
}
