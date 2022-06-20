using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetachment : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float force;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall") && GameManager.gameActive) //when a box collides with a wall
        {
            Analytics.instance.LevelFail();

            Instantiate(ParticleManager.instance.failParticle, transform.position, transform.rotation); //spawn a particle

            SoundManager.instance.gameAudio.PlayOneShot(SoundManager.instance.failBam, 0.5f); //play the tap sound

            rb.constraints = RigidbodyConstraints.None; //unlock all movement of the hit box
            ExplosionController.instance.positionOffset = transform.position + new Vector3(0, 0, 2f); //set the explosion position to the hit walls
            ExplosionController.instance.shouldExplode = true; //trigger the explosion

            GameManager.instance.movementAllowed = false; //block further movement
            GameManager.gameActive = false; //change the game state

            UIController.instance.defeatPanel.SetActive(true); //show the defeat panel
            UIController.instance.shotsTextParent.SetActive(false); //hide the shots left count

            UIController.instance.FlashRandomFeverMessage(UIController.instance.failMessages, UIController.instance.feverFadeTime * 2f); //show random fever message
            UIController.instance.scoreTextShadow.SetActive(false); //hide the text shadow


            StartCoroutine(ShowButtonsAfter(1f)); //show buttons
        }
    }

    IEnumerator ShowButtonsAfter(float delay)
    {
        yield return new WaitForSeconds(UIController.instance.feverFadeTime * 2f); //hol' up
        GameManager.score -= 10; //decrease the score
        UIController.instance.scoreText.text = GameManager.score.ToString(); //update the score on screen

        yield return new WaitForSeconds(delay);

        UIController.instance.scoreTextParent.SetActive(false); //hide the score
        UIController.instance.restartButton.SetActive(true); //show the defeat panel
        UIController.instance.startMenuButton.SetActive(true); //show the defeat panel
        UIController.instance.defeatMessage.SetActive(true); //show the defeat panel
    }
}
