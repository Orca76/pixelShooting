using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] bridgeGate;//橋のゲート　敵と戦っている間は閉まる
    void Start()
    {
        foreach (GameObject obj in bridgeGate)
        {
            obj.SetActive(false);//戦闘中ではないときは非アクティブに設定する
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("enemy").Length >= 1)
        {
            foreach (GameObject obj in bridgeGate)
            {
                obj.SetActive(true);//敵との戦闘中は部屋から出られないようにする
            }
        }
        else
        {
            foreach (GameObject obj in bridgeGate)
            {
                obj.SetActive(false);//戦闘中ではないときは非アクティブに設定する
            }

        }
    }
}
