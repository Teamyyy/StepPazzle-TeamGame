using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCommon : MonoBehaviour {
    
    private Vector3 groundPositionOnMap;
    
    public void SetGroundPositionOnMap(Vector3 pos)
    {
        groundPositionOnMap = pos;
    }

    public Vector3 GetGroundPositionOnMap()
    {
        return groundPositionOnMap;
    }

}
