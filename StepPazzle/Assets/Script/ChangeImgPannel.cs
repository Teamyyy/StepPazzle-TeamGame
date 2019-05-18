using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeImgPannel : MonoBehaviour
{
    [SerializeField]
    private Material defaultMaterial = null;
    [SerializeField]
    private Material changeMaterial = null;

    //BoxCollider pannelCollider;

    private bool changedTex = false;
    public bool GetChangeTexFlag() { return changedTex; }

    public void Start()
    {
        //現在のMarterialをdefault状態にする
        transform.GetComponent<Renderer>().material = defaultMaterial;
        //pannelCollider = GetComponent<BoxCollider>();
        //pannelCollider.size = new Vector3(0.5f, 1.0f, 0.5f);
        changedTex = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && changedTex == false)
        {
            StepedMarterialChange();
        }
    }

        //ここでパネルのMarterialが変更される
    private void StepedMarterialChange()
    {
        transform.GetComponent<Renderer>().material = changeMaterial;
        transform.tag = "changedPannel";
        changedTex = true;
    }

    //まだ実装途中の為使用していない関数。
    private void ReturnMarterialChange()
    {
        transform.GetComponent<Renderer>().material = defaultMaterial;
        transform.tag = "defaultPannel";
        changedTex = false;
    }

}
