using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LerpToLook : MonoBehaviour
{
    public bool first;
    public Transform target;
    public Transform lookAt;
    public int frames;
    private int frame;
    private Vector3 startLook;
    private Vector3 startLoc;
    public bool move;
    public float distTo;
    public bool activeLook;
    private Vector3 prevLoc;
    List<float> speeds;
    public int numSpeeds;
    public float speedMult;
    float speed;
    public float lerp;
    public Transform human;
    public Transform humanPos;
    public float humandist;
    public float distMult;
    public bool done;
    bool readyToDone;
    private void Start()
    {
        prevLoc = transform.position;
        speeds = new List<float>();
        for (int x = 0; x < numSpeeds; x++)
        {
            speeds.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        speeds.Add(Vector3.Distance(transform.position, prevLoc) * lerp);
        speeds.RemoveAt(0);
        speed = (sumArrList(speeds) / numSpeeds + 0.1f) * speedMult;
        GetComponent<Animator>().SetFloat("speed", speed);
        prevLoc = transform.position;
        if (first)
        {
            frame = 0;
            startLook = transform.forward * 3;
            startLoc = lookAt.position;
            first = false;
        }
        float interpolationRatio = (float)frame / frames;
        RaycastHit hit;
        if (Physics.Raycast(target.position, Vector3.down, out hit, Mathf.Infinity, 1 << 3) && activeLook)
        {
            transform.LookAt(Vector3.Lerp(startLook, hit.point, interpolationRatio));
        }
        lookAt.position = Vector3.Lerp(startLoc, target.position, interpolationRatio);
        frame++;

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            if (move && Vector2.Distance(transform.position, target.position) > distTo * distMult && !done)
            {
                agent.SetDestination(target.position - Vector3.Normalize(target.position - transform.position) * distTo);
                activeLook = false;
                //readyToDone = true;
            }
            else
            {
                if (readyToDone)
                {
                    done = true;
                    readyToDone = false;
                }
                agent.SetDestination(transform.position);
                activeLook = true;
            }
        }
        if (Vector3.Distance(transform.position, humanPos.position) < humandist)
        {
            transform.position = transform.position + Vector3.Normalize(transform.position - humanPos.position) * (humandist - Vector3.Distance(transform.position, humanPos.position));
        }
    }

    private float sumArrList(List<float> list)
    {
        float sum = 0;
        foreach (float x in list)
        {
            sum += x;
        }
        return sum;
    }
}
