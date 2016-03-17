using UnityEngine;
using UnityEngine.UI;


//Author: J.Anderson

public class TimerManager : MonoBehaviour 
{
    TimerClass timerClass = new TimerClass();

    public Text timerText;

    void Start()
    {
        timerClass.SetCountdown(5, 0);
    }

    void Update()
    {
        timerClass.UpdateTimer();
        timerText.text = "Time: " + timerClass.GetTime();
    }
}
