using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCard : MonoBehaviour
{

    public int 아이템코드;
    public InventoryManager inventoryManager;

    public void OpenInventory()
    {
        inventoryManager.CardClicked(아이템코드);
    }

}
