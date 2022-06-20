using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    void FixedUpdate()
    {
        if(GameManager.instance.movementAllowed)
        {
            transform.Translate(Vector3.forward * GameManager.instance.speed * Time.deltaTime, Space.World); //move the object forward by chosen speed over time
        }
    }
}
