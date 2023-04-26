using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers;
    int questionsSeen;

    public int CorrectAnswers()
    {
        return correctAnswers;
    }  
    
    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int QuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public int CalulateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
