using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    //受け取った配列arrayに非Active状態のobjを生成する
    public void CreatePool(GameObject obj,GameObject[] array)
    {
        for (int num = 0; num < array.Length; num++)
        { 
            array[num] = Instantiate(obj);
            array[num].transform.parent = transform;
            array[num].SetActive(false);
        }
    }

    public GameObject GetWaitingObject(GameObject[] array,Vector3 pos,Quaternion qua)
    {
        foreach(GameObject obj in array)
        {
            if(obj.activeSelf==false)
            {
                obj.SetActive(true);
                obj.transform.position = pos;
                obj.transform.rotation = qua;
                return obj;
            }
        }
        return null;
    }

    public void ResetWaitingObject(GameObject[] array)
    {
        foreach(GameObject obj in array)
        {
            if(obj.activeSelf==true)
            {
                obj.SetActive(false);
            }
        }
    }

}
