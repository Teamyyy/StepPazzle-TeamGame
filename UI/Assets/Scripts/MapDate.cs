using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDate : MonoBehaviour
{

    public enum eGroundName
    {
        eDefaultPannel = 0,
        eChangedPannel = 1,
        eWall = 2,
        eWhite = 3,
        ePlayerPosition = 4
    }

    public enum eStageName
    {
        eFirstStage,
        eSecondStage,
    }


    int[,] mapDateFirst =
        {
    {2,0,0,0,2},
    {0,0,0,0,2},
    {0,2,0,0,2},
    {0,0,0,0,0},
    {0,0,0,0,0},

    {2,0,0,0,0},
    {4,0,0,0,2}

    };

    int[,] mapDateSecond =
        {
        {2,0,0,0,0 },
        {2,0,2,2,0 },
        {2,0,4,2,0 },
        {2,2,2,2,0 },
        {0,0,0,0,0 },
    };


    GameObject[,] mapObjectDate =
    {
        {null, null,null,null,null, null,null,null,null,null},
        {null, null,null,null,null, null,null,null,null,null},
        {null, null,null,null,null, null,null,null,null,null},
        {null, null,null,null,null, null,null,null,null,null},
        {null, null,null,null,null, null,null,null,null,null},

        {null, null,null,null,null, null,null,null,null,null},
        {null, null,null,null,null, null,null,null,null,null},
        {null, null,null,null,null, null,null,null,null,null},
        {null, null,null,null,null, null,null,null,null,null},
        {null, null,null,null,null, null,null,null,null,null}
    };

    //二次元配列を返す
    public int[,] GetMapDate(eStageName stage,ref Vector3 pos,ref string fileName)
    {
        switch (stage)
        {
            case (eStageName.eFirstStage):
                pos=new Vector3(0.68f, 0, 0.69f);
                fileName = "FirstImage";
                return mapDateFirst;
                ;
            case (eStageName.eSecondStage):
                pos = new Vector3(0.8f, 0, 0.6f);
                fileName = "flower";
                return mapDateSecond;
                ;

        }

        return mapDateFirst;
    }

    public GameObject[,] GetNullObjectDate()
    {
        return mapObjectDate;
    }

}

