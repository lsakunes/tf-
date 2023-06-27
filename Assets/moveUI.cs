using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUI : MonoBehaviour
{
    public Transform screen;
    public Transform finalUI;
    public int interpolationFrames = 100;
    public bool started;
    public bool open;
    private int frame;
    public bool reset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) return;
        Vector3 interpolatedPosition;
        Vector3 interpolatedRotation;
        Vector3 interpolatedScale;

        float interpolationRatio = (float)frame / interpolationFrames;
        if (reset)
            frame = 0;

        if (open)
        {

            interpolatedPosition = Vector3.Lerp(screen.position, finalUI.position, interpolationRatio);
            interpolatedRotation = Vector3.Lerp(screen.rotation.eulerAngles, finalUI.rotation.eulerAngles, interpolationRatio);
            interpolatedScale = Vector3.Lerp(screen.localScale, finalUI.localScale, interpolationRatio);
        }
        else
        {
            interpolatedPosition = Vector3.Lerp(finalUI.position, screen.position, interpolationRatio);
            interpolatedRotation = Vector3.Lerp(finalUI.rotation.eulerAngles, screen.rotation.eulerAngles, interpolationRatio);
            interpolatedScale = Vector3.Lerp(finalUI.localScale, screen.localScale, interpolationRatio);
        }


        frame += 1;

        transform.position = interpolatedPosition;
        transform.rotation = ToQ(interpolatedRotation);
        transform.localScale = interpolatedScale;
    }
    public static Quaternion ToQ(Vector3 v)
    {
        return ToQ(v.y, v.x, v.z);
    }
    public static Quaternion ToQ(float yaw, float pitch, float roll)
    {
        yaw *= Mathf.Deg2Rad;
        pitch *= Mathf.Deg2Rad;
        roll *= Mathf.Deg2Rad;
        float rollOver2 = roll * 0.5f;
        float sinRollOver2 = (float)Math.Sin((double)rollOver2);
        float cosRollOver2 = (float)Math.Cos((double)rollOver2);
        float pitchOver2 = pitch * 0.5f;
        float sinPitchOver2 = (float)Math.Sin((double)pitchOver2);
        float cosPitchOver2 = (float)Math.Cos((double)pitchOver2);
        float yawOver2 = yaw * 0.5f;
        float sinYawOver2 = (float)Math.Sin((double)yawOver2);
        float cosYawOver2 = (float)Math.Cos((double)yawOver2);
        Quaternion result;
        result.w = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
        result.x = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
        result.y = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
        result.z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;

        return result;
    }
}
