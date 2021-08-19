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
    [Header("0: 메모, 1: 편지, 2: 문서, 3: 신문, 4: 기타")]
    public PaperData[] paperDatas;
    [Header("대화 데이터")]
    public TalkData[] talkDatas;

    [System.Serializable]
    public class TalkData
    {
        public int 대화코드;
        public bool 아이템활성여부;
        [Header("한 대화당 하나의 오브젝트 활성화가 가능하며 대화가 끝날때 활성화")]
        public GameObject 활성오브젝트;
        public TalkData2[] 대화리스트;
    }

    [System.Serializable]
    public class TalkData2
    {
        public string 대화; 
    }

    [System.Serializable]
    public class ItemData
    {
        public string 이름;
        public int 코드;
        public bool 인벤토리여부;
        [TextArea]
        public string 설명;
        public Sprite 이미지;
    }

    [System.Serializable]
    public class PaperData
    {
        public PaperData2[] 기록목록;
    }

    [System.Serializable]
    public class PaperData2
    {
        public int 기록코드;
        public string 기록이름;
        public bool 기록활성여부;
        [TextArea]
        public string 내용;
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

        string jsonData1 = JsonConvert.SerializeObject(paperDatas);
        File.WriteAllText(Application.persistentDataPath + "/paperDatas.json", jsonData1);

        //string jsonData2 = JsonConvert.SerializeObject(talkDatas);
        //File.WriteAllText(Application.persistentDataPath + "/talkDatas.json", jsonData2);

        Debug.Log("데이터 내보내기 완료");
    }

    public void ImportData()
    {
        string data0 = File.ReadAllText(Application.persistentDataPath + "/itemData.json");
        itemDatas = JsonConvert.DeserializeObject<ItemData[]>(data0);

        string data1 = File.ReadAllText(Application.persistentDataPath + "/paperDatas.json");
        paperDatas = JsonConvert.DeserializeObject<PaperData[]>(data0);

        //string data2 = File.ReadAllText(Application.persistentDataPath + "/talkDatas.json");
        //talkDatas = JsonConvert.DeserializeObject<TalkData[]>(data0);

        Debug.Log("데이터 불러오기 완료");
    }

}
