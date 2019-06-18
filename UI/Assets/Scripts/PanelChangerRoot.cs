using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelChangerRoot : MonoBehaviour
{

    public string Top;
    public UnityEvent onOpen;
    public UnityEvent onClose;

    //-- パネルを開く
    public void PanelOpen()
    {
        this.gameObject.SetActive(true);
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        if (onOpen != null) { onOpen.Invoke(); }
        this.gameObject.transform.Find(Top).GetComponent<PanelChanger>().PanelAppear();
    }

    //-- パネルを閉じる
    public void PanelClose()
    {
        if (onClose != null) { onClose.Invoke(); }
        this.gameObject.SetActive(false);
    }
}
