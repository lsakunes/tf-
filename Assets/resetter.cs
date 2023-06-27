using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetter : MonoBehaviour
{
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(InputBridge.Instance.YButtonDown)
        {
            transform.position = startPos;  
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
