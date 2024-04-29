using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    // Start is called before the first frame update
    //minimapの処理　メニュー開いた時には非表示
    public GameObject gunsSystem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gunsSystem.GetComponent<GunManager>().EditOn)//エディット中
        {

        }
    }
}
