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

    public void TypeClick(int code)
    {
        DeleteRecordData();

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

    public void RecordClick(ForRecord forRecord)
    {
        기록내용.text =
            DataManager.instance.paperDatas[forRecord.기록타입].기록목록[forRecord.기록코드].내용;
    }
}
