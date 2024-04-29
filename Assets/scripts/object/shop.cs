using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class shop : MonoBehaviour
{
    //コンポーネントを生成し、価格を設定する

    float collisionDistance;//ゲームオブジェクトとプレイヤー間の接触判定に使用する基準距離
    GameObject player;
    public int costType;//商品の値段グループ　安いものと高いものが存在　0～50 20~100 50~200
    int cost;//商品の値段
    DataBase data;

    public TextMeshProUGUI costText;//値段を表示するtext
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("GunSystem").GetComponent<DataBase>();
        switch (costType)//商品の値段を決定する
        {
            case 0:
                cost = Random.Range(0, 50);
                break;
            case 1:
                cost = Random.Range(20, 100);
                break;
            case 2:
                cost = Random.Range(50, 200);
                break;
        }
        GameObject item = Instantiate(data.Components[Random.Range(0, data.Components.Length)], transform.position, transform.rotation);//コンポーネントをランダムで一つ生成
        item.GetComponent<BulletComponent>().itemPrice = cost;
        costText.text = "$" + cost;

    }

    // Update is called once per frame
    void Update()
    {


    }

}
