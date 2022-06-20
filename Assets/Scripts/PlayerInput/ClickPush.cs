using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPush : MonoBehaviour
{
    private Vector3 particleOffset;

    private void Start()
    {
        particleOffset = new Vector3(0, 0, 4.5f);
    }

    private void OnMouseDown()
    {
        if(GameManager.gameActive)
        {
            if (GameManager.instance.tapsRemaining > 0) //if there is still a shot remaining
            {
                SoundManager.instance.gameAudio.PlayOneShot(SoundManager.instance.tapSound, 0.5f); //play the tap sound

                GameManager.instance.tapsRemaining--; //use up a shot

                UIController.instance.shotsText.text = GameManager.instance.tapsRemaining.ToString(); //update the shots left text

                GameManager.instance.boxesLeft--; //reduce the count of active boxes for the level end

                Instantiate(ParticleManager.instance.tapParticle, transform.position - particleOffset, transform.rotation); //spawn a particle object at the box's position
                Destroy(gameObject); //destroy the tapped box
            }
            else
            {
                SoundManager.instance.gameAudio.PlayOneShot(SoundManager.instance.tap2Sound, 0.5f); //play the tap sound
            }
        }
    }
}
