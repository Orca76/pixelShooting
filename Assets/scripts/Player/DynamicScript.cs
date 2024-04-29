using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DynamicScript : MonoBehaviour
{
    public TextMeshProUGUI dynamic;
     float floorFadeoutSpeed=0.5f;
    // Start is called before the first frame update
    //チュートリアルテキストを時間経過でフェードアウトさせるスクリプト
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dynamic.alpha > 0)//透明でない
        {

            dynamic.alpha -= floorFadeoutSpeed * Time.deltaTime;
           // Debug.Log("alpha--");
            //Destroy(floorText);
        }
    }
}
