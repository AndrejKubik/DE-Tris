using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] wallSegments;
    [SerializeField] private GameObject[] playerBoxesTotal;

    [SerializeField] private List<GameObject> activeWalls;
    private bool triggered;

    private int random1;
    private int random2;
    private int random3;

    [SerializeField] private GameObject[] finishBatch1;
    [SerializeField] private GameObject[] finishBatch2;

    private Vector3 particleOffset;

    private bool shouldResetScore = false;

    public GameEvent levelEnd;

    private void Start()
    {
        particleOffset = new Vector3(0, 0, 17f);
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        activeWalls = new List<GameObject>(); //make an empty list for active walls

        for (int i = 0; i < playerBoxesTotal.Length; i++) //check every player box in the scene
        {
            if (playerBoxesTotal[i].activeInHierarchy == true) //if the current box is active
            {
                activeWalls.Add(wallSegments[i]); //add the according obstacle to the spawning process
            }
        }

        if(GameManager.currentLevel < 9)
        {
            random1 = Random.Range(0, activeWalls.Count); //get random index to spawn 1st obstacle box
            activeWalls[random1].SetActive(true); //spawn the first obstacle box

            if (GameManager.currentLevel > 2)
            {
                random2 = random1; //equilize the 2 indexes for same-number check

                while (random2 == random1) //if the second index is the same as the first
                {
                    random2 = Random.Range(0, activeWalls.Count); //get random index to spawn 2nd obstacle box
                    if (random2 != random1) //when second index is different from the first
                    {
                        activeWalls[random2].SetActive(true); //spawn the 2nd obstacle box
                        break;
                    }
                }
            }

            if (GameManager.currentLevel > 5)
            {
                random3 = random1;

                while (random3 == random1 || random3 == random2) //if the 3rd index matches any previous
                {
                    random3 = Random.Range(0, activeWalls.Count);//get random index to spawn 3rd obstacle box
                    if (random3 != random1 && random3 != random2) //when 3rd index is unique
                    {
                        activeWalls[random3].SetActive(true); //spawn the 3rd obstacle box
                        break;
                    }
                }
            }
        }
        else if(GameManager.currentLevel == 9) //if the level is the final level
        {
            if(GameManager.instance.random == 1)
            {
                if(finishBatch1 != null)
                {
                    foreach (GameObject box in finishBatch1)
                    {
                        box.SetActive(true);
                        GameManager.instance.shotsPerRound = finishBatch1.Length;
                    }
                }
            }
            else if(GameManager.instance.random == 2)
            {
                if(finishBatch2 != null)
                {
                    foreach (GameObject box in finishBatch2)
                    {
                        box.SetActive(true);
                        GameManager.instance.shotsPerRound = finishBatch2.Length;
                    }
                }
            }
        }
    }

    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.gameActive)
        {
            if (!triggered)
            {
                Instantiate(ParticleManager.instance.obstacleParticle, transform.position - particleOffset, transform.rotation); //spawn a particle

                if (GameManager.currentLevel == 9) //in the boss level
                {
                    if (gameObject.CompareTag("Trigger1")) //upon passing the first obstacle in the boss level
                    {
                        shouldResetScore = true; //reset score

                        StartCoroutine(LevelWinSystem(UIController.instance.endGamePanel, GameManager.instance.winParticlesDelay)); //play the particle light show
                    }

                    UIController.instance.shotsTextParent.SetActive(false); //hide the taps remaining text
                }
                else if (GameManager.currentLevel < 9) //in a common level
                {
                    if (gameObject.CompareTag("Trigger3")) //upon passing the final obstacle 
                    {
                        StartCoroutine(LevelWinSystem(UIController.instance.victoryPanel, GameManager.instance.winParticlesDelay)); //play the particle light show
                    }
                    
                    if (GameManager.instance.tapsRemaining > 0)
                    {
                        UIController.instance.FlashRandomFeverMessage(UIController.instance.feverMessages, UIController.instance.feverFadeTime); //show random fever message
                        GameManager.instance.scoreTemp += GameManager.instance.tapsRemaining; //add the remaining taps to the score count
                        UIController.instance.scoreText.text = GameManager.score + "+" + GameManager.instance.scoreTemp; //update the score UI
                    }
                    
                    if(!gameObject.CompareTag("Trigger3")) //if it isn't the final obstacle in a level
                    {
                        GameManager.instance.tapsRemaining = GameManager.instance.shotsPerRound; //renew shot count
                        UIController.instance.shotsText.text = GameManager.instance.tapsRemaining.ToString(); //update the shots left text
                    }
                }

                SoundManager.instance.gameAudio.PlayOneShot(SoundManager.instance.obstaclePass, 0.5f); //play the obstacle sound
                triggered = true; //block the repetition of this method
            }

            //if(GameManager.currentLevel < 9) Destroy(transform.parent.gameObject); //nzm sto sam ovo stavio al mozda zatreba
        }
    }
    #endregion

    IEnumerator LevelWinSystem(GameObject panel, float delay)
    {
        Analytics.instance.LevelWin();

        Instantiate(ParticleManager.instance.obstacleParticle, transform.position - particleOffset, transform.rotation); //spawn a particle

        GameManager.gameActive = false; //change the game state

        List<GameObject> boxes = new List<GameObject>(); //make an empty list to store the all of the used boxes

        foreach (GameObject box in playerBoxesTotal)
        {
            if(box != null && box.activeInHierarchy == true)
            {
                boxes.Add(box); //add the current box to the list for later
            }
        }

        yield return new WaitForSeconds(0.5f); //hol' up!

        if(GameManager.currentLevel == 9)
        {
            ExplosionController.instance.positionOffset = ExplosionController.instance.transform.position; //set the center of explosion to the center of boxes
            ExplosionController.instance.radius *= 2; //double the explosion radius
            ExplosionController.instance.force /= 2; //halve the force
            ExplosionController.instance.upForce *= 300; //increase the upforce
            ExplosionController.instance.shouldExplode = true; //trigger the explosion
        }

        yield return new WaitForSeconds(GameManager.instance.delay);

        GameManager.instance.movementAllowed = false; //block further movement

        GameManager.score += GameManager.instance.scoreTemp; //add the bonus score
        UIController.instance.shotsTextParent.SetActive(false); //hide the shots left count
        UIController.instance.scoreText.text = GameManager.score.ToString(); //update the score
        SoundManager.instance.gameAudio.PlayOneShot(SoundManager.instance.winBoop, 0.5f); //play the tap sound

        yield return new WaitForSeconds(0.3f); //wait for the chosen amount of seconds

        foreach (GameObject box in boxes)
        {
            Instantiate(ParticleManager.instance.winParticle, box.transform.position, box.transform.rotation); //spawn a particle on the current box
            SoundManager.instance.gameAudio.PlayOneShot(SoundManager.instance.winBoop, 0.5f); //play the tap sound
            Destroy(box); //destroy the current box
            GameManager.score++; //increment the score for every box destroyed
            UIController.instance.scoreText.text = GameManager.score.ToString(); //update the score
            yield return new WaitForSeconds(delay / 1.5f); //wait for the chosen amount of seconds
        }

        if (shouldResetScore)
        {
            GameManager.currentLevel = 0; //reset the game to the first level(0 because restart button increments the level number)

            StartCoroutine(UIController.instance.CountTotalScore(delay / 3f));
        }

        yield return new WaitForSeconds(delay); //wait for the chosen amount of seconds

        levelEnd.Raise();
    }
}
