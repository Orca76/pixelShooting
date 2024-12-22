using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class statueBase : MonoBehaviour
{
    //プレイヤーのステータスを強化出来る石像　使うごとに対応ステータス+0.2倍　使用するごとに値段2倍　初期値段50

    public float collisionDistance;//基準距離　これより近いと接触判定
    GameObject Player;//プレイヤー
    public int prayNum;//石像を使用した回数
    public int cost;//必要金額

    public int statusType;//上昇してくれるステータス 昇順にHP MP MP回復速度 
    public TextMeshProUGUI costText;

    public int circleType = 0;//0ならステータス増強　1なら回復 2なら召喚
    public GameObject secretBoss;
    public int baseCost = 50;

    public GameObject healParticle;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        costText.text = "$" + cost;
        cost =(int)(baseCost * Mathf.Pow(2, prayNum));
        if (collisionDistance > Vector3.Distance(gameObject.transform.position, Player.transform.position))//プレイヤーの近く
        {
            PlayerBase Pdata = Player.GetComponent<PlayerBase>();
            Pdata.dynamicGuide.text = "祈る:Space";
            Pdata.dynamicGuide.alpha = 1;
            if (Input.GetKeyDown(KeyCode.Space))//
            {
                if (Pdata.money >= cost)//所持金足りてる
                {

                    Pdata.dynamicGuide.text = "";

                    if(circleType == 0)
                    {
                        switch (statusType)
                        {
                            case 0://HP石像
                                Pdata.MaxHp[Pdata.CharaIndex] *= 1.4f;
                                break;
                            case 1://MP石像
                                Pdata.MaxMp[Pdata.CharaIndex] *= 1.4f;
                                break;
                            case 2://MP自動回復石像
                                Pdata.MpRegenerateSpeed[Pdata.CharaIndex] *= 1.4f;
                                break;


                        }
                        Player.GetComponent<PlayerBase>().money -= cost;//金払う
                        prayNum++;
                    }
                    else if (circleType == 1)
                    {
                        Pdata.HP[Pdata.CharaIndex] += 30;
                        Player.GetComponent<PlayerBase>().money -= cost;//金払う
                        //回復
                        Instantiate(healParticle, Player.transform.position, transform.rotation);
                    }
                    else
                    {
                        Instantiate(secretBoss, transform.position, transform.rotation);
                        Player.GetComponent<PlayerBase>().money -= cost;//金払う
                        Destroy(gameObject);
                    }
                

                }
                
            }
        }
    }
}
