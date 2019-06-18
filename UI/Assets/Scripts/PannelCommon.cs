using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelCommon : MonoBehaviour
{

    [SerializeField]
    protected Sprite changedSprite = null;

    private void Start()
    {
        transform.GetComponent<SpriteRenderer>().sprite = changedSprite;
    }

    public void SetSprite(Sprite sprite)
    {
        changedSprite = sprite;
    }
}
