using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] Gates = new GameObject[4];//��E����
    public bool[] GateOn = new bool[4];//true�œ����蔻�薳���@trigger�Ŕ��ʂ���s����
    BoxCollider2D[] colliders = new BoxCollider2D[4];

    public GameObject[] RoomObjects;// �󔒁@�󔠁@�L�����@�m���͂��ꂼ��@60 30 10

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
        //    Instantiate(RoomObjects[0], transform.position, transform.rotation);//�󔒕���
        //}else if (x > 60 && x <= 90)
        //{
        //    Instantiate(RoomObjects[1], transform.position, transform.rotation);//�󔠕���
        //}
        //else
        //{
        //    Instantiate(RoomObjects[2], transform.position, transform.rotation);//�L��������
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
