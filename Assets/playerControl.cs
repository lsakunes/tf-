using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerControl : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    private Vector3 prevPosition;
    private float prevRot;
    public float lerp;
    public Transform follow;
    public bool chasing;
    public bool pickup;
    public bool dropping;
    public bool pickupFinish;
    public Transform dropPos;
    public Transform pickPos;
    public List<float> speeds;
    public int numSpeeds;
    public float speed;
    public float speedMult;
    public float runMult;
    public float walkMult;
    public float turnMult;
    public float turn;
    public float turnAnim;
    public float pickLength;
    public float pickTime;
    public bool picking;
    public float pickExact;
    public float grabHeight;
    private void Start()
    {
        prevPosition = transform.position;
        speeds = new List<float>();
        for (int x = 0; x < numSpeeds; x++)
        {
            speeds.Add(0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if(!chasing && InputBridge.Instance.YButton && !dropping) { 
        //    chasing = true;
        //    InputBridge.Instance.VibrateController(0.1f, 0.2f, 0.5f, ControllerHand.Left);
        //}
        speeds.Add(Vector3.Distance(transform.position, prevPosition) * lerp);
        speeds.RemoveAt(0);
        speed = (sumArrList(speeds) / numSpeeds + 0.1f) * speedMult;
        turn = Mathf.Abs((transform.rotation.eulerAngles.y - prevRot) * turnMult) + 0.1f;
        animator.SetFloat("turn", turn);
        animator.SetFloat("turnAnim", Mathf.Clamp(turn * turnAnim, 0.3f, turnAnim * 2));
        animator.SetFloat("speed", speed);
        animator.SetFloat("walk", speed * walkMult);
        animator.SetFloat("runSpeed", speed*runMult);
        if (chasing && follow != null)
        {
            agent.SetDestination(follow.position + (transform.position - pickPos.position));
        }
        else if (dropping) ;
        else if (Input.GetMouseButtonDown(0))
        {
            Ray movePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(movePos, out var hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        else agent.SetDestination(transform.position);
        if (xzclose(pickPos, follow, 1f) && chasing && follow.position.y < grabHeight)
        {
            if (!picking)
            {
                pickTime = Time.time;
                picking = true;
            }
            pickup = true;
            animator.SetBool("pick", true);

        }

        if (Time.time - pickTime > pickLength * pickExact && pickup)
        {
            follow.position = pickPos.position;
            follow.parent = pickPos;
        }
        if (Time.time - pickTime > pickLength && pickup)
        {
            picking = false;
            animator.SetBool("pick", false);
            pickupFinish = true;
            pickup = false;
        }

        if (pickupFinish)
        {
            follow.GetComponent<Rigidbody>().Sleep();
            agent.SetDestination(dropPos.position);
            chasing = false;
            dropping = true;
            pickupFinish = false;
        }
        if (dropping)
        {
            follow.GetComponent<Rigidbody>().Sleep();

            follow.position = pickPos.position;
        }
        if (xzclose(transform,dropPos, 0.5f))
        {
            follow.parent = null;
            dropping = false;
        }
        prevPosition = transform.position;
        prevRot = transform.rotation.eulerAngles.y;
    }

    bool xzclose(Transform i, Transform j, float nearby)
    {
       return Mathf.Abs(i.position.z - j.position.z) < nearby && Mathf.Abs(i.position.x - j.position.x) < nearby;
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