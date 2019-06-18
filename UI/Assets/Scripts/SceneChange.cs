using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    [SerializeField]
    private GameObject StartUp, StartUp1, StartUp2, StartUp3, smallButton, smallButton2, StartPanel, StartPanel2;

    //Startボタンが押されたら起動アニメーション、2秒後に関数呼び出し
    public void MainScene()
    {
        Invoke("StartUP", 2);
        Invoke("SceneChangeMain", 4);
        smallButton.SetActive(true);
    }

    public void SceneChangeMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void StartUP()
    {
        StartUp.SetActive(true);
    }

    //Picture1ボタンが押されたら起動アニメーション、2秒後に関数呼び出し
    public void Picture1()
    {
        Invoke("StartUP1", 2);
        Invoke("SceneChanePicture1", 4);
        smallButton2.SetActive(true);
    }

    public void StartUP1()
    {
        StartUp1.SetActive(true);
    }

    public void SceneChanePicture1()
    {
        SceneManager.LoadScene("Picture 1");
    }

    //Picture2ボタンが押されたら起動アニメーション、2秒後に関数呼び出し
    public void Picture2()
    {
        Invoke("StartUP1", 2);
        Invoke("SceneChanePicture2", 4);
        smallButton2.SetActive(true);
    }

    public void SceneChanePicture2()
    {
        SceneManager.LoadScene("Picture 2");
    }

    //Picture3ボタンが押されたら起動アニメーション、2秒後に関数呼び出し
    public void Picture3()
    {
        Invoke("StartUP1", 2);
        Invoke("SceneChanePicture3", 4);
        smallButton2.SetActive(true);
    }

    public void SceneChanePicture3()
    {
        SceneManager.LoadScene("Picture 3");
    }

    //Selectボタンが押されたらMainにシーン遷移
    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }

    //Titleボタンが押されたらTitleシーン遷移
    public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void GameStart()
    {
        StartPanel = GameObject.Find("GameStart");
        Destroy(StartPanel);
        StartPanel2.SetActive(true);
    }

    public void GameStart2()
    {
        StartPanel2 = GameObject.Find("GameStart2");
        Destroy(StartPanel2);
    }
}
