using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestBase : MonoBehaviour
{
    // Start is called before the first frame update
    public float distance;//デバッグ用　プレイヤーとの距離
    GameObject player;
    DataBase data;
   public bool opened = false;

    public Sprite OpenedSprite;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        data = GameObject.Find("GunSystem").GetComponent<DataBase>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("distance" + Vector3.Distance(gameObject.transform.position, player.transform.position));
        if (!opened)//開いていない
        {
            if (distance > Vector3.Distance(gameObject.transform.position, player.transform.position))//プレイヤーから一定以上近い（宝箱に触れている）
            {
                //ボタンの処理追加するかも
                Instantiate(data.Components[Random.Range(0, data.Components.Length)], transform.position, transform.rotation);//コンポーネントをランダムで一つ生成
                gameObject.GetComponent<SpriteRenderer>().sprite = OpenedSprite;
                opened = true;
            }
        }
        
    }

}
