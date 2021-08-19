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
    public TalkManager talkManager;
    public int 현재조사중인아이템코드;
    public Animator animator;
    bool doorLock;
    public bool rayActived = true;

    private void Start()
    {
        rayActived = true;
    }

    void Update()
    {
        // 레이캐스트가 보이게
        Debug.DrawRay(transform.position, transform.forward * range, Color.green);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        // 실제 검사
        if (Physics.Raycast(ray, out hit, range) && rayActived)
        {
            if (hit.collider.CompareTag("talk"))
            {
                animator.SetBool("isCursor", true);
                uiText.text = "조사하기";
                현재조사중인아이템코드 = 0;

                if (Input.GetMouseButtonDown(0))
                {
                    uiText.text = "";
                    talkManager.TalkStart(hit.collider.gameObject);
                }

            }
            else if (hit.collider.CompareTag("door"))
            {
                animator.SetBool("isCursor", true);
                //  열여있으면 닫기 아니라면 열기 활성화
                if (hit.collider.gameObject.GetComponent<ForDoor>().isOpen && !doorLock)
                {
                    uiText.text = "닫기";
                }
                else if (!hit.collider.gameObject.GetComponent<ForDoor>().isOpen && !doorLock)
                {
                    uiText.text = "열기";
                }

                // 마우스 클릭했을때
                if (Input.GetMouseButtonDown(0))
                {
                    // 열수있는 문이고 잠금이 해제되어 있다면 문을 연다.
                    if (hit.collider.gameObject.GetComponent<ForDoor>().canOpen && !hit.collider.gameObject.GetComponent<ForDoor>().isLocked)
                    {
                        hit.collider.gameObject.GetComponent<ForDoor>().DoorClicked();
                    }
                    else
                    {
                        // 위의 조건이 아니라면 2초동안 잠겨있다를 보여준다.
                        uiText.text = "잠겨있다.";
                        doorLock = true;
                        Invoke("isDoorLock", 2);
                    }
                }
            }
            else if (hit.collider.CompareTag("paper"))
            {
                animator.SetBool("isCursor", true);
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
                animator.SetBool("isCursor", true);
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

                            // 아이템 피드백                        
                            uIManager.FeedBack(hit.collider.gameObject, "item");

                            break;
                        }
                    }
                }
            }
            else if (hit.collider.CompareTag("pin"))
            {
                animator.SetBool("isCursor", true);
                if (hit.collider.transform.parent.GetComponent<ForDoor>().isLocked)
                {
                    uiText.text = "입력하기";

                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.collider.GetComponent<ForPin>().Pin();
                    }
                }
                else
                {
                    uiText.text = "이미 해제된 단말이다";
                }
            }
            else if (hit.collider.CompareTag("card"))
            {
                animator.SetBool("isCursor", true);
                if (hit.collider.transform.parent.GetComponent<ForDoor>().isLocked)
                {
                    uiText.text = "사용하기";

                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.collider.GetComponent<ForCard>().OpenInventory();
                    }
                }
                else
                {
                    uiText.text = "이미 해제된 단말이다";
                }
            }
            else
            {
                animator.SetBool("isCursor", false);
                uiText.text = "";
                현재조사중인아이템코드 = 0;
                isDoorLock();
            }
        }
        else
        {
            animator.SetBool("isCursor", false);
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