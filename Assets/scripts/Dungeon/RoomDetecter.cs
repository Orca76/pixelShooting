using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetecter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject icon;
    //部屋にプレイヤーが入ったことを感知　マップに書き足す
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            icon.SetActive(true);
        }
    }
}
