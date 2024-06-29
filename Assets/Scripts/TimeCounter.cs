using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    Text timeUI;

    float startTime;
    float ellapsedTime;
    bool startCounter;

    int minute;
    int second;
    // Start is called before the first frame update
    void Start()
    {
        startCounter = false;

        timeUI = GetComponent<Text>();
    }

    public void StartTimeCounter()
    {
        startTime = Time.time;
        startCounter = true;
    }

    public void StopTimeCounter()
    {
        startCounter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCounter)
        {
            ellapsedTime = Time.time - startTime;
            minute = (int) ellapsedTime/60;
            second = (int) ellapsedTime%60;

            timeUI.text = string.Format("{0:00}:{1:00}", minute, second);
        }   
    }
}
