using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLevelGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] batch1;
    [SerializeField] GameObject[] batch2;
    [SerializeField] GameObject[] batch3;
    [SerializeField] GameObject[] batch4;
    [SerializeField] GameObject[] batch5;
    [SerializeField] GameObject[] batch6;
    [SerializeField] GameObject[] batch7;

    private void Start()
    {
        switch(GameManager.currentLevel)
        {
            case 2:
                foreach (GameObject box in batch1)
                {
                    box.SetActive(true);
                }
                break;
            case 3:
                foreach (GameObject box in batch2)
                {
                    box.SetActive(true);
                }
                break;
            case 4:
                foreach (GameObject box in batch3)
                {
                    box.SetActive(true);
                }
                break;
            case 5:
                foreach (GameObject box in batch4)
                {
                    box.SetActive(true);
                }
                break;
            case 6:
                foreach (GameObject box in batch5)
                {
                    box.SetActive(true);
                }
                break;
            case 7:
                foreach (GameObject box in batch6)
                {
                    box.SetActive(true);
                }
                break;
            case 8:
                foreach (GameObject box in batch7)
                {
                    box.SetActive(true);
                }
                break;
        } //activate the player box batches according to the level number

        #region Activation Of Previous Level Boxes

        if (GameManager.currentLevel < 9)
        {
            //activate the batch of the previous levels according to the current level
            if (GameManager.currentLevel > 1)
            {
                foreach (GameObject box in batch1)
                {
                    box.SetActive(true);
                }
            }
            if (GameManager.currentLevel > 2)
            {
                foreach (GameObject box in batch2)
                {
                    box.SetActive(true);
                }
            }
            if (GameManager.currentLevel > 3)
            {
                foreach (GameObject box in batch3)
                {
                    box.SetActive(true);
                }
            }
            if (GameManager.currentLevel > 4)
            {
                foreach (GameObject box in batch4)
                {
                    box.SetActive(true);
                }
            }
            if (GameManager.currentLevel > 5)
            {
                foreach (GameObject box in batch5)
                {
                    box.SetActive(true);
                }
            }
            if (GameManager.currentLevel > 6)
            {
                foreach (GameObject box in batch6)
                {
                    box.SetActive(true);
                }
            }
            if (GameManager.currentLevel > 7)
            {
                foreach (GameObject box in batch7)
                {
                    box.SetActive(true);
                }
            }
        }
        else if(GameManager.currentLevel == 9)
        {
            if(CompareTag("Finish") || CompareTag("Player")) //only on the finish obstacle wall and the player boxes parent
            {
                foreach(Transform child in transform) //for every child object of the current parent
                {
                    child.gameObject.SetActive(true); //spawn the child object
                }
            }
            else
            {
                foreach (Transform child in transform) //for every child object of the current parent
                {
                    child.gameObject.SetActive(false); //remove the child object
                }
            }
        }
        
        #endregion
    }

    private void Update()
    {
        #region BatchTesting
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (GameObject box in batch1)
            {
                box.SetActive(!box.activeSelf);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (GameObject box in batch2)
            {
                box.SetActive(!box.activeSelf);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (GameObject box in batch3)
            {
                box.SetActive(!box.activeSelf);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            foreach (GameObject box in batch4)
            {
                box.SetActive(!box.activeSelf);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            foreach (GameObject box in batch5)
            {
                box.SetActive(!box.activeSelf);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            foreach (GameObject box in batch6)
            {
                box.SetActive(!box.activeSelf);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            foreach (GameObject box in batch7)
            {
                box.SetActive(!box.activeSelf);
            }
        }
        #endregion
    }
}
