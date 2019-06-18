using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToScore : MonoBehaviour {

    public GameObject Score;

    private void Start()
    {
        Invoke("ScorePanel", 2);
    }

    void ScorePanel()
    {
        Score.SetActive(true);
    }
}
