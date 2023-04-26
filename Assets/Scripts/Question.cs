using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questionList = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;

    [Header("Button Images")]
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;

    [Header("Timer Image")]
    [SerializeField] Image timerImage;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress")]
    [SerializeField] Slider progressBar;

    Timer timerScript;
    AudioManager audioManager;

    int correctAnswerIndex;
    bool hasAnsweredEarly = true;
    public bool isGameComplete;

    void Awake()
    {
        timerScript = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Start()
    {
        progressBar.maxValue = questionList.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timerScript.fillFraction;

        if(timerScript.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isGameComplete = true;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timerScript.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timerScript.isAnswerQuestion)
        {
            DisplayAnswers(-1);
            ButtonState(false);
        }
    }

    void GetNextQuestion()
    {
        if(questionList.Count > 0)
        {
            ButtonState(true);
            GetRandomQuestion();
            SetDefaultSprites();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
        
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questionList.Count);
        currentQuestion = questionList[index];

        if(questionList.Contains(currentQuestion))
        {
            questionList.Remove(currentQuestion);
        }    
    }
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI answerButtonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            answerButtonText.text = currentQuestion.GetAnswer(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswers(index);
        ButtonState(false);
        timerScript.CancelTimer();
        
        scoreText.text = "Score: " + scoreKeeper.CalulateScore() + "%";
    }

    void DisplayAnswers(int index)
    {
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Thats Correct. Well Done!";
            audioManager.PlayCorrectAnswerSFX();
            Image correctButtonImage = answerButtons[index].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }

        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswerText = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Unlucky. The correct answer was: \n" + correctAnswerText;
            audioManager.PlayWrongAnswerSFX();
            Image correctButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
        }
    }

    void ButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image defaultImage = answerButtons[i].GetComponent<Image>();
            defaultImage.sprite = defaultSprite;
        }
        
    }

    
}
