using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPositioning : MonoBehaviour
{
    [SerializeField]
    private GameObject wallObject = null;
    [SerializeField]
    private GameObject pannelObject = null;
    [SerializeField]
    private GameObject whiteObject = null;

    //[SerializeField]
    //private Sprite spriteObject = null;

    [HideInInspector]
    public float spriteSizeX = 6.9f;
    [HideInInspector]
    public float spriteSizeZ = 7.0f;


    public void Positioning()
    {
        spriteSizeX = 0.69f; spriteSizeZ = 0.70f;

        Sprite[] mapSprite = Resources.LoadAll<Sprite>("Img/image0");
        int spriteCnt = 0;

        //MapControllerからマップ情報をint型で取得
        int[,] mapDate = { };
        mapDate = GetComponent<MapController>().GetMapDate();
        //MapControllerからマップ情報をGameObject型で取得 初期はすべてnull(int型マップ情報の配列と同じサイズの配列)
        GameObject[,] mapObjectDate = { };
        mapObjectDate = GetComponent<MapController>().GetMapObjectDate();

        whiteObject.transform.localScale = new Vector3(spriteSizeX, 1, spriteSizeZ);
        wallObject.transform.localScale = new Vector3(spriteSizeX, 1, spriteSizeZ);


        //MapControllerから配列の最大値やステージ情報を取得し配置する
        for (int z = mapDate.GetLength(0)-1; z >= 0; z--)
        {
            for (int x = 0; x < mapDate.GetLength(1); x++)
            {
                //パネル情報。MapDateのeGroundNameで命名済み
                int pannelInfo = mapDate[z, x];
                //設置していくオブジェクトの座標や向き
                Vector3 objectPos = new Vector3(x * spriteSizeX, 0, z * spriteSizeZ);
                Quaternion objectQua = new Quaternion(0, 0, 0, 0);
                //マップ情報によって書き換えられるGameObjectを初期化。初期値はイベントの無い白いブロック
                GameObject setObject = whiteObject;

                //壁
                if (pannelInfo == (int)MapDate.eGroundName.eWall)
                {
                    setObject = wallObject;
                }
                //変化前のパネル
                if (pannelInfo == (int)MapDate.eGroundName.eDefaultPannel)
                {
                    //90度回転させ上を向かせるようにする
                    objectQua = Quaternion.Euler(90, 0, 0);
                    setObject = pannelObject;
                    setObject.GetComponent<ChangedSprite>().SetSprite(mapSprite[spriteCnt]);
                    if (mapSprite.Length != spriteCnt)
                    {
                        spriteCnt++;

                    }
                }
                //プレイヤーの初期座標またはイベントの無い白いブロック
                if (pannelInfo == (int)MapDate.eGroundName.ePlayerPosition ||
                    pannelInfo == (int)MapDate.eGroundName.eWhite)
                {
                    setObject = whiteObject;
                }
                //オブジェクトを生成
                setObject = Instantiate(setObject, objectPos, objectQua);
                //すべてnullで登録されていたGameObject型のマップ情報の全てにsetObjectを登録していく
                mapObjectDate[z, x] = setObject;
                //このScriptがタッチされているオブジェクトの子にする
                setObject.transform.parent = transform;

                //if (pannelInfo == (int)MapDate.eGroundName.eDefaultPannel)
                //{
                //    Vector3 spritePos = objectPos + new Vector3(0,3,0);
                //    Instantiate(oneSprite, spritePos, new Quaternion(0,0,90,0));
                //    //setObject.GetComponent<ChangeMaterialPannel>().changeMaterial=;
                //}
            }
        }
    }
}
