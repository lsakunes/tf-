using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerground : MonoBehaviour
{
    public Transform copy;

    public void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(copy.position, Vector3.down, out hit, Mathf.Infinity, 1 << 3))
        {
            transform.position = hit.point;
        }
    }
}
