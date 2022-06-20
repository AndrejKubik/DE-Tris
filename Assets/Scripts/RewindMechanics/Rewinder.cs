using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewinder : MonoBehaviour
{
    public bool isRewinding = false;

    private List<PointInTime> pointsInTime;
    private Rigidbody rb;
    private RigidbodyConstraints startConstraints;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //every box stores it's rigidbody

        pointsInTime = new List<PointInTime>(); //list initialization

        if(rb != null) startConstraints = rb.constraints; //store the default constraints
    }
    private void Update()
    {
        if (GameManager.instance.checkpointReached) //when a checkpoint has been reached
        {
            int pointsCount = pointsInTime.Count;
            for(int i = 0; i < pointsCount; i++)
            {
                pointsInTime.RemoveAt(0); //clear the latest point in time from the list
            }
            GameManager.instance.checkpointReached = false; //reset the checkpoint trigger
        }

        if (GameManager.instance.shouldRewind) //if the checkpoint button has been pressed
        {
            StartCoroutine(StartRewindAfter(0.1f));
        }
    }
    private void FixedUpdate()
    {
        //record the boxes' state in time whenever the rewind is not in progress
        if (isRewinding) Rewind();
        else RecordPosition();
    }
    public void StartRewind()
    {
        GameManager.gameActive = true; //make the game active again
        isRewinding = true; //activate the rewind state
        if (rb != null) rb.isKinematic = true; //if there is a rigidbody on the current object set it to kinematic
        Time.timeScale = 2f; //speed-up the game time
        UIController.instance.defeatPanel.SetActive(false); //hide the defeat screen
    }

    public void StopRewind()
    {
        if(rb != null) //if there is a rigidbody component on the current object
        {
            rb.constraints = startConstraints; //reset the constraints to as they were before the rewind itself
            rb.isKinematic = false; //remove the object from kinematic state
        }

        Time.timeScale = 1f; //reset the game speed
        isRewinding = false; //de-activate the rewind state
    }

    void RecordPosition()
    {
        if (GameManager.gameActive) //while the game is in progress
        {
            //store the current state in time of the object in the according list
            pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
        }

        //store the current state in time of the object in the according list
        //pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0) //if there is at least 1 memorised state in time of the object
        {
            PointInTime previousPoint = pointsInTime[0]; //take the latest point in time
            transform.position = previousPoint.position; //change the current object's position to the previous one
            transform.rotation = previousPoint.rotation; //change the current object's rotation to the previous one
            pointsInTime.RemoveAt(0); //clear the latest point in time from the list
        }
        else StopRewind();
    }

    IEnumerator StartRewindAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartRewind();
        GameManager.instance.shouldRewind = false; //reset the rewind trigger
    }
}
