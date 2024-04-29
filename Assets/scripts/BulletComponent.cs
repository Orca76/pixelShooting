using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public int Number;//コンポーネントのナンバー
    public bool Connect;//まだパーツは続くか
    public GameObject NextComponent;
    public int NextBulletNumber;
    public int ComponentIndex;//何番目のコンポーネントとなっているか
    public int PlayerIndex;//何番目のプレイヤーの放った攻撃か 0,1,2,3

    public string bulletName;
    public float cost;//消費魔力
    public float damage;
    public string explain;
    public float speed;
    public float lifeTime;

    GameObject gunSystem;
    GunManager gunSc;
    GameObject Player;
    TextMeshProUGUI bulletNameTx;
    TextMeshProUGUI bulletExplainTx;
    TextMeshProUGUI bulletDamageTx;
    TextMeshProUGUI bulletCostTx;

    public bool Picked;//拾われた

    public float collisionDistance;

    public int Adjust = 0;//1=Mp消費減少　2攻撃上昇 3速度上昇 4回転 5貫通 6ホーミング　7ロックオン 8拡大 9減速　10加速

    public int componentType = 0;//0弾丸1調整 2 コネクトパーツ

    public int itemPrice;//価格

    SpriteRenderer renderer;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        gunSystem = GameObject.Find("GunSystem");
        gunSc = gunSystem.GetComponent<GunManager>();
        renderer = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
       

        if (!Picked)//まだ拾われていない
        {
            renderer.sortingOrder = 2;//edit画面で見えないように
            if (collisionDistance > Vector3.Distance(gameObject.transform.position, Player.transform.position))//プレイヤーの近く
            {
                Player.GetComponent<PlayerBase>().dynamicGuide.text = "拾う:Space";
                Player.GetComponent<PlayerBase>().dynamicGuide.alpha = 1;//実体化
                if (Input.GetKeyDown(KeyCode.Space))//ひろう
                {
                    //ひろえる

                    if (Player.GetComponent<PlayerBase>().money >= itemPrice)//所持金足りてる
                    {
                        Player.GetComponent<PlayerBase>().dynamicGuide.text = "";
                        gameObject.transform.parent = GameObject.FindWithTag("backGround").transform;//バックグラウンドの子オブジェクトに指定
                        if (componentType == 0)
                        {
                            gameObject.transform.position = transform.parent.position + new Vector3(0, 0, -0.1f);//親オブジェクトの位置に移動
                        }
                        else
                        {
                            gameObject.transform.position = transform.parent.position + new Vector3(-0.5f, 0, -0.1f);//親オブジェクトの位置に移動
                        }


                        Picked = true;
                        Player.GetComponent<PlayerBase>().money -= itemPrice;//金払う
                    }
                    
                }
            }

        }
        else//拾われた
        {
            renderer.sortingOrder = 7;
        }


        if (componentType == 0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameObject[] Slots = GameObject.FindGameObjectsWithTag("SkillSlot");
                GameObject SetSlot = ClosestComponent(Slots);
                //float dist = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x - SetSlot.transform.position.x, 2) +
                //   Mathf.Pow(gameObject.transform.position.y - SetSlot.transform.position.y, 2));

                float dist = Vector2.Distance(gameObject.transform.position, SetSlot.transform.position);
                if (collisionDistance > dist)//テスト
                {

                    gameObject.transform.position = SetSlot.transform.position + new Vector3(0, 0, -0.1f);
                    ComponentIndex = SetSlot.GetComponent<SkillSlot>().SkillIndex;
                    PlayerIndex = SetSlot.GetComponent<SkillSlot>().PlayerNumber;
                }
            }
        }

    }


    void OnMouseDrag()
    {

        if (!gunSystem)
        {

            gunSystem = GameObject.Find("GunSystem");
            gunSc = gunSystem.GetComponent<GunManager>();
        }
        if (gunSc.EditOn)//ドラッグ移動はエディット中のみ
        {
            if (Picked)//既に取得したもののみ
            {
                transform.position = GetMouseWorldPos();


                bulletNameTx = GameObject.Find("name").GetComponent<TextMeshProUGUI>();
                bulletCostTx = GameObject.Find("cost").GetComponent<TextMeshProUGUI>();
                bulletDamageTx = GameObject.Find("damage").GetComponent<TextMeshProUGUI>();
                bulletExplainTx = GameObject.Find("explain").GetComponent<TextMeshProUGUI>();

                bulletNameTx.text = bulletName;
                bulletCostTx.text = "MP:" + cost.ToString();
                bulletDamageTx.text = "Damage:" + damage.ToString();
                bulletExplainTx.text = explain;
            }
           

        }

    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 a = Camera.main.ScreenToWorldPoint(mousePos);

        a.z = -0.1f;

        return a;
    }
    GameObject ClosestComponent(GameObject[] compos)
    {
        float closestDistance = Mathf.Infinity;
        GameObject target = null;
        foreach (GameObject card in compos)
        {
            float distanceTocomp = Vector3.Distance(transform.position, card.transform.position);
            if (distanceTocomp < closestDistance)
            {
                closestDistance = distanceTocomp;
                target = card;
            }
        }
        return target;
    }
}
