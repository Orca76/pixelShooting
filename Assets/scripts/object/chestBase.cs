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

    public bool unlocked = true;//空けることができる　ダンジョンのデフォルトはこれ 店のはfalse
    public int cost;//宝箱の値段

    public int componentNumber;//指定のコンポーネントを取得
    public bool randomItem = true;
    public bool mimic = false;
    public GameObject mimicObj;

    public AudioClip seClip; // 再生したいSEをInspectorで設定
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        data = GameObject.Find("GunSystem").GetComponent<DataBase>();
        mimic = Random.Range(0, 100) < 10;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("distance" + Vector3.Distance(gameObject.transform.position, player.transform.position));

        if (!opened)//開いていない
        {
            if (unlocked)
            {
                if (distance > Vector3.Distance(gameObject.transform.position, player.transform.position))//プレイヤーから一定以上近い（宝箱に触れている）
                {
                    GetComponent<AudioSource>().PlayOneShot(seClip);
                    if (mimic)
                    {
                        Instantiate(mimicObj, transform.position, transform.rotation);
                        Destroy(gameObject);
                    }

                    //ボタンの処理追加するかも
                    if (randomItem)
                    {
                        Instantiate(data.Components[Random.Range(0, data.Components.Length)], transform.position, transform.rotation);//コンポーネントをランダムで一つ生成
                    }
                    else
                    {
                        Instantiate(data.Components[componentNumber], transform.position, transform.rotation);//指定のコンポーネントを一つ生成
                    }
                    
                    gameObject.GetComponent<SpriteRenderer>().sprite = OpenedSprite;
                    opened = true;
                }
            }
            else
            {

                if (distance > Vector3.Distance(gameObject.transform.position, player.transform.position))//プレイヤーから一定以上近い（宝箱に触れている）
                {
                    //GetComponent<AudioSource>().PlayOneShot(seClip);
                    if (Input.GetKeyDown(KeyCode.Space))//お支払い
                    {
                        if (player.GetComponent<PlayerBase>().money >= cost)//金が足りるか
                        {
                            //Instantiate(data.Components[Random.Range(0, data.Components.Length)], transform.position, transform.rotation);//コンポーネントをランダムで一つ生成
                           // gameObject.GetComponent<SpriteRenderer>().sprite = OpenedSprite;
                            player.GetComponent<PlayerBase>().money -= cost;//金を失う
                            unlocked = true;
                            //opened = true;
                        }
                   
                    }
                   
                  
                }
            }
           

        }


    }

}
