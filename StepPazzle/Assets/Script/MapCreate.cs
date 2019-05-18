using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject pannel = null;
    [SerializeField]
    private GameObject wall = null;
    [SerializeField]
    private GameObject ground = null;
    //パネルすべてを管理するList
    private List<GameObject> pannelList = new List<GameObject>();
    //ステージ情報を持ったMapInfo
    MapInfo mapInfo;
    //マップ情報をMapInfoから受け取り格納するList
    private List<int> mapList = new List<int>();

    //クリアしたかどうかのフラグ。シーン遷移が実装されたら削除する予定
    private bool clearFlag = false;

    void Start()
    {
        clearFlag = false;

        mapInfo = MapInfo.Instance;
        mapList = mapInfo.GetStageList();

        int mapWidth = 0;
        int mapHeight = 0;

        int mapHeightMaxSize = mapInfo.GetStageHeight();

        //ステージの生成、子要素に追加してpannelListに加えていく
        foreach (int num in mapList)
        {

            GameObject mapObject = null;
            if (num == 0)
            {
                mapWidth++;
                mapObject = pannel;
            }
            else if (num == 1)
            {
                mapWidth++;
                mapObject = wall;
            }
            else if (num == 2)
            {
                mapWidth++;
                mapObject = ground;
            }
            else
            {
                mapWidth = 0;
                mapHeight++;
                continue;
            }

            Vector3 objectPos = new Vector3(mapWidth - 1, 0, mapHeightMaxSize - mapHeight);
            Quaternion objectQua = new Quaternion();

            mapObject = Instantiate(mapObject, objectPos, objectQua);

            mapObject.transform.parent = transform;
            pannelList.Add(mapObject);
        }
        int cnt = 0;
        foreach (GameObject child in pannelList)
        {
            cnt++;
        }
        Debug.Log("List cnt " + cnt);
    }

    void Update()
    {
        //クリアしていない状態なら　全てのpannelが踏まれてないかをチェックする
        if (clearFlag == false)
        {
            bool allPannelChangedFlag = true;
            foreach (GameObject child in pannelList)
            {
                if (child.tag == "defaultPannel")
                {
                    allPannelChangedFlag = false;
                }
            }
            if (allPannelChangedFlag == true)
            {
                clearFlag = true;
                Debug.Log("Clear");
            }
        }
    }
}
