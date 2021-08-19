using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCard : MonoBehaviour
{

    public int 아이템코드;
    public InventoryManager inventoryManager;
    public GameObject 빨강패널;

    public void OpenInventory()
    {
        inventoryManager.CardClicked(gameObject);
    }

}
