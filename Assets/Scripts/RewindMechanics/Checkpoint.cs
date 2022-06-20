using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool jobDone;

    private void OnTriggerEnter(Collider other)
    {
        if(!jobDone) //if job bellow is not done already
        {
            GameManager.instance.checkpointReached = true; //trigger the boxes points in time before the checkpoint
            jobDone = true; //block multiple triggers
        }
    }
}
