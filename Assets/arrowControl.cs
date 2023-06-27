using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowControl : MonoBehaviour
{
    public float mult;
    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxis("Horizontal")*mult, 0, Input.GetAxis("Vertical")*mult));
    }
}
