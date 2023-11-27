using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTitleFood : MonoBehaviour
{
    public GameObject foodPrefabParent;
    public int interval = 1;
    public float x;
    public float y;

    private Vector2 flyDirection;
    private GameObject[] foodPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFood());
        flyDirection = new Vector2(x, y).normalized;
        foodPrefab = new GameObject[foodPrefabParent.transform.childCount];
        for(int i = 0; i < foodPrefabParent.transform.childCount; i++){
            foodPrefab[i] = foodPrefabParent.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnFood(){
        while(true){
            yield return new WaitForSeconds(interval);
            int randomIndex = Random.Range(0, foodPrefab.Length);
            //spawn food and make it move up right
            GameObject food = Instantiate(foodPrefab[randomIndex], this.transform.position, Quaternion.identity);
            food.AddComponent<Rigidbody2D>();
            food.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            food.GetComponent<Rigidbody2D>().velocity = flyDirection;
            Destroy(food, 30);
        }
    }
}
