using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI congratulationText;

    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    // Start is called before the first frame update
    public void ShowFinalScore()
    {
        congratulationText.text = "Congratulations \n You scored:" + scoreKeeper.CalulateScore() + "%";
    }

    
}
