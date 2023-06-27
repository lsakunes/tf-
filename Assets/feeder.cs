using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feeder : MonoBehaviour
{
    public Transform food;
    public Transform bag;
    public feeding doggo;
    public bool hasFood;
    public bool eating;
    public float minDist;
    public feeder other;
    public Transform mouth;
    public float mouthDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!eating)
        {
            food.gameObject.SetActive(hasFood);
            if (bag.gameObject.activeSelf && Vector3.Distance(transform.position, bag.position) < minDist && !other.hasFood)
            {
                hasFood = true;
                doggo.foodPos = food;
                doggo.feed = true;
            }
            if (Vector3.Distance(food.position, mouth.position) < mouthDist && GetComponent<HandPhysics>().colliding && hasFood)
            {
                eating = true;
            }
        }
        else
        {
            hasFood = false;
            doggo.foodPos = null;
            doggo.feed = false;
            doggo.eating = true;
            doggo.start = true;
            eating = false;
        }
    }
}
