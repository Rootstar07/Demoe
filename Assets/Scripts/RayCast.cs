using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayCast : MonoBehaviour
{
    public float range;
    public TextMeshProUGUI uiText;
       
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
            }
            else if (hit.collider.CompareTag("Object"))
            {
                uiText.text = "조사하기";
            }
            else if (hit.collider.CompareTag("Text"))
            {
                uiText.text = "읽기";
            }
            else if (hit.collider.CompareTag("item"))
            {
                uiText.text = "가져가기";
            }
            else
            {
                uiText.text = "";
            }
        }
        else
        {
            uiText.text = "";
        }
    }
}
