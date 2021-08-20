using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkManager : MonoBehaviour
{
    public RayCast rayCast;
    public mouselook _mouselook;
    public GameObject 대화UI;
    public TextMeshProUGUI 대화text;
    int 대화인덱스;
    int 대화코드;
    
    bool talkMode = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && talkMode)
        {
            // 마지막 대화인지 확인
            if (DataManager.instance.talkDatas[대화코드].대화리스트.Length - 1 == 대화인덱스)
            {
                // 오브젝트 있으면 활성화
                if (DataManager.instance.talkDatas[대화코드].활성오브젝트 != null && !DataManager.instance.talkDatas[대화코드].아이템활성여부)
                {
                    DataManager.instance.talkDatas[대화코드].활성오브젝트.SetActive(true);
                }

                // 대화 종료
                Invoke("TalkEnd", 0.1f);
            }
            else
            {
                대화인덱스++;
                대화text.text = DataManager.instance.talkDatas[대화코드].대화리스트[대화인덱스].대화;

            }
        }
    }

    public void TalkStart(GameObject 대화오브젝트)
    {
        대화코드 = 대화오브젝트.GetComponent<ForTalk>().대화코드;
        대화인덱스 = 0;

        rayCast.rayActived = false;
        _mouselook.canMouseMove = false;
        대화UI.SetActive(true);
        talkMode = true;

        // 초기 대화
        대화text.text = DataManager.instance.talkDatas[대화코드].대화리스트[대화인덱스].대화;

        // 마우스 느리게
        _mouselook.FocusTalk();
    }

    public void TalkEnd()
    {
        DataManager.instance.talkDatas[대화코드].아이템활성여부 = true;
        talkMode = false;

        rayCast.rayActived = true;
        _mouselook.canMouseMove = true;
        대화UI.SetActive(false);

        // 마우스 리셋
        _mouselook.FocuseStop();
    }
}
