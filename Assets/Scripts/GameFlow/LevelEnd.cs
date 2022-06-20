using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private List<GameObject> boxes;

    [SerializeField] private float delay;
    [SerializeField] private float panelDelay;

    [SerializeField] private Transform cameraTarget;
    [SerializeField] private int cameraOffset = 7;

    [SerializeField] private GameObject multipliers;
    private GameObject finalBox;

    [SerializeField] private List<GameObject> multiplierColors;

    public void StartBoxSpawning()
    {
        StartCoroutine(BoxSpawning(delay, UIController.instance.victoryPanel, multipliers));
    }

    IEnumerator BoxSpawning(float delay, GameObject panel, GameObject multipliers)
    {
        multipliers.SetActive(true); //show score multipliers
        GameManager.scoreMultiplier = 5;
        int counter = 0; //counts boxes in a multiplier region
        int mCounter = 0;
        multiplierColors[mCounter].SetActive(true); //highlight first multiplier

        for (int i = 0; i < GameManager.instance.boxesLeft; i++) //do the following until the i reaches the number of current active player boxes
        {
            counter++; //increment the counter
            Debug.Log("counter: " + counter);

            if(counter > 5)
            {
                GameManager.scoreMultiplier *= 2; //increase the multiplier
                counter = 1; //reset the counter

                mCounter++; //choose next multiplier
                multiplierColors[mCounter].SetActive(true); //highlight current multiplier
            }

            UIController.instance.scoreText.text = GameManager.score + "+" + GameManager.scoreMultiplier; //show how much bonus points player gets

            boxes[i].SetActive(true); //show the current box
            finalBox = boxes[i]; //store the final box

            SoundManager.instance.gameAudio.PlayOneShot(SoundManager.instance.tapSound, 0.4f); //play the tap sound
            Instantiate(ParticleManager.instance.tapParticle, boxes[i].transform.position, transform.rotation); //spawn a particle object at the box's position

            if (i > cameraOffset)
            {
                Vector3 nextPos = new Vector3(cameraTarget.position.x, boxes[i - cameraOffset].transform.position.y, cameraTarget.position.z); //calculate next camera position
                cameraTarget.position = nextPos; //cameraTarget.position = Vector3.Slerp(cameraTarget.position, nextPos, delay); //change view smoothly towards the target position
            }

            yield return new WaitForSeconds(delay); //ayo hol' up!
        }

        Instantiate(ParticleManager.instance.levelEndParticle, finalBox.transform.position + new Vector3(0, 5f, 0), transform.rotation); //spawn a particle object at the box's position
        
        SoundManager.instance.gameAudio.PlayOneShot(SoundManager.instance.levelWin, 0.4f); //play the tap sound

        yield return new WaitForSeconds(panelDelay); //ayo hol' up!

        GameManager.score += GameManager.scoreMultiplier; //calculate the final score
        UIController.instance.scoreText.text = GameManager.score.ToString(); //update score text
        SoundManager.instance.gameAudio.PlayOneShot(SoundManager.instance.winBoop, 0.5f); //play the obstacle sound

        panel.SetActive(true);
        UIController.instance.scoreTextShadow.SetActive(false); //hide the text shadow
    }

    public void ShowVictoryScreen()
    {
        UIController.instance.endGamePanel.SetActive(true); //show victory panel
        SoundManager.instance.gameAudio.PlayOneShot(SoundManager.instance.levelWin, 0.4f); //play the tap sound
    }
}
