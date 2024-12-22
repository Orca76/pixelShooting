using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    // public float speed;
    public float Damage;
    public bool penetrate;//貫通するか
    public float speed;
    public float lifetime;
    Rigidbody2D rig;

    public float delay;

    public GameObject damageText;
    GameObject damageCanvas;
    public bool normalShot = true;//親オブジェクトが存在する場合は関係を切る　通常の弾丸としての挙動を

    float t;
    public bool specialBullet = false;//特殊弾丸　敵の攻撃を利用する場合→敵にダメージが通る
    // Start is called before the first frame update
    void Start()
    {
        damageCanvas = GameObject.Find("damageCanvas");
        Destroy(gameObject, lifetime); // lifetime秒後にGameObjectを破壊
        rig = GetComponent<Rigidbody2D>();
        if (normalShot)
        {
            if (gameObject.transform.parent)
            {
                gameObject.transform.parent = null;
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        //  t += Time.deltaTime;
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        if (delay <= 0)
        {
            rig.velocity = transform.up * speed;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))//プレイヤーに接触した時にHPを減らす処理
        {
            if (!specialBullet)
            {
                collision.gameObject.GetComponent<PlayerBase>().HP[collision.GetComponent<PlayerBase>().CharaIndex] -= Damage;



                GameObject DamageUI = Instantiate(damageText, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                DamageUI.transform.SetParent(damageCanvas.transform);

                DamageUI.GetComponent<TextMeshProUGUI>().color = Color.red;

                DamageUI.GetComponent<TextMeshProUGUI>().text = Damage.ToString();
                //

              //  collision.gameObject.GetComponent<PlayerBase>().HP[collision.gameObject.GetComponent<PlayerBase>().CharaIndex] -= Damage;//hpを減らす



                if (!penetrate)
                {
                    Destroy(gameObject);
                }

            }

        }
        if (collision.CompareTag("enemy"))
        {
            if (specialBullet)
            {
                collision.gameObject.GetComponent<enemyBase>().hp -= Damage;
                //if (!penetrate)
                //{
                //    Destroy(gameObject);//貫通する弾丸であるため時間経過でのみ消滅
                //}
            }
        }
    }
}
