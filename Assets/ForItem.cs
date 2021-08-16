using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForItem : MonoBehaviour
{
    [Header("DataManager의 열거형과 데이터 확인")]
    public int 아이템코드;

    public enum Item
    {
        없음,
        사무실열쇠,
        CD
    }
}
