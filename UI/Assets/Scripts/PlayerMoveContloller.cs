﻿using System.Collections;
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
    public MapController haveMapDateObject = null;
    //マップデータ上のPlayerの位置
    private Vector3 playerPosOnMapDate;

    private bool up;
    private bool down;
    private bool right;
    private bool left;
    //移動に必要な上下のサイズ。GameManagerから値を受け取る。
    private Vector3 moveSpriteSize;

    //初期化に必要な処理。GameManagerから呼ばれる。
     public void Reset()
    {
        //MapPositioningから上下の移動距離であるspriteサイズを受け取る
        moveSpriteSize = haveMapDateObject.GetComponent<MapController>().GetSpriteSize();
        //PlayerのX,ZサイズをPannelと同じサイズに変更する
        moveSpriteSize.y = 1.0f;
        transform.localScale = moveSpriteSize;
        moveSpriteSize.y = 0.0f;

        //プレイヤーの初期座標をMapControllerから受け取る
        playerPosOnMapDate = haveMapDateObject.GetComponent<MapController>().GetFirstPositionPlayer();

        transform.position = new Vector3(playerPosOnMapDate.x * moveSpriteSize.x, 1, playerPosOnMapDate.z * moveSpriteSize.z);

        //目的座標をリセット
        targetPos = transform.position;

    }

    //ゲーム中毎F更新され続ける処理
    public void UpdateInGame()
    {
        bool moving = true;
        //移動中かどうかの判定。移動中でなければ入力を受付
        if (CheckMovingEnd()==true)
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
                moveVectorOnMap = new Vector3(moveVectorOnScene.x / moveSpriteSize.x, 0, moveVectorOnScene.z / moveSpriteSize.z);

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
            moveVectorOnScene.x = +moveSpriteSize.x;
            moveVectorOnMap.x += 1;
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || left == true)
        {
            moveVectorOnScene.x = -moveSpriteSize.x;
            moveVectorOnMap.x -= 1;
            return;
        }
        if (Input.GetKey(KeyCode.UpArrow) || up == true)
        {
            moveVectorOnScene.z = +moveSpriteSize.z;
            moveVectorOnMap.z += 1;
            return;
        }
        if (Input.GetKey(KeyCode.DownArrow) || down == true)
        {
            moveVectorOnScene.z = -moveSpriteSize.z;
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

    public bool CheckMovingEnd()
    {
        return transform.position == targetPos; 
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

