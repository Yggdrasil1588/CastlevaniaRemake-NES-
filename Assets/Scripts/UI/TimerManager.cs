using UnityEngine;
using UnityEngine.UI;


//Author: J.Anderson

public class TimerManager : MonoBehaviour
{
    [HideInInspector]
    public TimerClass timerClass = new TimerClass();

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
