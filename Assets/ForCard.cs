using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCard : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public void OpenInventory()
    {
        inventoryManager.CardClicked();
    }

}
