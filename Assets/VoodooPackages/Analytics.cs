using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analytics : MonoBehaviour
{
    public static Analytics instance;
    private void Awake()
    {
        instance = this;
    }
    public string levelNumber;
    public void LevelStart()
    {
        levelNumber = GameManager.currentLevel.ToString(); //get the current level number
        TinySauce.OnGameStarted(levelNumber); //fire the event
        Debug.Log("Level " + levelNumber + " started!");
    }

    public void LevelFail()
    {
        levelNumber = GameManager.currentLevel.ToString(); //get the current level number
        TinySauce.OnGameFinished(false, GameManager.score, levelNumber);
        Debug.Log("Level " + levelNumber + " failed!");
    }

    public void LevelWin()
    {
        levelNumber = GameManager.currentLevel.ToString(); //get the current level number
        TinySauce.OnGameFinished(true, GameManager.score, levelNumber);
        Debug.Log("Level " + levelNumber + " beaten!");
    }
}
