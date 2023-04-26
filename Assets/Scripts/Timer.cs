using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswerQuestion;
    [SerializeField] float timeToSeeCorrectAnswer;

    public bool isAnswerQuestion;
    public bool loadNextQuestion;

    public float fillFraction;
    float timerValue;

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0f;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(isAnswerQuestion)
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToAnswerQuestion;
            }

            else
            {
                isAnswerQuestion = false;
                timerValue = timeToSeeCorrectAnswer;
            }
            
        }

        else
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToSeeCorrectAnswer;
            }

            else
            {
                isAnswerQuestion = true;
                timerValue = timeToAnswerQuestion;
                loadNextQuestion = true;
            }
        }

       
    }


}
