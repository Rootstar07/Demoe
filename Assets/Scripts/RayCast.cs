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
       
    void Update()
    {
        // 레이캐스트가 보이게
        Debug.DrawRay(transform.position, transform.forward * range, Color.green);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        // 실제 검사
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.CompareTag("Door"))
            {
                uiText.text = "열기";
                현재조사중인아이템코드 = 0;
            }
            else if (hit.collider.CompareTag("Object"))
            {
                uiText.text = "조사하기";
                현재조사중인아이템코드 = 0;
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
            else
            {
                uiText.text = "";
                현재조사중인아이템코드 = 0;
            }
        }
        else
        {
            uiText.text = "";
            현재조사중인아이템코드 = 0;
        }
    }
}
