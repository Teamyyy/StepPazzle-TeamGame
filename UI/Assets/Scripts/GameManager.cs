using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    MapDate.eStageName stageName = 0;

    [SerializeField]
    PlayerMoveContloller player = null;
    [SerializeField]
    MapController stageCreater = null;
    [SerializeField]
    FootPrint footPrinter = null;
    [SerializeField]
    Image blackScreen = null;

    [SerializeField]
    GameObject clearCanvas = null;

    enum gameState
    {
        Ready,
        Start,
        Game,
        Clear,
        Result
    }

    gameState state;

    private void Start()
    {
        //state = gameState.Game;
        state = gameState.Ready;
        //player.Reset();
        //blackScreen.color = new Color(0, 0, 0, 0.0f);
        blackScreen.color = new Color(0, 0, 0, 0.8f);

        stageCreater = Instantiate(stageCreater);
        stageCreater.transform.parent = transform;

        player = Instantiate(player);
        player.transform.parent = transform;

        footPrinter = Instantiate(footPrinter);
        footPrinter.transform.parent = transform;
    }

    //// Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case (gameState.Ready):
                StateChange();
                    break;
            case (gameState.Start):
                //if (Input.GetKey(KeyCode.Return))
                {
                    StateChange();
                }
                break;
            case (gameState.Game):
                //stageControllerから、そのステージをクリアしたかどうかのフラグを受け取る[クリア=true]
                bool updateStop = false;
                updateStop = stageCreater.ClearCheck();
                //プレイヤーの更新が終了する条件。「ゲームクリア」かつ「移動の完了」
                bool playerUpdateEnd = (updateStop && player.CheckMovingEnd());
                //playerUpdateEnd = false;
                if (playerUpdateEnd == false)
                {
                    player.UpdateInGame();
                }
                else
                {
                    clearCanvas.SetActive(true);
                }
                break;
            case (gameState.Clear):

                break;
            case (gameState.Result):

                break;
        }
    }

    //stateを変える関数
    private void StateChange()
    {
        switch (state)
        {
            case (gameState.Ready):
                state = gameState.Start;

                footPrinter.FirstProccess();
                //ステージを配置させる
                stageCreater.footPrinter = footPrinter;
                stageCreater.InitProcces();
                stageCreater.MapReset(stageName);

                //playerを初期化
                player.haveMapDateObject = stageCreater;
                player.Reset();

                break;
            //StartからGameへ
            case (gameState.Start):
                state = gameState.Game;
                //暗転を消す
                blackScreen.color = new Color(0, 0, 0, 0);


                break;
            //GameからClearへ
            case (gameState.Game):
                state = gameState.Clear;

                stageCreater.ResetMap();
                
                break;
            case (gameState.Clear):
                state = gameState.Result;
                break;
            case (gameState.Result):
                state = gameState.Ready;
                break;
        }
    }
}

