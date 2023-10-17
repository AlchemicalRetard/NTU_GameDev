using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMovement : MonoBehaviour
{
    public float speed = 1f;
    private GameObject[] joints;

    // Start is called before the first frame update
    void Start()
    {
        //get all children gameobjects
        joints = new GameObject[this.transform.childCount];
        for(int i = 0; i < this.transform.childCount; i++)
        {
            joints[i] = this.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        if(x != 0){
            for(int i = 0; i < joints.Length; i++)
            {
                joints[i].SetActive(false);
            }
            Vector3 movement = new Vector3(x, 0, 0);
            transform.Translate(movement * speed * Time.deltaTime);
        }else{
            for(int i = 0; i < joints.Length; i++)
            {
                joints[i].SetActive(true);
            }
        }
    }
}
