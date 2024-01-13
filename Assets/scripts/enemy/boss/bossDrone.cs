using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDrone : MonoBehaviour
{
    enemyBase sc;
    GameObject player;
    public GameObject droneLastShot;//破壊時に放たれる弾丸
    public bool laserOn;//レーザーを打つかどうか
    public GameObject[] bullet;
    public float[] span;//攻撃間隔
    float t;
    public GameObject turret;//発射口

    public int number;//ドローン攻撃パターン

    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<enemyBase>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (sc.hp <= 0)
        {
            Debug.Log("chargeShot");
            player = GameObject.FindWithTag("Player");
            Vector2 shotVector = (gameObject.transform.position - player.transform.position).normalized;
            float deg = Mathf.Atan2(shotVector.x, shotVector.y) * Mathf.Rad2Deg;

           
            Instantiate(droneLastShot, gameObject.transform.position, Quaternion.Euler(0, 0, -deg));
            Destroy(gameObject);
        }
        t += Time.deltaTime;
        switch (number)
        {
            case 0://高速連射
                if (t > span[0])
                {
                    Instantiate(bullet[0], transform.position, turret.transform.rotation);
                    t = 0;
                }
                break;
            case 1://時間差ロックオン
                if (t > span[1])
                {
                    Instantiate(bullet[1], transform.position, turret.transform.rotation);
                    t = 0;
                }
                break;
            case 2://左から回転撃ち
                if (t > span[2])
                {
                    Instantiate(bullet[2], transform.position, turret.transform.rotation);
                    t = 0;
                }
                break;

        }
        if (laserOn)//ただ単に弾撃ち続けるフェーズ
        {
            
           
        }
        else
        {

        }
    }
}
