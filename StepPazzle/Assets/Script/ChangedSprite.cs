using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangedSprite : MonoBehaviour {

    [SerializeField]
    private Sprite defaultSprite=null;
    [SerializeField]
    private Sprite changedSprite = null;

    void Start()
    {
        transform.GetComponent<SpriteRenderer>().sprite = defaultSprite;

        transform.tag = "defaultPannel";
    }

    public void SetSprite(Sprite sprite)
    {
        changedSprite = sprite;
    }


    public void ChangeSprite(bool changed)
    { 
        if(changed==true)
        {
            transform.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            transform.tag = "defaultPannel";
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().sprite = changedSprite;
            transform.tag = "changedPannel";
        }
    }
}
