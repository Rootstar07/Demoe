using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinManager : MonoBehaviour
{
    public GameObject PinCanvas;
    public mouselook _mouselook;
    public UIManager uIManager;
    public RayCast rayCast;
    public int 입력한핀;
    public int 정답핀;
    public TextMeshProUGUI 핀미리보기;
    public int 현재자릿수;
    GameObject 타켓핀;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && PinCanvas.activeSelf)
        {
            PinOut();
        }
    }

    public void Pin(GameObject targetPin)
    {
        정답핀 = targetPin.GetComponent<ForPin>().password;
        타켓핀 = targetPin;

        PinCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        _mouselook.canMouseMove = false;

        DeletePin();

        // 레이캐스트 입력 안되게
        rayCast.rayActived = false;

        // QE 입력 안되게
        uIManager.canUI = false;
    }

    public void PinOut()
    {
        PinCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        _mouselook.canMouseMove = true;

        rayCast.rayActived = true;

        uIManager.canUI = true;
    }

    public void DeletePin()
    {
        // 자릿수 초기화
        현재자릿수 = 1;

        // 핀 초기화
        입력한핀 = 0;
        핀미리보기.text = "";
    }

    public void CompletePin()
    {
        if (입력한핀 == 정답핀)
        {
            핀미리보기.text = "환영합니다";

            타켓핀.GetComponent<ForPin>().Unlock();
        }
        else
        {
            // 자릿수 초기화
            현재자릿수 = 1;

            // 핀 초기화
            입력한핀 = 0;

            핀미리보기.text = "에러";
        }
    }

    public void InputPin(int num)
    {
        if (현재자릿수 == 1)
        {
            입력한핀 = num;
            현재자릿수 = 2;
        }
        else if (현재자릿수 == 2)
        {
            입력한핀 = 입력한핀 * 10 + num;
            현재자릿수 = 3;
        }
        else if (현재자릿수 == 3)
        {
            입력한핀 = 입력한핀 * 10 + num;
            현재자릿수 = 4;
        }
        else if (현재자릿수 == 4)
        {
            입력한핀 = 입력한핀 * 10 + num;
            현재자릿수 = 5;
        }
        else if (현재자릿수 == 5)
        {
            입력한핀 = 입력한핀 * 10 + num;
            현재자릿수 = 6;
        }
        else if (현재자릿수 == 6)
        {
            입력한핀 = 입력한핀 * 10 + num;
            현재자릿수 = 7;
        }
        else if (현재자릿수 == 7)
        {

        }
        핀미리보기.text = 입력한핀.ToString();
    }
}
