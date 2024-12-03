using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] Gates = new GameObject[4];//上右下左
    public bool[] GateOn = new bool[4];//trueで当たり判定無し　triggerで判別する都合上
    BoxCollider2D[] colliders = new BoxCollider2D[4];

    public GameObject[] RoomObjects;// 空白　宝箱　キャラ　確率はそれぞれ　60 30 10

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            colliders[i] = Gates[i].GetComponent<BoxCollider2D>();
        }
        Instantiate(RoomObjects[Random.Range(0,RoomObjects.Length)], transform.position, transform.rotation);
        //int x = Random.Range(0, 100);
        //if (x <=60)
        //{
        //    Instantiate(RoomObjects[0], transform.position, transform.rotation);//空白部屋
        //}else if (x > 60 && x <= 90)
        //{
        //    Instantiate(RoomObjects[1], transform.position, transform.rotation);//宝箱部屋
        //}
        //else
        //{
        //    Instantiate(RoomObjects[2], transform.position, transform.rotation);//キャラ部屋
        //}

    }

    // Update is called once per frame
    void Update()
    {
        for (int j = 0; j < 4; j++)
        {
            if (GateOn[j] == true)
            {
                colliders[j].isTrigger = true;
              
            }
            else
            {
                colliders[j].isTrigger = false;
                Gates[j].gameObject.tag = "Block";
            }
        }
    }
}
