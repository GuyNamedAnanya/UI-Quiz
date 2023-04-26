using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Question question;
    EndScreen endScreen;

    void Awake()
    {
        question = FindObjectOfType<Question>();
        endScreen = FindObjectOfType<EndScreen>();
    }
    // Start is called before the first frame update
    void Start()
    {
        question.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(question.isGameComplete)
        {
            endScreen.gameObject.SetActive(true);
            question.gameObject.SetActive(false);
            endScreen.ShowFinalScore();
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
