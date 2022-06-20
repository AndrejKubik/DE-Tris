using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    #region Singleton
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject startPanel;
    public GameObject defeatPanel;
    public GameObject victoryPanel;
    public GameObject endGamePanel;

    public GameObject restartButton;
    public GameObject startMenuButton;
    public GameObject defeatMessage;

    public TextMeshProUGUI shotsText;
    public GameObject shotsTextParent;

    public TextMeshProUGUI scoreText;
    public GameObject scoreTextParent;
    public GameObject scoreTextShadow;
    public TextMeshProUGUI FinalScoreText;

    public List<TextMeshProUGUI> feverMessages;
    private TextMeshProUGUI message;

    public List<TextMeshProUGUI> failMessages;

    public float feverFadeTime;
    private bool isFull = false;

    private void Start()
    {
        if (GameManager.currentLevel == 0)
        {
            startPanel.SetActive(true); //if the game is just turned on, show start screen
            scoreTextParent.SetActive(false); //hide the number of remaining taps
        }
        else if(GameManager.currentLevel > 0) //if a new level is started
        {
            shotsTextParent.SetActive(true); //show the number of remaining taps
            shotsText.text = GameManager.instance.tapsRemaining.ToString(); //set the shots left to the according value
            scoreTextShadow.SetActive(true); //show the text shadow
        }
    }

    private void Update()
    {
        if (isFull) StartCoroutine(FadeOUT(message, feverFadeTime)); //fade out the shown fever message
    }

    public void RestartGame()
    {
        GameManager.gameActive = true;
        GameManager.instance.movementAllowed = true; //allow movement of boxes
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //restart current scene
    }

    public void NextLevel()
    {
        GameManager.currentLevel++; //switch to the next game level

        GameManager.gameActive = true; //un-pause the game(since it is paused on start)
        //GameManager.instance.movementAllowed = true; //allow movement of boxes
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //restart current scene
    }

    public void StartOver()
    {
        Analytics.instance.LevelStart();
        GameManager.currentLevel = 1; //start a new run from level 1
        GameManager.score = 0; //reset the score
        GameManager.gameActive = true; //un-pause the game(since it is paused on start)

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //restart current scene
    }

    public void StartGame()
    {
        Analytics.instance.LevelStart();
        GameManager.gameActive = true; //change the game state
        GameManager.instance.movementAllowed = true; //allow movement of boxes
        startPanel.SetActive(false); //hide the start panel
        shotsTextParent.SetActive(true); //show the shots left text
        shotsText.text = GameManager.instance.tapsRemaining.ToString(); //set the shots left to the according value
        GameManager.currentLevel = 1; //set the current level to the first
        GameManager.instance.UpdateActiveBoxes();
        GameManager.score = 0; //reset the player score
        scoreText.text = GameManager.score.ToString(); //set the shots left to the according value
        scoreTextParent.SetActive(true); //show the number of remaining taps
        scoreTextShadow.SetActive(true); //show the text shadow
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        GameManager.currentLevel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //restart current scene
    }

    public void FlashRandomFeverMessage(List<TextMeshProUGUI> messages, float fadeTime)
    {
        int random = Random.Range(0, messages.Count); //choose random message from the list
        message = messages[random]; //store the chosen message for easier use

        isFull = false;

        StartCoroutine(FadeIN(message, fadeTime));
    }

    IEnumerator FadeIN(TextMeshProUGUI message, float fadeTime)
    {
        while (message.color.a < 1f)
        {
            message.color = new Color(message.color.r, message.color.g, message.color.b, message.color.a + (Time.deltaTime / fadeTime));
            yield return null;
        }
        isFull = true;
    }
    IEnumerator FadeOUT(TextMeshProUGUI message, float fadeTime)
    {
        while (message.color.a > 0f)
        {
            message.color = new Color(message.color.r, message.color.g, message.color.b, message.color.a - (Time.deltaTime / fadeTime));
            yield return null;
        }
        isFull = false;
    }

    public IEnumerator CountTotalScore(float delay)
    {
        shotsTextParent.SetActive(false); //hide the shots left count
        scoreTextShadow.SetActive(false); //hide the text shadow
        scoreTextParent.SetActive(false); //hide small score count

        int finalScore = 0;

        while (finalScore < GameManager.score)
        {
            finalScore++; //increment the score
            FinalScoreText.text = finalScore.ToString(); //update the score
            SoundManager.instance.gameAudio.PlayOneShot(SoundManager.instance.winBoop, 0.1f); //play the tap sound
            yield return new WaitForSeconds(delay / 3f); //hol' up!
        }
    }

    public void ReturnToCheckpoint()
    {
        GameManager.instance.shouldRewind = true;
    }
}
