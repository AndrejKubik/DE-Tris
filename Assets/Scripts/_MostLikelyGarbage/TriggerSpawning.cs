using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawning : MonoBehaviour
{
    [SerializeField] GameObject[] wallSegments;
    private bool hasSpawned;
    private void OnTriggerEnter(Collider other)
    {
        //get random indexes to spawn
        int random1 = Random.Range(0, wallSegments.Length); 
        int random2 = Random.Range(0, wallSegments.Length);

        if(random1 != random2 && !hasSpawned) //if the two chosen are not the same
        {
            //spawn both
            wallSegments[random1].SetActive(true);
            wallSegments[random2].SetActive(true);

            hasSpawned = true;
        }
        else if(random1 == random2 && !hasSpawned) //in case they are the same one
        {
            random2++; //increment the second index

            //spawn both
            wallSegments[random1].SetActive(true);
            wallSegments[random2].SetActive(true);

            hasSpawned = true;
        }
    }
}
