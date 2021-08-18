using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public GameObject PinCanvas;
    public mouselook _mouselook;
    public UIManager uIManager;

    public void Pin()
    {
        PinCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        _mouselook.canMouseMove = false;

        // QE 입력 안되게
        uIManager.canUI = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && PinCanvas.activeSelf)
        {
            PinOut();
        }
    }

    public void PinOut()
    {
        PinCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        _mouselook.canMouseMove = true;

        uIManager.canUI = true;
    }
}
