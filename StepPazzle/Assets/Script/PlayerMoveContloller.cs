using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerMoveContloller : MonoBehaviour
{
    // 移動速度
    [SerializeField]
    private float speed = 5;
    // 入力受付時、移動先の位置を算出して保存 
    private Vector3 targetPos;
    //移動方向ベクトル 
    private Vector3 moveVectorOnScene;
    //移動方向ベクトルをSpriteサイズで割った値。xyz全てに1と0しか入らない
    private Vector3 moveVectorOnMap;

    //MapDateがコンポーネントされているGameObject
    [SerializeField]
    private GameObject haveMapDateObject = null;
    //マップデータ上のPlayerの位置
    private Vector3 playerPosOnMapDate;

    private bool up;
    private bool down;
    private bool right;
    private bool left;


    private float moveSpriteSizeX;
    private float moveSpriteSizeZ;

    void Start()
    {
        moveSpriteSizeX = haveMapDateObject.GetComponent<MapPositioning>().spriteSizeX;
        moveSpriteSizeZ = haveMapDateObject.GetComponent<MapPositioning>().spriteSizeZ;
        transform.localScale = new Vector3(moveSpriteSizeX, 1, moveSpriteSizeZ);


        //プレイヤーの初期座標を受け取り入れる
        playerPosOnMapDate = haveMapDateObject.GetComponent<MapController>().GetFirstPositionPlayer();
        transform.position = new Vector3(playerPosOnMapDate.x * moveSpriteSizeX, 1, playerPosOnMapDate.z * moveSpriteSizeZ);



        //目的座標をリセット
        targetPos = transform.position;
    }

    void Update()
    {
        bool moving = true;
        //移動中かどうかの判定。移動中でなければ入力を受付
        if (transform.position == targetPos)
        {
            moving = false;
            SetTargetPosition();
            //移動先に壁がない場合targetPosを更新させる
            if (TargetPositionHaveWall() == false)
            {
                targetPos = transform.position + moveVectorOnScene;
            }
        }


        if (transform.position != targetPos)
        {
            if (moving == false)
            {
                moveVectorOnMap = new Vector3(moveVectorOnScene.x / moveSpriteSizeX, 0, moveVectorOnScene.z / moveSpriteSizeZ);

                haveMapDateObject.GetComponent<MapController>().MovedPlayer(playerPosOnMapDate, moveVectorOnMap);
                playerPosOnMapDate += moveVectorOnMap;
            }
            Move();
        }
        SetInputKey("reset");
    }


    //入力に応じて移動方向を代入
    private void SetTargetPosition()
    {
        moveVectorOnScene = new Vector3(0, 0, 0);
        moveVectorOnMap = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow) || right == true)
        {
            moveVectorOnScene.x = +moveSpriteSizeX;
            moveVectorOnMap.x += 1;
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || left == true)
        {
            moveVectorOnScene.x = -moveSpriteSizeX;
            moveVectorOnMap.x -= 1;
            return;
        }
        if (Input.GetKey(KeyCode.UpArrow) || up == true)
        {
            moveVectorOnScene.z = +moveSpriteSizeZ;
            moveVectorOnMap.z += 1;
            return;
        }
        if (Input.GetKey(KeyCode.DownArrow) || down == true)
        {
            moveVectorOnScene.z = -moveSpriteSizeZ;
            moveVectorOnMap.z -= 1;
            return;
        }
        return;
    }

    //移動先が移動可能か
    bool TargetPositionHaveWall()
    {
        bool targetPositionNoWallFlag = true;

        //int targetPosZ = (int)(playerPosOnMapDate.z + (moveVectorOnScene.z / moveSpriteSizeZ));
        //int targetPosX = (int)(playerPosOnMapDate.x + (moveVectorOnScene.x / moveSpriteSizeX));

        //壁だった場合移動できない
        if (haveMapDateObject.GetComponent<MapController>().canMove(playerPosOnMapDate + moveVectorOnMap) == true)
        {
            targetPositionNoWallFlag = false;
        }

        return targetPositionNoWallFlag;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void SetInputKey(string key)
    {
        switch (key)
        {
            case ("up"):
                up = true;
                break;
            case ("down"):
                down = true;
                break;
            case ("right"):
                right = true;
                break;
            case ("left"):
                left = true;
                break;
            default:
                up = false; down = false; right = false; left = false;
                break;
        }

    }
}

