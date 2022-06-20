using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistOnLoad : MonoBehaviour
{
    private GameObject[] instances;
    private void Awake()
    {
        instances = GameObject.FindGameObjectsWithTag("BGMusic");

        if (instances.Length > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }
}
