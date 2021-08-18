using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayCast : MonoBehaviour
{
    public float range;
    public UIManager uIManager;
    public TextMeshProUGUI uiText;
    public PaperManager paperManager;
    public int 현재조사중인아이템코드;
    public Animator animator;
    bool doorLock;

    void Update()
    {
        // 레이캐스트가 보이게
        Debug.DrawRay(transform.position, transform.forward * range, Color.green);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        // 실제 검사
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.CompareTag("Object"))
            {
                uiText.text = "조사하기";
                현재조사중인아이템코드 = 0;
            }
            else if (hit.collider.CompareTag("door"))
            {
                if (hit.collider.gameObject.GetComponent<ForDoor>().isOpen && !doorLock)
                {
                    uiText.text = "닫기";
                }
                else if (!hit.collider.gameObject.GetComponent<ForDoor>().isOpen && !doorLock)
                {
                    uiText.text = "열기";
                }
                else
                {
                    uiText.text = "잠겨있다.";
                }

                // 문 열고 닫기
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.gameObject.GetComponent<ForDoor>().canOpen)
                    {
                        hit.collider.gameObject.GetComponent<ForDoor>().DoorClicked();
                    }
                    else
                    {
                        doorLock = true;
                        Invoke("isDoorLock", 2);
                    }
                }

            }
            else if (hit.collider.CompareTag("paper"))
            {
                uiText.text = "읽기";
                현재조사중인아이템코드 = 0;

                if (Input.GetMouseButtonDown(0))
                {
                    // 페이퍼 보여줌
                    uIManager.ShowPaper(hit.collider.gameObject);
                }

            }
            else if (hit.collider.CompareTag("item"))
            {
                uiText.text = "가져가기";
                현재조사중인아이템코드 = hit.collider.gameObject.GetComponent<ForItem>().아이템코드;

                if (Input.GetMouseButtonDown(0))
                {
                    // 아이템 획득
                    for (int i = 0; i < DataManager.instance.itemDatas.Length; i++)
                    {
                        if (DataManager.instance.itemDatas[i].코드 == 현재조사중인아이템코드)
                        {
                            hit.collider.gameObject.SetActive(false);
                            DataManager.instance.itemDatas[i].인벤토리여부 = true;
                            break;
                        }
                    }
                }
            }
            else if (hit.collider.CompareTag("pin"))
            {
                uiText.text = "사용하기";

                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.GetComponent<ForPin>().Pin();
                }
            }
            else
            {
                uiText.text = "";
                현재조사중인아이템코드 = 0;
                isDoorLock();
            }
        }
        else
        {
            uiText.text = "";
            현재조사중인아이템코드 = 0;
            isDoorLock();
        }
    }

    // 문이 잠겨있을때 text 변경
    public void isDoorLock()
    {
        doorLock = false;
    }
}