using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip correctAnswerClip;
    [SerializeField, Range(0, 1)] float correctAnswerClipVolume;

    [SerializeField] AudioClip wrongAnswerClip;
    [SerializeField, Range(0, 1)] float wrongAnswerClipVolume;
    static AudioManager instance;
    void Awake()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void PlayCorrectAnswerSFX()
    {
        AudioSource.PlayClipAtPoint(correctAnswerClip, Camera.main.transform.position, correctAnswerClipVolume);
    }

    public void PlayWrongAnswerSFX()
    {
        AudioSource.PlayClipAtPoint(wrongAnswerClip, Camera.main.transform.position, wrongAnswerClipVolume);
    }

}
