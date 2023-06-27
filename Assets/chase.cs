using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class chase : MonoBehaviour
{
    public playerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputBridge.Instance.YButton)
        {
            if (!playerControl.chasing)
            {
                playerControl.chasing = true;
                InputBridge.Instance.VibrateController(0.1f, 0.2f, 0.5f, ControllerHand.Left);
            }
        }
    }
}
