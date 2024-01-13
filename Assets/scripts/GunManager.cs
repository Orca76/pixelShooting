using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GunManager : MonoBehaviour
{
    public GameObject Players;
    //public GameObject ActivePlayer;
    public GameObject turret;
    public GameObject[] Components0;
    public GameObject[] Components1;
    public GameObject[] Components2;
    public GameObject[] Components3;
    public GameObject EmptySlot;//空のスキルスロット

    public int baseIndex;//4で割る元の数
    public int ActiveCharactorIndex;//現在操作中のキャラクター　0,1,2,3で区別
    //public int CharactorCount = 1;//現在の仲間の数 1~4

    public GameObject EditBackGround;
    public bool EditOn;//編集中
    int editnum = 0;
    DataBase DataBase_Script;
    public Vector3 EditPosition;//エディットのオブジェクト

    public GameObject cameraObj;//カメラ

    public bool Rescued;//仲間が増える

    public GameObject nameText;
    public GameObject mpText;
    public GameObject damageText;
    public GameObject explainText;

    public bool gunSet;//銃をセットする　trueでセット　味方が出来たときとシーンをロードした時



    public GameObject[] PlayerExistObj;//プレイヤーが存在するのか判別用のオブジェクト　キャラが生成されたときにスキルスロットを格納　0,1,2,3

    public bool[] exist;
    void Start()
    {

        Players = GameObject.FindWithTag("Player");
        EditBackGround = GameObject.FindWithTag("backGround");
        DataBase_Script = gameObject.GetComponent<DataBase>();

        nameText = GameObject.Find("name");
        mpText = GameObject.Find("cost");
        damageText = GameObject.Find("damage");
        explainText = GameObject.Find("explain");

        turret = GameObject.Find("Gun");
        cameraObj = GameObject.Find("Main Camera");

        gunSet = true;

        PlayerExistObj = Players.GetComponent<PlayerBase>().existCopy;
        //nameText.SetActive(false);

    }

   
  
    // Update is called once per frame
    void Update()
    {
        ActiveCharactorIndex = baseIndex % 4;
      

        if (Input.GetKeyDown(KeyCode.B) || Input.GetMouseButtonDown(0))//撃つ
        {
            if (!EditOn)
            {
                Shot();
            }

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            editnum++;
            gunSet = true;
        }
        if (editnum % 2 != 0)
        {
            EditOn = true;
        }
        else
        {
            EditOn = false;
        }

        if (Input.GetKeyDown(KeyCode.C)||Rescued)//操作キャラ変更　デバッグ用
        {
            Debug.Log("Cおす");
            int i = 1;
            while (true)
            {
                //Debug.Log("exist[(" + ActiveCharactorIndex + " + " + i + ") % 4]は" + exist[(ActiveCharactorIndex + i) % 4]);
                if (PlayerExistObj[(ActiveCharactorIndex + i) % 4] != false)//existに枠が存在
                {
                    baseIndex += i;

                    break;
                }
                else
                {
                    i++;
                }
            }
            Rescued = false;
        }


        for (int j = 0; j < 4; j++)
        {
          

            if (exist[j] == true)//プレイヤーは存在していて
            {

                if (PlayerExistObj[j] == null)//スキルは無い
                {
                   
                    int createNumber = j;//次にスキルスロットを作るインデックスを取得

                    int SkillNum = Players.GetComponent<PlayerBase>().Capacity[createNumber];//スキルスロットの数

                    //キャラのスプライトを表示
                    GameObject charaSprite = new GameObject();//空のオブジェクトを生成

                    SpriteRenderer spriteSc = charaSprite.AddComponent<SpriteRenderer>();
                    spriteSc.sprite = Players.GetComponent<PlayerBase>().charactorImage[createNumber];//スプライトを登録
                    spriteSc.sortingOrder = 6;//表示順の調整
                    spriteSc.transform.localScale = new Vector3(-0.6f, 0.6f, 1);
                    spriteSc.transform.position = new Vector3(-0.9f, 0.3f - 0.2f * (createNumber), 0) + EditBackGround.transform.position;

                    //一連のオブジェクトを全てとあるオブジェクトの子オブジェクトにしておく
                    GameObject temporaryParent = new GameObject();
                    temporaryParent.transform.parent = EditBackGround.transform;
                    charaSprite.transform.parent = temporaryParent.transform;

                    for (int i = 0; i < SkillNum; i++)
                    {
                        GameObject Slot = Instantiate(EmptySlot, new Vector3(-0.8f + 0.08f * i, 0.3f - 0.2f * (createNumber), 0) + EditBackGround.transform.position, transform.rotation);
                        Slot.transform.parent = temporaryParent.transform;
                        Slot.GetComponent<SkillSlot>().SkillIndex = i;
                        Slot.GetComponent<SkillSlot>().PlayerNumber = createNumber;
                        PlayerExistObj[createNumber] = temporaryParent;
                    }

                }
            }
        }

        if (gunSet)//銃弾セット　デバッグ用
        {

            for (int j = 0; j < Components0.Length; j++)
            {
                if (Components0[j] != null)
                {
                    Components0[j].GetComponent<BulletComponent>().NextComponent = null;
                    Components0[j].GetComponent<BulletComponent>().NextBulletNumber = 0;
                }
                if (Components1[j] != null)
                {
                    Components1[j].GetComponent<BulletComponent>().NextComponent = null;
                    Components1[j].GetComponent<BulletComponent>().NextBulletNumber = 0;
                }
                if (Components2[j] != null)
                {
                    Components2[j].GetComponent<BulletComponent>().NextComponent = null;
                    Components2[j].GetComponent<BulletComponent>().NextBulletNumber = 0;
                }
                if (Components3[j] != null)
                {
                    Components3[j].GetComponent<BulletComponent>().NextComponent = null;
                    Components3[j].GetComponent<BulletComponent>().NextBulletNumber = 0;
                }
            }

            GameObject[] Slots = GameObject.FindGameObjectsWithTag("SkillSlot");//スキルスロットをすべて取得
            foreach (GameObject slo in Slots)
            {
                slo.GetComponent<SkillSlot>().SkillDetect();//スキル検知の処理
            }
            int i = 0;


            while (true)
            {

               
                {
                    if (Components0[i] != null)//最初の項目が埋まっている
                    {
                        if (Components0[i + 1] == null)//その次の項目が埋まっていない
                        {
                            break;//設定終了
                        }
                        else
                        {
                            Components0[i].GetComponent<BulletComponent>().NextComponent = Components0[i + 1];//コンポーネントの次のコンポーネントに設定
                            Components0[i].GetComponent<BulletComponent>().NextBulletNumber = Components0[i + 1].GetComponent<BulletComponent>().Number;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (i >= Players.GetComponent<PlayerBase>().Capacity[0] - 1)
                {
                    break;
                }
                i++;
            }
            i = 0;
            while (true)
            {

                //if (ActiveCharactorIndex == 0)//0番目のプレイヤー
                {
                    if (Components1[i] != null)//最初の項目が埋まっている
                    {
                        if (Components1[i + 1] == null)//その次の項目が埋まっていない
                        {
                            break;//設定終了
                        }
                        else
                        {
                            Components1[i].GetComponent<BulletComponent>().NextComponent = Components1[i + 1];//コンポーネントの次のコンポーネントに設定
                            Components1[i].GetComponent<BulletComponent>().NextBulletNumber = Components1[i + 1].GetComponent<BulletComponent>().Number;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (i >= Players.GetComponent<PlayerBase>().Capacity[1] - 1)
                {
                    break;
                }
                i++;
            }
            i = 0;
            while (true)
            {

                //if (ActiveCharactorIndex == 0)//0番目のプレイヤー
                {
                    if (Components2[i] != null)//最初の項目が埋まっている
                    {
                        if (Components2[i + 1] == null)//その次の項目が埋まっていない
                        {
                            break;//設定終了
                        }
                        else
                        {
                            Components2[i].GetComponent<BulletComponent>().NextComponent = Components2[i + 1];//コンポーネントの次のコンポーネントに設定
                            Components2[i].GetComponent<BulletComponent>().NextBulletNumber = Components2[i + 1].GetComponent<BulletComponent>().Number;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (i >= Players.GetComponent<PlayerBase>().Capacity[2] - 1)
                {
                    break;
                }
                i++;
            }
            i = 0;
            while (true)
            {

                //if (ActiveCharactorIndex == 0)//0番目のプレイヤー
                {
                    if (Components3[i] != null)//最初の項目が埋まっている
                    {
                        if (Components3[i + 1] == null)//その次の項目が埋まっていない
                        {
                            break;//設定終了
                        }
                        else
                        {
                            Components3[i].GetComponent<BulletComponent>().NextComponent = Components3[i + 1];//コンポーネントの次のコンポーネントに設定
                            Components3[i].GetComponent<BulletComponent>().NextBulletNumber = Components3[i + 1].GetComponent<BulletComponent>().Number;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (i >= Players.GetComponent<PlayerBase>().Capacity[3] - 1)
                {
                    break;
                }
                i++;
            }
            gunSet = false;
        }




        if (EditOn)//編集中
        {
            // EditBackGround.SetActive(true);
            EditBackGround.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, 0);
            nameText.SetActive(true);
            mpText.SetActive(true);
            damageText.SetActive(true);
            explainText.SetActive(true);


            if (Input.GetKeyDown(KeyCode.L) || Input.GetMouseButtonUp(0))//銃弾セット　デバッグ用
            {

                for (int j = 0; j < Components0.Length; j++)
                {
                    if (Components0[j] != null)
                    {
                        Components0[j].GetComponent<BulletComponent>().NextComponent = null;
                        Components0[j].GetComponent<BulletComponent>().NextBulletNumber = 0;
                    }
                    if (Components1[j] != null)
                    {
                        Components1[j].GetComponent<BulletComponent>().NextComponent = null;
                        Components1[j].GetComponent<BulletComponent>().NextBulletNumber = 0;
                    }
                    if (Components2[j] != null)
                    {
                        Components2[j].GetComponent<BulletComponent>().NextComponent = null;
                        Components2[j].GetComponent<BulletComponent>().NextBulletNumber = 0;
                    }
                    if (Components3[j] != null)
                    {
                        Components3[j].GetComponent<BulletComponent>().NextComponent = null;
                        Components3[j].GetComponent<BulletComponent>().NextBulletNumber = 0;
                    }
                }

                GameObject[] Slots = GameObject.FindGameObjectsWithTag("SkillSlot");//スキルスロットをすべて取得
                foreach (GameObject slo in Slots)
                {
                    slo.GetComponent<SkillSlot>().SkillDetect();//スキル検知の処理
                }
                int i = 0;


                while (true)
                {

                    //if (ActiveCharactorIndex == 0)//0番目のプレイヤー
                    {
                        if (Components0[i] != null)//最初の項目が埋まっている
                        {
                            if (Components0[i + 1] == null)//その次の項目が埋まっていない
                            {
                                break;//設定終了
                            }
                            else
                            {
                                Components0[i].GetComponent<BulletComponent>().NextComponent = Components0[i + 1];//コンポーネントの次のコンポーネントに設定
                                Components0[i].GetComponent<BulletComponent>().NextBulletNumber = Components0[i + 1].GetComponent<BulletComponent>().Number;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (i >= Players.GetComponent<PlayerBase>().Capacity[0] - 1)
                    {
                        break;
                    }
                    i++;
                }
                i = 0;
                while (true)
                {

                    //if (ActiveCharactorIndex == 0)//0番目のプレイヤー
                    {
                        if (Components1[i] != null)//最初の項目が埋まっている
                        {
                            if (Components1[i + 1] == null)//その次の項目が埋まっていない
                            {
                                break;//設定終了
                            }
                            else
                            {
                                Components1[i].GetComponent<BulletComponent>().NextComponent = Components1[i + 1];//コンポーネントの次のコンポーネントに設定
                                Components1[i].GetComponent<BulletComponent>().NextBulletNumber = Components1[i + 1].GetComponent<BulletComponent>().Number;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (i >= Players.GetComponent<PlayerBase>().Capacity[1] - 1)
                    {
                        break;
                    }
                    i++;
                }
                i = 0;
                while (true)
                {

                    //if (ActiveCharactorIndex == 0)//0番目のプレイヤー
                    {
                        if (Components2[i] != null)//最初の項目が埋まっている
                        {
                            if (Components2[i + 1] == null)//その次の項目が埋まっていない
                            {
                                break;//設定終了
                            }
                            else
                            {
                                Components2[i].GetComponent<BulletComponent>().NextComponent = Components2[i + 1];//コンポーネントの次のコンポーネントに設定
                                Components2[i].GetComponent<BulletComponent>().NextBulletNumber = Components2[i + 1].GetComponent<BulletComponent>().Number;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (i >= Players.GetComponent<PlayerBase>().Capacity[2] - 1)
                    {
                        break;
                    }
                    i++;
                }
                i = 0;
                while (true)
                {

                    //if (ActiveCharactorIndex == 0)//0番目のプレイヤー
                    {
                        if (Components3[i] != null)//最初の項目が埋まっている
                        {
                            if (Components3[i + 1] == null)//その次の項目が埋まっていない
                            {
                                break;//設定終了
                            }
                            else
                            {
                                Components3[i].GetComponent<BulletComponent>().NextComponent = Components3[i + 1];//コンポーネントの次のコンポーネントに設定
                                Components3[i].GetComponent<BulletComponent>().NextBulletNumber = Components3[i + 1].GetComponent<BulletComponent>().Number;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (i >= Players.GetComponent<PlayerBase>().Capacity[3] - 1)
                    {
                        break;
                    }
                    i++;
                }
            }
        }
        else
        {
            //EditBackGround.SetActive(false);
            nameText.SetActive(false);
            mpText.SetActive(false);
            damageText.SetActive(false);
            explainText.SetActive(false);

            EditBackGround.transform.position = EditPosition;//遠くへ飛ばしておく
        }
    }
    void Shot()//銃弾を撃つ
    {
        if (ActiveCharactorIndex == 0)//4で割ったあまりで判別
        {
            if (Components0[0] != null)
            {
                if (Players.GetComponent<PlayerBase>().MP[0] > 0)//魔法を放つMPが残っている場合
                {
                   // Debug.Log("SHOT");
                    GameObject FirstBullet = Instantiate(DataBase_Script.Bullets[Components0[0].GetComponent<BulletComponent>().Number]
               , Players.transform.position, turret.transform.rotation);
                    BulletBase Data = FirstBullet.GetComponent<BulletBase>();
                    Data.RelatedComponent = Components0[0];
                    Data.NextBullet = DataBase_Script.Bullets[Components0[0].GetComponent<BulletComponent>().NextBulletNumber];

                   // Debug.Log("Shot MP消費=" + DataBase_Script.Bullets[Components0[0].GetComponent<BulletComponent>().Number]);
                    Players.GetComponent<PlayerBase>().MP[0] -= DataBase_Script.Components[Components0[0].GetComponent<BulletComponent>().Number].GetComponent<BulletComponent>().cost;
                }

            }

        }
        else if (ActiveCharactorIndex == 1)
        {
            if (Components1[0] != null)
            {
                if (Players.GetComponent<PlayerBase>().MP[1] > 0)//魔法を放つMPが残っている場合
                {
                    Debug.Log("SHOT");
                    GameObject FirstBullet = Instantiate(DataBase_Script.Bullets[Components1[0].GetComponent<BulletComponent>().Number]
               , Players.transform.position, turret.transform.rotation);

                    Debug.Log("SSHOTOOTT");
                    BulletBase Data = FirstBullet.GetComponent<BulletBase>();
                    Data.RelatedComponent = Components1[0];
                    Data.NextBullet = DataBase_Script.Bullets[Components1[0].GetComponent<BulletComponent>().NextBulletNumber];
                    Players.GetComponent<PlayerBase>().MP[1] -= DataBase_Script.Bullets[Components1[0].GetComponent<BulletComponent>().Number].GetComponent<BulletBase>().requiredMp;


                }

            }
        }
        else if (ActiveCharactorIndex == 2)
        {
            if (Components2[0] != null)
            {
                if (Players.GetComponent<PlayerBase>().MP[2] > 0)//魔法を放つMPが残っている場合
                {
                    GameObject FirstBullet = Instantiate(DataBase_Script.Bullets[Components2[0].GetComponent<BulletComponent>().Number]
               , Players.transform.position, turret.transform.rotation);
                    BulletBase Data = FirstBullet.GetComponent<BulletBase>();
                    Data.RelatedComponent = Components2[0];
                    Data.NextBullet = DataBase_Script.Bullets[Components2[0].GetComponent<BulletComponent>().NextBulletNumber];
                    Players.GetComponent<PlayerBase>().MP[2] -= DataBase_Script.Bullets[Components2[0].GetComponent<BulletComponent>().Number].GetComponent<BulletBase>().requiredMp;
                }

            }
        }
        else if (ActiveCharactorIndex == 3)
        {
            if (Components3[0] != null)
            {
                if (Players.GetComponent<PlayerBase>().MP[3] > 0)//魔法を放つMPが残っている場合
                {
                    GameObject FirstBullet = Instantiate(DataBase_Script.Bullets[Components3[0].GetComponent<BulletComponent>().Number]
               , Players.transform.position, turret.transform.rotation);
                    BulletBase Data = FirstBullet.GetComponent<BulletBase>();
                    Data.RelatedComponent = Components3[0];
                    Data.NextBullet = DataBase_Script.Bullets[Components3[0].GetComponent<BulletComponent>().NextBulletNumber];
                    Players.GetComponent<PlayerBase>().MP[3] -= DataBase_Script.Bullets[Components3[0].GetComponent<BulletComponent>().Number].GetComponent<BulletBase>().requiredMp;
                }

            }
        }

    }

}
