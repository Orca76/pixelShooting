using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public float detectRange;
    public GameObject[] enemies;
    public int totalEnemiesNum;//部屋にいる敵の総数
    public int fieldEnemiesNum;//一度に出てこられる敵の上限
    public GameObject[] createPosition;
    public bool enteredRoom;//プレイヤーが部屋に入った
    int createIndex;
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("距離は" + Vector2.Distance(gameObject.transform.position, player.transform.position));

        if (detectRange > Vector2.Distance(gameObject.transform.position, player.transform.position))
        {
            enteredRoom = true;
        }
        

            if (enteredRoom)//プレイヤーが部屋に入った
            {
            int  level = player.GetComponent<PlayerBase>().currentFloor;
            enemies = GameObject.Find("GunSystem").GetComponent<DataBase>().enemies;
            if (totalEnemiesNum >= 0)//部屋にまだ出撃できる敵が残っている
                {
                    if (GameObject.FindGameObjectsWithTag("enemy").Length < fieldEnemiesNum)//部屋にまだ出撃できるスペースがある
                    {
                        Instantiate(enemies[Random.Range(0, Mathf.Min(level*2,enemies.Length))], createPosition[createIndex].transform.position, transform.rotation);

                        if (createIndex == createPosition.Length - 1)
                        {
                            createIndex = 0;
                        }
                        else
                        {
                            createIndex++;
                        }
                        totalEnemiesNum--;

                    }
                }

            }
        

    }
}
