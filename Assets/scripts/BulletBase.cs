using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BulletBase : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RelatedComponent;
    public GameObject NextBullet;
    BulletComponent ComponentScript;

    public float Damage;
    public float requiredMp;

    public bool penetrate;//敵を貫通するか

    public GameObject damageText;
    GameObject damageCanvas;


   public bool multiHit;//毒の類

    float timer;
    public float interval;
    void Start()
    {
        if (RelatedComponent)
        {
            ComponentScript = RelatedComponent.GetComponent<BulletComponent>();
        }
        damageCanvas = GameObject.Find("damageCanvas");

        if (RelatedComponent)
        {
            Damage = RelatedComponent.GetComponent<BulletComponent>().damage;
           // Debug.Log("relatedname=" + RelatedComponent.name + " damage=" + Damage);
            requiredMp = RelatedComponent.GetComponent<BulletComponent>().cost;
          //  Debug.Log("relatedname=" + RelatedComponent.name + " mp=" + requiredMp);


            if (RelatedComponent.GetComponent<BulletComponent>().Adjust == 1)//魔力消費減少
            {
                requiredMp /= 2;
            }
            else if (RelatedComponent.GetComponent<BulletComponent>().Adjust == 2)//攻撃力上昇
            {
                Damage *= 2;
            }
            else if (RelatedComponent.GetComponent<BulletComponent>().Adjust == 5)//貫通弾
            {
                penetrate = true;
            }

        }



    }

    // Update is called once per frame
    void Update()
    {
        if (RelatedComponent)
        {
            if (ComponentScript.Connect)//まだ続くパーツがある場合
            {
                GameObject DataBase = GameObject.Find("GunSystem");

                NextBullet = DataBase.GetComponent<DataBase>().Bullets[ComponentScript.NextBulletNumber];

            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))//ブロックに当たった時
        {
            Destroy(gameObject);
        }
        if (!multiHit)
        {
            if (collision.CompareTag("enemy"))//敵に当たった時
            {
                Debug.Log("敵に接触 敵名="+collision.name);
                //ダメージ表記
                GameObject DamageUI = Instantiate(damageText, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                DamageUI.transform.SetParent(damageCanvas.transform);

                DamageUI.GetComponent<TextMeshProUGUI>().color = Color.red;

                DamageUI.GetComponent<TextMeshProUGUI>().text = Damage.ToString();
                //

                collision.gameObject.GetComponent<enemyBase>().hp -= Damage;//敵のhpを減らす
                if (!penetrate)//貫通しない場合
                {
                    Destroy(gameObject);
                }

            }
            if (collision.CompareTag("Box"))
            {
                Destroy(collision.gameObject);
                if (!penetrate)
                {
                    Destroy(gameObject);
                }

            }
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (multiHit)
        {
            if (collision.CompareTag("enemy"))//敵に当たった時
            {

                timer += Time.deltaTime;

                if (timer >= interval)
                {
                    GameObject DamageUI = Instantiate(damageText, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                    DamageUI.transform.SetParent(damageCanvas.transform);

                    DamageUI.GetComponent<TextMeshProUGUI>().color = Color.red;

                    DamageUI.GetComponent<TextMeshProUGUI>().text = Damage.ToString();
                    //

                    collision.gameObject.GetComponent<enemyBase>().hp -= Damage;//敵のhpを減らす

                    timer = 0;
                }
                //ダメージ表記

            }
        }

    }
}
