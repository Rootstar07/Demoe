using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperManager : MonoBehaviour
{
    public mouselook _mouselook;
    public GameObject 대화;
    public GameObject[] 기록목록;
    public TextMeshProUGUI 기록내용;
    public GameObject[] 기록타입;
    public Color 기록타입비활성색;
    public Color 기록타입활성색;


    private void Start()
    {
        for (int i = 0; i < 대화.transform.childCount; i++)
        {
            기록목록[i] = 대화.transform.GetChild(i).gameObject;
        }
    }

    // 기록 목록 초기화
    public void DeleteRecordData()
    {
        for (int i = 0; i < 기록목록.Length; i++)
        {
            if (기록목록[i].activeSelf)
            {
                기록목록[i].SetActive(false);
            }
        }
        기록내용.text = "";
    }

    public void TypeColorReset()
    {
        // 해당 버튼에 하이라이트
        for (int i = 0; i < 기록타입.Length; i++)
        {
            기록타입[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = 기록타입비활성색;
        }

        for (int i = 0; i < 기록목록.Length; i++)
        {
            if (기록목록[i].activeSelf)
            {
                기록목록[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = 기록타입비활성색;
            }

        }
    }

    public void TypeClick(int code)
    {
        DeleteRecordData();

        // 해당 버튼에 하이라이트
        for (int i = 0; i < 기록타입.Length; i++)
        {
            기록타입[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = 기록타입비활성색;
        }

        기록타입[code].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = 기록타입활성색;

        // 기록 목록 생성
        for (int i = 0; i < DataManager.instance.paperDatas[code].기록목록.Length; i++)
        {
            if (DataManager.instance.paperDatas[code].기록목록[i].기록활성여부 == true)
            {
                기록목록[i].SetActive(true);
                기록목록[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    DataManager.instance.paperDatas[code].기록목록[i].기록이름;
                기록목록[i].GetComponent<ForRecord>().기록타입 = code;
                기록목록[i].GetComponent<ForRecord>().기록코드 =
                    DataManager.instance.paperDatas[code].기록목록[i].기록코드;
            }
        }
    }

    public void RecordClick(GameObject target)
    {
        기록내용.text =
            DataManager.instance.paperDatas[target.GetComponent<ForRecord>().기록타입].기록목록[target.GetComponent<ForRecord>().기록코드].내용;

        // 기록 하이라이트
        for (int i = 0; i < 기록목록.Length; i++)
        {
            if (기록목록[i].activeSelf)
            {
                기록목록[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = 기록타입비활성색;
            }

            target.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = 기록타입활성색;
        }
    }
}
