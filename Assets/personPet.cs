using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personPet : MonoBehaviour
{
    public void give()
    {
        GetComponent<Animator>().SetBool("give", true);
    }
}
