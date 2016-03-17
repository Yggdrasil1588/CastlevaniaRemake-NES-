using UnityEngine;

// Author: J.Anderson
// Majority of the script referenced from 'C# Game Programming Cookbook for Unity 3D'

public class TimerClass
{
    public bool isTimerRunning = false;
    public bool isCountdownRunning = false;

    float timeElapsed = 0.0f;
    float currentTime = 0.0f;
    float startTime = 0.0f;
    float lastTime = 0.0f;
    float timeScaleFactor = 1.1f; //<-- If you need to scale time, change this!

    string timeString;
    // string hour;
    string minutes;
    string seconds;
    //string mills;

    // int aHour;
    int aMinute;
    int aSecond;
    //int aMillis;
    int temp;
    //int aTime;

    //GameObject callback;

    public void UpdateTimer()
    {
        // calculate the time elapsed since the last Update()
        timeElapsed = Mathf.Abs(Time.realtimeSinceStartup - lastTime);

        // if the timer is running, we add the time elapsed to the 
        // current time (advancing the timer)
        if (isTimerRunning)
        {
            currentTime += timeElapsed * timeScaleFactor;
        }
        // if countdown is running, we take time elapsed from
        // the set startTime.
        if (isCountdownRunning)
        {
            currentTime -= timeElapsed * timeScaleFactor;
        }
        // store the current time so that we can use it on the next 
        // update
        lastTime = Time.realtimeSinceStartup;
    }

    public void StartTimer()
    {
        //set up initial variables to start the timer
        isTimerRunning = true;
        lastTime = Time.realtimeSinceStartup;
    }

    public void SetCountdown(int min, int sec/*, int milisec*/)
    {
        //set up initial variables for countdown
        //call this method instead of StartTimer() for a countdown
        isCountdownRunning = true;

        // set countdown minutes
        float minuits = min * 60;

        // set countdown seconds
        float seconds = sec;

        // set countdown milliseconds
        //float milliseconds = milisec / 1000f;

        // set total coundown time as float so the timer can use it
        startTime = minuits + seconds /*+ milliseconds*/;
        currentTime = startTime; //set current time to whatever start time is chosen

        lastTime = Time.realtimeSinceStartup;
    }

    public void StopTimer()
    {
        //stop the timer
        isTimerRunning = false;
        isCountdownRunning = false;
    }

    public void ResetTimer()
    {
        //reset timer will set the timer back to zero
        timeElapsed = 0.0f;
        currentTime = 0.0f;
        lastTime = Time.realtimeSinceStartup;
        if (isCountdownRunning)
        {
            currentTime = startTime;
        }
    }

    public string GetFormattedTime()
    {
        // carry out an update to the timer so it is 'up to date'
        UpdateTimer();

        // grab minutes
        aMinute = (int)currentTime / 60;
        aMinute = aMinute % 60;

        // grab seconds
        aSecond = (int)currentTime % 60;

        // grab milliseconds
        //aMillis = (int)(currentTime * 100) % 100;

        // format strings for individual mm/ss/mills
        temp = (int)aSecond;
        seconds = temp.ToString();
        if (seconds.Length < 2)
            seconds = "0" + seconds;

        temp = (int)aMinute;
        minutes = temp.ToString();
        if (minutes.Length < 2)
            minutes = "0" + minutes;

        //temp = (int)aMillis;
        //mills = temp.ToString();
        //if (mills.Length < 2)
        //    mills = "0" + mills;

        // pull together a formatted string to return
        timeString = minutes + ":" + seconds /*+ ":" + mills*/;

        return timeString;
    }

    public void AddTime(int seconds)
    {
        // for adding time to the countdown or timer
        currentTime += seconds;
    }

    public void SubtractTime(int seconds)
    {
        // for subtracting time from the countdown or timer
        currentTime -= seconds;
    }

    public int GetTime()
    {
        // remember to call UpdateTimer() before trying to use this  
       // function, otherwise the time value will not be up to date     
        return (int)(currentTime);
    }
}
