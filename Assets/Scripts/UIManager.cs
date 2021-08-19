using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject 인벤토리;
    public InventoryManager inventoryManager;
    public GameObject 기록;
    public PaperManager paperManager;
    public GameObject 페이퍼조사;
    public TextMeshProUGUI 페이퍼내용;
    public mouselook _mouselook;
    public GameObject tablet;
    bool canClosePaper;
    public bool canUI = true;

    [Header("피드백 버블")]
    public GameObject[] feedBack;

    [Header("개별적으로 조사할때 페이퍼")]
    public GameObject paperUI;

    [Header("현재 조사 중인 페이퍼")]
    public GameObject nowPaper;

    private void Update()
    {
        if (canUI)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !inventoryManager.selectMode)
            {
                if (기록.activeSelf == true)
                {
                    기록.SetActive(false);
                    tablet.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    _mouselook.canMouseMove = true;
                }
                else
                {
                    기록.SetActive(true);
                    paperManager.TypeColorReset();
                    tablet.SetActive(true);
                    인벤토리.SetActive(false);
                    Cursor.lockState = CursorLockMode.Confined;
                    _mouselook.canMouseMove = false;

                    paperManager.DeleteRecordData();
                }
            }

            // 선택모드가 아닐때만 작동
            if (Input.GetKeyDown(KeyCode.E) && !inventoryManager.selectMode)
            {
                if (인벤토리.activeSelf == true)
                {
                    인벤토리.SetActive(false);
                    tablet.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    _mouselook.canMouseMove = true;
                }
                else
                {
                    인벤토리.SetActive(true);                  
                    기록.SetActive(false);
                    tablet.SetActive(false);
                    Cursor.lockState = CursorLockMode.Confined;
                    _mouselook.canMouseMove = false;

                    inventoryManager.CheckItem();
                }
            }

            if (Input.GetKeyDown(KeyCode.F) && canClosePaper)
            {
                FeedBack(nowPaper, "paper");
                SavePaper();
            }
        }

    }

    public void ShowPaper(GameObject paper)
    {
        nowPaper = paper;
        paperUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        _mouselook.canMouseMove = false;
        canClosePaper = true;

        페이퍼내용.text =
            DataManager.instance.paperDatas[(int)nowPaper.GetComponent<ForPaper>().기록타입].기록목록[(int)nowPaper.GetComponent<ForPaper>().기록코드].내용;

    }

    public void SavePaper()
    {
        paperUI.SetActive(false);
        인벤토리.SetActive(false);
        기록.SetActive(false);
        tablet.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        _mouselook.canMouseMove = true;
        canClosePaper = false;

        // 해당 페이퍼 비활성화
        nowPaper.SetActive(false);

        // 조사 목록에 추가
        DataManager.instance.paperDatas[(int)nowPaper.GetComponent<ForPaper>().기록타입].기록목록[(int)nowPaper.GetComponent<ForPaper>().기록코드].기록활성여부 = true;
    }

    public void FeedBack(GameObject target, string type)
    {
        int bubbleIndex;

        // 남은 버블 확인
        for (int i = 0; i < feedBack.Length; i++)
        {
            if (!feedBack[i].transform.GetChild(0).gameObject.activeSelf)
            {
                bubbleIndex = i;

                feedBack[bubbleIndex].GetComponent<Animator>().SetTrigger("Show");

                // 텍스트 변경
                if (type == "item")
                {
                    feedBack[bubbleIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                        "아이템을 보관함에 추가하였습니다.";
                    feedBack[bubbleIndex].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                        DataManager.instance.itemDatas[target.GetComponent<ForItem>().아이템코드].이름;
                }
                else if (type == "paper")
                {
                    feedBack[bubbleIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                         "새로운 정보를 기록하였습니다.";
                    feedBack[bubbleIndex].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                        DataManager.instance.paperDatas[(int)target.GetComponent<ForPaper>().기록타입].기록목록[target.GetComponent<ForPaper>().기록코드].기록이름;

                    
                }

                break;
            }
        }
    }

}
