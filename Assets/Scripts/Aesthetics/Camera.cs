using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject camTarget;
    [SerializeField] private Vector3 offset;

    private void Awake()
    {
        offset = transform.position - new Vector3(0, 20f, 0);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void FixedUpdate()
    {
        transform.position = camTarget.transform.position + offset; //move the camera object to the chosen target with set offset
    }
}
