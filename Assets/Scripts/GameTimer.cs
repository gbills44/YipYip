using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem.LowLevel;

public class GameTimer : MonoBehaviour
{
    private bool b_activeTimer;
    private float currentTime;
    
    // TODO: below method is quick to implement but inefficient. Fix if time
    [SerializeField] private TMP_Text timerText;

    // Used to convert the time to a better format for display
    private TimeSpan timeDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = 0.0f;

        // For testing, should start timer in Game script
        b_activeTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        // check if timer is running
        if(b_activeTimer)
        {  
            // increment time seperate from cpu
            currentTime += Time.deltaTime;
        }

        // convert currentTime to text and format for display
        timeDisplay = TimeSpan.FromSeconds(currentTime);
        timerText.text = timeDisplay.Minutes.ToString() + ":" + timeDisplay.Seconds.ToString() + ":" + timeDisplay.Milliseconds.ToString();
    }

    public void StartGameTimer()
    {
        b_activeTimer = true;
    }

    public void StopGameTimer()
    {
        b_activeTimer = false;
    }

    public bool Get_ActiveTimer()
    {
        return b_activeTimer;
    }

    public void Set_ActiveTimer(bool p_active)
    {
        b_activeTimer = p_active;
    }

    public float Get_CurrentTime()
    {
        return currentTime;
    }

    public void Set_CurrentTime(float p_time)
    {
        currentTime = p_time;
    }
}
