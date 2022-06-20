//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WallSegmentTrigger : MonoBehaviour
//{
//    private GameObject segment1;
//    private GameObject segment2;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (gameObject.CompareTag("Trigger1"))
//        {
//            if (GameManager.instance.randomIndex == 0)
//            {
//                segment1 = GameManager.instance.wallSegments[20];
//                segment2 = GameManager.instance.wallSegments[21];
//            }
//            else if (GameManager.instance.randomIndex == 1)
//            {
//                segment1 = GameManager.instance.wallSegments[18];
//                segment2 = GameManager.instance.wallSegments[17];
//            }
//            else if (GameManager.instance.randomIndex == 2)
//            {
//                segment1 = GameManager.instance.wallSegments[11];
//                segment2 = GameManager.instance.wallSegments[22];
//            }

//            segment1.SetActive(true);
//            segment2.SetActive(true);
//        }
//        else if(gameObject.CompareTag("Trigger2"))
//        {
//            if (GameManager.instance.randomIndex == 0)
//            {
//                segment1 = GameManager.instance.wallSegments[17];
//                segment2 = GameManager.instance.wallSegments[24];
//            }
//            else if (GameManager.instance.randomIndex == 1)
//            {
//                segment1 = GameManager.instance.wallSegments[16];
//                segment2 = GameManager.instance.wallSegments[23];
//            }
//            else if (GameManager.instance.randomIndex == 2)
//            {
//                segment1 = GameManager.instance.wallSegments[15];
//                segment2 = GameManager.instance.wallSegments[16];
//            }

//            Debug.Log(segment1.name);
//            Debug.Log(segment2.name);

//            segment1.SetActive(true);
//            segment2.SetActive(true);
//        }
//        else if (gameObject.CompareTag("Trigger3"))
//        {
//            GameManager.instance.gameActive = false; //change the game state
//            UIController.instance.victoryPanel.SetActive(true); //show the restart button
//            return; //skip the rest
//        }
//        GameManager.instance.tapsRemaining = GameManager.instance.shotsPerRound;
//        UIController.instance.shotsText.text = GameManager.instance.tapsRemaining.ToString(); //update the shots left text
//        transform.parent.gameObject.SetActive(false);
//    }
//}
