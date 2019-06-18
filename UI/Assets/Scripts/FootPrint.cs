using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour
{
    //設置される足跡のObject、数をInspectorで指定できる
    [SerializeField]
    private GameObject[] foot = null;

    //Object全てを管理する
    //ActiveでないObjectが格納されている配列
    private GameObject[] waitingFoot;
    //ActiveのObject。配列から足跡を受け取った時にアクティブにさせここに格納する
    private List<GameObject> activeFoot;
    //配列のサイズ
    private static int footNum = 50;

    //MapControllerからSpriteSizeを取得
    private Vector3 spriteSize;

    //乱数を取得する変数
    private System.Random random;

    public void FirstProccess()
    {

        //配列waitingFootにfootNum個のFootObjectを非Active状態で生成
        waitingFoot = new GameObject[footNum];
        //乱数所得
        random = new System.Random(foot.Length - 1);
        for (int num = 0; num < waitingFoot.Length; num++)
        {
            int range = random.Next(foot.Length);

            waitingFoot[num] = Instantiate(foot[range]);
            waitingFoot[num].transform.parent = transform;
            waitingFoot[num].SetActive(false);
        }

        activeFoot = new List<GameObject>();
    }

    //MapPositioning.Positioning()から呼ばれspriteSizeを受け取り保存する。
    public void SetSpriteSize(Vector3 size)
    {
        spriteSize=size;
        spriteSize.y = 0.2f;
    }

    public void SetFoot(Vector3 pos, Vector3 direction)
    {
        //そのままfootPosに足跡を設置すると進んでいない場所にはみ出してしまうため0.5ずらす
        Vector3 disPlaceVec = direction;
        disPlaceVec /= 2.0f;
        //元の座標にdisPlaceVecを足し0.5ずらさせる
        pos += disPlaceVec;
        //進んでる方向に足跡を回転させ更にy軸に90°回転させる
        direction.y = 90;
        Quaternion qua = Quaternion.LookRotation(direction);


        //Footだけは、配列にあるObjectをnullにする必要があるためObjectPoolを使用しない
        for (int num = 0; num < waitingFoot.Length; num++)
        {
            GameObject footObj = waitingFoot[num];
            if (footObj)
            {
                //アクティブにする
                footObj.SetActive(true);
                //座標、角度をセット
                footObj.transform.position = new Vector3(pos.x*spriteSize.x,0.1f,pos.z*spriteSize.z);
                footObj.transform.rotation = qua;

                //配列からリストへ渡す
                activeFoot.Add(footObj);
                waitingFoot[num] = null;
                break;
            }
        }
    }

    public void DeleteOneFoot()
    {
        //画面から消したいObjectを非Activeにしリストから配列に渡す
        GameObject footObj = activeFoot[activeFoot.Count - 1];
        footObj.SetActive(false);

        for (int num = 0; num < waitingFoot.Length; num++)
        {
            if (!waitingFoot[num])
            {
                waitingFoot[num] = footObj;
                activeFoot.RemoveAt(activeFoot.Count - 1);
                break;
            }
        }
    }

    public void Reset()
    {
        foreach (GameObject obj in activeFoot)
        {
            for (int num = 0; num < waitingFoot.Length; num++)
            {
                if (waitingFoot[num] == null)
                {
                    waitingFoot[num] = obj;
                    waitingFoot[num].SetActive(false);
                    break;
                }
            }
        }
        activeFoot.Clear();
    }
}
