using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSegmentsDependency : MonoBehaviour
{
    [SerializeField] private GameObject secondary1;
    [SerializeField] private GameObject secondary2;
    private void OnEnable()
    {
        secondary1.SetActive(true);
        secondary2.SetActive(true);
    }
}
