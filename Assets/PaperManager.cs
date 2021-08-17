using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    public GameObject paper;
    public GameObject tablet;
    public mouselook _mouselook;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (paper.activeSelf == true)
            {
                paper.SetActive(false);
                tablet.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                _mouselook.canMouseMove = true;
            }
            else
            {
                paper.SetActive(true);
                tablet.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                _mouselook.canMouseMove = false;
            }
        }
    }
}
