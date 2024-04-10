using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class PlayerBase : MonoBehaviour
{

    public List<float> HP;
    public List<float> MaxHp;
    public List<float> MP;
    public List<float> MaxMp;
    public List<float> MpRegenerateSpeed;
    public List<float> Speed;
    public List<float> ShotSpeed;
    public List<int> Capacity;//スキルスロットの数
    public List<Sprite> charactorImage;//キャラの画像

    public List<RuntimeAnimatorController> walkAnim;//歩行用アニメーション

    public GameObject grave;//墓


    //public List<Sprite> PlayerSprites;
    //public GameObject Gun;

    public Slider HpGauge;
    public Slider MpGauge;

    public int CharaIndex;
    public GunManager gunSc;
    SpriteRenderer rendere;

    public int money;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI dynamicGuide;
    public TextMeshProUGUI floorText;//現在の階層のテキスト　開始時にフェードアウト
    public int currentFloor;//現在の階層

    public float floorFadeoutSpeed;//階層テキストのフェードアウト速度

    playerData levelSc;
    public GameObject[] existCopy;

    // Start is called before the first frame update
    void Start()
    {
        levelSc = gameObject.GetComponent<playerData>();
        // MaxHp = HP;
        gunSc = GameObject.Find("GunSystem").GetComponent<GunManager>();
        rendere = gameObject.GetComponent<SpriteRenderer>();
        //HP = MaxHp;

        MaxHp[0] = MaxHp[0] * levelSc.ratio[levelSc.HpLevel - 1];
        MaxMp[0] = MaxMp[0] * levelSc.ratio[levelSc.MpLevel - 1];
        Speed[0] = Speed[0] * levelSc.ratioSpeed[levelSc.speedLevel - 1];
        MpRegenerateSpeed[0] = MpRegenerateSpeed[0] * levelSc.ratio[levelSc.chargeLevel - 1];

        floorText.text = "1-" + currentFloor;

        for (int i = 0; i < HP.Count; i++)
        {
            HP[i] = MaxHp[i];
            MP[i] = MaxMp[i];
            Debug.Log("HP初期化");
        }

    }


    // Update is called once per frame
    void Update()
    {
        floorText.alpha -= floorFadeoutSpeed * Time.deltaTime;
        if (floorText.alpha < 0)
        {
            Destroy(floorText);
        }

        moneyText.text = "＄" + money.ToString();//所持金を表示
        if (!gunSc)
        {
            gunSc = GameObject.Find("GunSystem").GetComponent<GunManager>();
        }
        CharaIndex = gunSc.ActiveCharactorIndex;//現在使用しているキャラ番号
        HpGauge.value = HP[CharaIndex] / MaxHp[CharaIndex];//HPゲージの処理
        MpGauge.value = MP[CharaIndex] / MaxMp[CharaIndex];//Mpゲージの処理
        hpText.text = HP[CharaIndex].ToString() + "/" + MaxHp[CharaIndex].ToString();
        mpText.text = Mathf.FloorToInt(MP[CharaIndex]).ToString() + "/" + MaxMp[CharaIndex].ToString();

        for (int i = 0; i < HP.Count; i++)//過剰回復防止
        {
            if (HP[i] > MaxHp[i])
            {
                HP[i] = MaxHp[i];
            }
           


        }

        rendere.sprite = charactorImage[CharaIndex];//スプライトを使用キャラに変更


        for (int i = 0; i < 4; i++)
        {
            if (MP[i] < MaxMp[i])
            {
                MP[i] += MpRegenerateSpeed[i];//Mp自動回復
            }
        }



        if (HP[CharaIndex] <= 0)//死亡時
        {
            Debug.Log(gunSc.exist.Count(item => item));
            if (gunSc.PlayerExistObj.Count(item => item) > 1)//最後の一人ではない
            {
                Debug.Log("sibou");
                int pastPlayerIndex = CharaIndex;//今死んだプレイヤーの番号
                Debug.Log("今死んだプレイヤーの番号は：" + CharaIndex);

                gunSc.Rescued = true; ;//nullではないプレイヤーに移行
                Debug.Log("次のプレイヤーの番号は：" + CharaIndex);

                GameObject delete = gunSc.PlayerExistObj[pastPlayerIndex];

                Debug.Log(delete + "を削除します");
                gunSc.exist[CharaIndex] = false;
                gunSc.PlayerExistObj[pastPlayerIndex] = null;
                Destroy(delete);




            }
            else
            {
                //最後の一人　ゲームオーバー
            }
        }



    }

}
