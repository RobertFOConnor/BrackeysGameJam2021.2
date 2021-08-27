using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TimerHandler : MonoBehaviour
{
    public float startingTime = 300.0f; // all in seconds
    public float timeRemaining;

    [SerializeField]
    TextMeshProUGUI timerLabel;

    [SerializeField]
    Image timerFill;

    private void Start()
    {
        timeRemaining = startingTime;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        timerFill.fillAmount = timeRemaining / startingTime;
        timerLabel.text = ConvertToMinutesAndSeconds(timeRemaining);
        if (timeRemaining <= 0)
        {
            GameOver();
        }
    }

    string ConvertToMinutesAndSeconds(float time)
    {
        int mins = Mathf.FloorToInt(Mathf.RoundToInt(time) / 60);
        int seconds = Mathf.RoundToInt(time % 60);
        return mins.ToString() + ":" + seconds.ToString();
    }

    void GameOver()
    {
        FindObjectOfType<InGameUIHandler>().GameOver();
    }
}
