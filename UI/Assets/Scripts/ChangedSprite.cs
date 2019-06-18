using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangedSprite : PannelCommon
{
    
    [SerializeField]
    private Sprite defaultSprite = null;
    SpriteRenderer mySprite = null;

    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.sprite = defaultSprite;
    }


    //受け取ったフラグによって、踏まれた後のSpriteに変化するか踏まれる前のSpriteに変化するか
    public void ChangeSprite(bool changed)
    { 
        if(changed==true)
        {
            mySprite.sprite = defaultSprite;
        }
        else
        {
            mySprite.sprite = changedSprite;
        }
    }

 
}
