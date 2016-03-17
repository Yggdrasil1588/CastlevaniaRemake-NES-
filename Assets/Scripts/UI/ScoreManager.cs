using UnityEngine;
using UnityEngine.UI;


//Author: J.Anderson

public class ScoreManager : MonoBehaviour 
{
    int score;
    public Text scoreText;

    void Stat()
    {
        score = 0;
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddScore(int amount)
    {
        score = score + amount;
    }

}
