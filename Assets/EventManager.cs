using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public int nextScene;
    public List<float> eventTimes;
    public List<UnityEvent> events;
    public float nextTime;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < eventTimes.Count;i++)
        {
            if (eventTimes[i] <= Time.time - startTime) 
            {
                events[i].Invoke();
            }
        }
        if (nextTime == Time.realtimeSinceStartup)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
