using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForPin : MonoBehaviour
{
    public ForDoor forDoor;
    public int password;
    public PinManager pinManager;

    public void Pin()
    {
        pinManager.Pin();
    }

}
