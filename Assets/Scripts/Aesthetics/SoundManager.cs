using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public AudioSource gameAudio;

    public AudioClip winBoop; 
    public AudioClip failBam; 
    public AudioClip tapSound; 
    public AudioClip levelWin;
    public AudioClip tap2Sound;
    public AudioClip obstaclePass;
}
