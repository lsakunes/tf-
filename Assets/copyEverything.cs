using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class copyEverything : MonoBehaviour
{
    private Vector3 initLocation;
    private Quaternion initRotation;
    private Vector3 copyInitLocation;
    private Quaternion copyInitRotation;
    public Transform copy;
    public bool collide;
    // Start is called before the first frame update
    void Start()
    {
        initLocation = transform.position;
        initRotation = transform.rotation;
        copyInitLocation = copy.position;
        copyInitRotation = copy.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (collide)
        {
            //transform.Translate(transform.position - initLocation + copy.position - copyInitLocation);
        }
        else
        {
            transform.position = initLocation + copy.position - copyInitLocation;
        }
        transform.rotation = ToQ(initRotation.eulerAngles + copy.rotation.eulerAngles - copyInitRotation.eulerAngles);
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
