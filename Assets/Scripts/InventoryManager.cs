using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotList;
    public GameObject[] slotLists;
    public Sprite 기본이미지;

    public Color 슬롯활성색;
    public Color 슬롯비활성색;

    [Header("타켓 UI")]
    public TextMeshProUGUI 아이템이름;
    public TextMeshProUGUI 아이템설명;

    [Header("선택모드 관련 변수")]
    public bool selectMode = false;
    public GameObject 선택버튼;
    public GameObject 선택UI;
    public mouselook _mouselook;
    public GameObject 인벤토리Canvas;
    public RayCast rayCast;

    void Start()
    {
        // 슬롯 리스트 등록
        for (int i = 0; i < slotList.transform.childCount; i++)
        {
            slotLists[i] = slotList.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        // 선택모드에서 종료
        if (Input.GetKeyDown(KeyCode.F) && selectMode)
        {
            Cursor.lockState = CursorLockMode.Locked;
            _mouselook.canMouseMove = true;
            selectMode = false;
            rayCast.rayActived = true;

            인벤토리Canvas.SetActive(false);
        }
    }

    public void CardClicked()
    {
        selectMode = true;
        Cursor.lockState = CursorLockMode.Confined;
        _mouselook.canMouseMove = false;

        인벤토리Canvas.SetActive(true);
        CheckItem();

        선택UI.SetActive(true);

        rayCast.rayActived = false;
    }

    public void CheckItem()
    {
        bool canInventory = true;
        DeleteInventory();

        선택UI.SetActive(false);
        선택버튼.SetActive(false);

        for (int i = 0; i < DataManager.instance.itemDatas.Length; i++)
        {
            // 데이터에서 활성화된 아이템 검사
            if (DataManager.instance.itemDatas[i].인벤토리여부 == true)
            {
                // 중복 아이템 검사
                for (int j = 0; j < slotLists.Length; j++)
                {
                    if (slotLists[j].GetComponent<ForSlot>().가진아이템코드 == DataManager.instance.itemDatas[i].코드)
                    {
                        // 아이템 중복
                        canInventory = false;
                        break;
                    }
                    else
                    {
                        canInventory = true;
                    }
                }

                if (canInventory)
                {
                    // 인벤토리에서 빈 공간 찾기
                    for (int j = 0; j < slotLists.Length; j++)
                    {
                        if (slotLists[j].GetComponent<ForSlot>().가진아이템코드 == 0)
                        {
                            slotLists[j].GetComponent<ForSlot>().가진아이템코드 = DataManager.instance.itemDatas[i].코드;
                            slotLists[j].GetComponent<ForSlot>().아이템이미지.sprite = DataManager.instance.itemDatas[i].이미지;
                            // slotLists[i].GetComponent<ForSlot>().아이템이미지.color = 슬롯활성색;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void DeleteInventory()
    {
        for (int i = 0; i < slotLists.Length; i++)
        {
            slotLists[i].GetComponent<ForSlot>().가진아이템코드 = 0;
            slotLists[i].GetComponent<ForSlot>().아이템이미지.sprite = 기본이미지;
            // slotLists[i].GetComponent<ForSlot>().아이템이미지.color = 슬롯비활성색;

            아이템이름.text = "";
            아이템설명.text = "";
        }
    }

    public void ItemClicked(ForSlot forSlot)
    {
        if (forSlot.가진아이템코드 != 0)
        {
            아이템이름.text = DataManager.instance.itemDatas[forSlot.가진아이템코드].이름;
            아이템설명.text = DataManager.instance.itemDatas[forSlot.가진아이템코드].설명;
        }

        if (selectMode)
        {
            선택버튼.SetActive(true);
        }

    }

}
