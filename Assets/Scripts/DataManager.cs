using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;

public class DataManager : MonoBehaviour
{
    [Header("아이템 데이터")]
    public ItemData[] itemDatas;

    [System.Serializable]
    public class ItemData
    {
        public string 이름;
        public int 코드;
        public bool 인벤토리여부;
        [TextArea]
        public string 설명;
        public Image 이미지;
    }

    public enum Item
    {
        없음,
        사무실열쇠,
        CD
    }

    public static DataManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ExportData()
    {
        string jsonData0 = JsonConvert.SerializeObject(itemDatas);
        File.WriteAllText(Application.persistentDataPath + "/itemData.json", jsonData0);

        Debug.Log("데이터 내보내기 완료");
    }

    public void ImportData()
    {
        string data0 = File.ReadAllText(Application.persistentDataPath + "/itemData.json");
        itemDatas = JsonConvert.DeserializeObject<ItemData[]>(data0);

        Debug.Log("데이터 불러오기 완료");
    }

}
