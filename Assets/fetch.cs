using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class fetch : MonoBehaviour
{
    public Transform fetching;
    private Grabber side;
    public bool waitForThrow;
    public float timeThrown;
    public float timeForThrow;
    public float throwDist;
    public bool pointing;
    public bool thrown;
    public Transform human;
    public bool readyForFetch;
    public Transform lookAt;
    private bool startLook;
    private Vector3 origLookPos;
    public Transform forward;
    private int frame;
    public int lookInterpolateFrames;
    public Transform humanFace;
    public bool lookAtHuman;
    public bool randomLooking;
    private Vector3 prevLooking;

    private void Start()
    {
        prevLooking = lookAt.position;
        frame = 0;
        origLookPos = forward.position;
    }
    // Update is called once per frame
    void Update()
    {
        playerControl control = GetComponent<playerControl>();
        Grabbable grab = fetching.GetComponent<Grabbable>();
        float interpolationRatio = (float)frame / lookInterpolateFrames;
        bool lookedAtHuman = false;
        if (control.dropping)
        {
            lookAt.position = control.dropPos.position;
        }
        else if (control.chasing || waitForThrow)
        {
            lookAt.position = fetching.position;
        }
        else if (lookAtHuman)
        {
            RaycastHit hit;
            if (Physics.Raycast(human.position, Vector3.down, out hit, Mathf.Infinity, 1 << 3))
            {
                transform.LookAt(Vector3.Lerp(origLookPos, hit.point, interpolationRatio));
            }
            frame++;
            lookedAtHuman = true;
            lookAt.position = Vector3.Lerp(prevLooking, humanFace.position, interpolationRatio);
        }
        else if (!randomLooking)
        {
            lookAt.position = fetching.position;
        }
        if (!lookedAtHuman)
        {
            prevLooking = lookAt.position;
            frame = 0;
            origLookPos = forward.position;
        }
        if (!readyForFetch) return;
        if (grab.BeingHeld)
        {
            waitForThrow = true;
            side = grab.GetPrimaryGrabber();
        }
        if (!grab.BeingHeld && waitForThrow)
        {
            waitForThrow = false;
            timeThrown = Time.time;
            thrown = true;
            lookAtHuman = false;
        }

        if ((thrown && Time.time - timeThrown > timeForThrow) || pointing)
        {

            lookAtHuman = true;
            if (Vector3.Distance(fetching.position, human.position) > throwDist)
            {
                startLook = false;
                control.chasing = true;
                InputBridge.Instance.VibrateController(0.1f, 0.1f, 0.1f, side.HandSide);
                thrown = false;
            }
            else
            {
                prevLooking = lookAt.position;
                startLook = true;
            }
            thrown = false;
        }
    }
}
