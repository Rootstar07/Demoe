using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForSlot : MonoBehaviour
{
    public int 가진아이템코드;
    public Image 배경;
    public Image 아이템이미지;
    public TextMeshProUGUI 아이템이름;

    public enum Item
    {
        없음,
        사무실열쇠,
        CD
    }
}
