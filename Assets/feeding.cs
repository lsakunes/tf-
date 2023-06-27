using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feeding : MonoBehaviour
{
    public bool feed;
    public bool eating;
    public Transform foodPos;
    public Transform jaw;
    public float scale;
    public bool start;
    float startTime;
    int frame;
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;
    Vector3 norm;
    public Vector3 open;
    public float timeToEat;
    public Transform forward;

    private void Start()
    {
        norm = jaw.position;
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().Pause();
        LerpToLook looker = GetComponent<LerpToLook>();
        if (feed && foodPos != null) {
            looker.target = foodPos;
            looker.move = true;
            looker.distTo = 0.4f;
        }
        else if (eating)
        {
            GetComponent<AudioSource>().Play();
            if (start)
            {
                start = false;
                startTime = Time.time;
            }
            looker.target = forward;
            looker.move = false;
            looker.distTo = 2;

            float interpolationRatio = 1-((float)Mathf.Abs(interpolationFramesCount-elapsedFrames) / interpolationFramesCount);

            Vector3 interpolatedPosition = Vector3.Lerp(norm, norm + open, interpolationRatio);

            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount*2 + 1);
            jaw.position = interpolatedPosition;
            if (Time.time - startTime > timeToEat)
            {
                GetComponent<LerpToLook>().done = false;
                eating = false;
            }
        }
        else
        {
            looker.target = looker.human;
            looker.move = false;
            jaw.position = norm;
        }
    }
}
