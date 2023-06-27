using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class splosh : MonoBehaviour
{
    float prevFloat;
    public GameObject splash;
    public AudioClip sound;
    public float speed;
    public float loud;
    private void OnTriggerEnter(Collider other)
    {
        NavMeshAgent control = other.gameObject.GetComponent<NavMeshAgent>();
        if (control != null)
        {
            prevFloat = control.speed;
            control.speed = speed;
        }
        Instantiate(splash, other.transform);
        GetComponent<AudioSource>().PlayOneShot(sound, loud);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(splash);
        NavMeshAgent control = other.gameObject.GetComponent<NavMeshAgent>();
        if (control != null)
        {
            control.speed = prevFloat;
        }
    }
}
