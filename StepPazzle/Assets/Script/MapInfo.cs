using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    //ステージ情報を格納したシングルトン。MapCreaterとPlayerから呼ばれる。
    private int[] stage =
        {
        0,0,0,0,0 ,5,
        0,0,1,1,0 ,5,
        0,0,0,0,0 ,5,
        0,0,1,0,0 ,5,
        0,0,0,0,2 
    };

    List<int> stageList = new List<int>();
    int playerPosX = 0;
    int playerPosZ = 0;
    ////ステージの幅。ステージ情報で改行が行われるとカウントが終了される
    int stageWidth = 0;
    //ステージの高さ。ステージ情報で改行が行われるたびにカウントが増える
    int stageHeight = 0;

    private void Awake()
    {
        bool stageWidthCountEnd = false;
        for (int x = 0; x < stage.Length; x++)
        {
            stageList.Add(stage[x]);
            if (stageWidthCountEnd == false)
            {
                stageWidth++;
            }
            if (stage[x] == 5)
            {
                stageWidthCountEnd = true;
                stageHeight++;
            }
        }
        //PlayerがMap情報上でどこにいるかを探す。
        foreach (int x in stageList)
        {
            if (x == 0 || x == 1)
            {
                playerPosX++;
            }
            else if (x == 5)
            {
                playerPosX = 0;
                playerPosZ++;
            }
            else if (x == 2)
            {
                break;
            }
        }

    }

    private static MapInfo mInstance;
    public static MapInfo Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject obj = new GameObject("MapInfo");
                mInstance = obj.AddComponent<MapInfo>();
            }
            return mInstance;
        }
    }

    public List<int> GetStageList()
    {
        return stageList;
    }

    public int GetplayerPositionX()
    {
        return playerPosX;
    }

    public int GetplayerPositionZ()
    {
        return stageHeight - playerPosZ;
    }

    public int GetstageWidth()
    {
        return stageWidth;
    }

    public int GetStageHeight()
    {
        return stageHeight;
    }

    //public int GetStageInfo(int x, int z)
    //{
    //    return stage[x, z];
    //}
    //public int GetStageSizeWidth()
    //{
    //    return stage.GetLength(1);
    //}

    //public int GetStageSizeHeight()
    //{
    //    return stage.GetLength(0);
    //}
}
