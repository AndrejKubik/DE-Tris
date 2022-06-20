using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public static bool gameActive;
    public bool movementAllowed;

    public float gravity;
    public float speed;

    public float speedEasy;
    public float speedMedium;
    public float speedHard;

    public int tapsRemaining = 0;
    public int shotsPerRound;
    public int obstacleNumber;

    public int boxesLeft = 0; //number of player boxes left

    public int random;

    public bool checkpointReached;
    public bool shouldRewind;

    public static int currentLevel;
    public static int score;
    public static int scoreMultiplier;
    public int scoreTemp;

    public float delay = 0;

    public float winParticlesDelay;

    [SerializeField] private GameEvent OnLevelFinished;

    [SerializeField] private List<Level> levels;

    private void Start()
    {
        Application.targetFrameRate = 60;

        UIController.instance.scoreText.text = score.ToString();

        if (currentLevel > 0) UpdateActiveBoxes();

        if (currentLevel == 0) movementAllowed = false;
        else movementAllowed = true;

        random = Random.Range(1, 3); //choose random final level

        Physics.gravity = new Vector3(0, -gravity, 0); //get stronger gravity

        Debug.Log("current level: " + currentLevel); //print the current level

        //change the difficulty stats according the the current level
        if (currentLevel <= 2)
        {
            shotsPerRound = 2;
            obstacleNumber = 1;
            speed = speedEasy;
        }
        else if (currentLevel > 2 && currentLevel <= 5)
        {
            shotsPerRound = 3;
            obstacleNumber = 2;
            speed = speedMedium;
            delay = 0.3f;
        }
        else if(currentLevel > 5 && currentLevel <= 8)
        {
            shotsPerRound = 4;
            obstacleNumber = 3;
            speed = speedHard;
            delay = 0.8f;
        }
        else if(currentLevel == 9) 
        {
            if (random == 1) shotsPerRound = 13;
            else if (random == 2) shotsPerRound = 14;
        }

        tapsRemaining = shotsPerRound; //give the player set number of tries per round

        if(currentLevel > 0) Analytics.instance.LevelStart();
    }

    private void Update()
    {
        if (currentLevel == 9 && tapsRemaining == 0) speed = speedEasy; //when the player uses all shots on the boss level, start moving

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLevel = 8; //last level shortcut
        }
    }

    public void UpdateActiveBoxes()
    {
        boxesLeft = levels[currentLevel - 1].startingActiveBoxNumber; //get the starting active box ammount
    }
}
