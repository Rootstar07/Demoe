using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForPaper : MonoBehaviour
{
    public enum Type
    {
        메모,
        편지,
        보고서,
        신문,
        기타
    }

    public Type 기록타입;
    public int 기록코드;
}
