using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDoor : MonoBehaviour
{
    [Header("애초에 열 수 있는 문인지")]
    public bool canOpen;
    [Header("canOpen이 true인 상태에서 잠긴 문인지")]
    public bool isLocked;
    [Header("문 자체가 열여있는 상태인지")]
    public bool isOpen = false;

    public void DoorClicked()
    {
        if (isOpen == false)
        {
            Open();
            isOpen = true;
        }
        else
        {
            Close();
            isOpen = false;
        }
    }


    public void Open()
    {
        GetComponent<Animator>().SetBool("isOpen", true);
        
    }

    public void Close()
    {
        GetComponent<Animator>().SetBool("isOpen", false);
    }

}
