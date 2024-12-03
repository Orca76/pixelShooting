using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
public class PlayerBase : MonoBehaviour
{

    public List<float> HP;
    public List<float> MaxHp;
    public List<float> MP;
    public List<float> MaxMp;
    public List<float> MpRegenerateSpeed;
    // public List<float> Speed;
    public List<float> ShotSpeed;
    public List<int> Capacity;//スキルスロットの数
    public List<Sprite> charactorImage;//キャラの画像
    public List<Sprite> rodImage;//武器の画像

    public List<RuntimeAnimatorController> walkAnim;//歩行用アニメーション

    public GameObject grave;//墓

    public Slider HpGauge;
    public Slider MpGauge;

    public int CharaIndex;
    public GunManager gunSc;
    SpriteRenderer rendere;//キャラ画像のレンダラー
    SpriteRenderer rodRenderer;//武器

    public int money;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI dynamicGuide;
    public TextMeshProUGUI floorText;//現在の階層のテキスト　開始時にフェードアウト
    public GameObject changeText;//仲間切り替え用のtext　仲間が出来てから表示（最初何のことか不明な為）

    public int currentFloor;//現在の階層
    public bool floorChange;//階層が変わった時にtrueになる

    public float floorFadeoutSpeed;//階層テキストのフェードアウト速度

    public GameObject gun;//銃

    //  playerData levelSc;

    public GameObject[] existCopy;

    public GameObject gameOverCanvas;
    bool CreateGameOverCanvas = true;//一度しかキャンバスを作らない trueなら作っていい
    GameObject createdCanvas;//生成されたゲームオーバーキャンバス
    public RawImage gameOverBlack;//死亡時画面を真っ暗にする
    float gameOverAlpha;
    Color gameOverImageColor;


    public bool isGameOver;//全滅　



    int tombLimit = 2;//死亡後墓が無限に作られるためこれで防ぐ
    public GameObject minimap;//ミニマップ　メニュー非表示処理
    // Start is called before the first frame update
    public GameObject playercamera;//死亡後シーン遷移時に一瞬だけカメラが消えてしまうから死んだときにさっさと関係解除する

    public GameObject gunNameTextObj;
    public GameObject gunMpText;
    public GameObject gunDamageText;
    public GameObject gunExplainText;

    public RawImage fade;//フェードアウト
    Color currentColor;
    float newAlpha;
    public float fadeSpeed;
    public GameObject weapon;//武器

    void Start()
    {
        // levelSc = gameObject.GetComponent<playerData>();
        // MaxHp = HP;
        gunSc = GameObject.Find("GunSystem").GetComponent<GunManager>();
        rendere = gameObject.GetComponent<SpriteRenderer>();
        rodRenderer=weapon.GetComponent<SpriteRenderer>();
        //HP = MaxHp;
        
        //   MaxHp[0] = MaxHp[0] * levelSc.ratio[levelSc.HpLevel - 1];
        //   MaxMp[0] = MaxMp[0] * levelSc.ratio[levelSc.MpLevel - 1];

        //   MpRegenerateSpeed[0] = MpRegenerateSpeed[0] * levelSc.ratio[levelSc.chargeLevel - 1];
        //floorText.alpha = 255.0f;
        currentColor = fade.color;
        newAlpha = fade.color.a;
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
        existCopy = gunSc.PlayerExistObj;
        if (floorChange)
        {
            currentFloor++;
            floorText.alpha = 1;

            newAlpha = 1;
            fade.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);//シーンアニメーション

            floorChange = false;

        }
        if (floorText.alpha > 0)
        {
            floorText.text = "1-" + currentFloor;
            floorText.alpha -= floorFadeoutSpeed * Time.deltaTime;
            Debug.Log("alpha--");
            //Destroy(floorText);
        }

        if (gunSc.EditOn)//マップと文字が重なるから対処
        {
            minimap.SetActive(false);
        }
        else
        {
            minimap.SetActive(true);
        }

        if (fade.color.a > 0)//最初にシーン移動した時のフェード処理
        {
            //fadeOutBlack.color.a += fadeSpeed * Time.deltaTime;
            currentColor = fade.color;
            newAlpha -= fadeSpeed * Time.deltaTime;
            fade.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            Debug.Log("透明度" + fade.color.a);

        }



        moneyText.text = "＄" + money.ToString();//所持金を表示
        if (!gunSc)
        {
            gunSc = GameObject.Find("GunSystem").GetComponent<GunManager>();
        }
        CharaIndex = gunSc.ActiveCharactorIndex;//現在使用しているキャラ番号
        HpGauge.value = HP[CharaIndex] / MaxHp[CharaIndex];//HPゲージの処理
        MpGauge.value = MP[CharaIndex] / MaxMp[CharaIndex];//Mpゲージの処理
        hpText.text = Mathf.Floor(HP[CharaIndex]).ToString() + "/" + Mathf.Floor(MaxHp[CharaIndex]).ToString();
        mpText.text = Mathf.FloorToInt(MP[CharaIndex]).ToString() + "/" + Mathf.FloorToInt(MaxMp[CharaIndex]).ToString();

        for (int i = 0; i < HP.Count; i++)//過剰回復防止
        {
            if (HP[i] > MaxHp[i])
            {
                HP[i] = MaxHp[i];
            }



        }

        rendere.sprite = charactorImage[CharaIndex];//スプライトを使用キャラに変更
        rodRenderer.sprite = rodImage[CharaIndex];//武器を使用キャラに変更
        


        for (int i = 0; i < 4; i++)
        {
            if (MP[i] < MaxMp[i])
            {
                MP[i] += MpRegenerateSpeed[i];//Mp自動回復
            }
        }


        if (gunSc.PlayerExistObj.Count(item => item) > 1)//player一人以上
        {
            changeText.SetActive(true);
        }
        else
        {
            changeText.SetActive(false);
        }

        if (HP[CharaIndex] <= 0)//死亡時
        {
            Debug.Log("残りプレイヤー人数" + gunSc.exist.Count(item => item));
            if (gunSc.PlayerExistObj.Count(item => item) > 1)//最後の一人ではない
            {
                //Debug.Log("sibou");
                int pastPlayerIndex = CharaIndex;//今死んだプレイヤーの番号
                Debug.Log("今死んだプレイヤーの番号は：" + CharaIndex + "番です");

                gunSc.Rescued = true; ;//nullではないプレイヤーに移行


                Debug.Log("Gunmanagerの処理は終わりPlayerBaseです");
                Debug.Log("次のプレイヤーの番号は：" + CharaIndex);

                //  if (gunSc.PlayerExistObj[pastPlayerIndex] != null)
                {
                    GameObject delete = gunSc.PlayerExistObj[pastPlayerIndex];

                    Debug.Log(delete + "を削除します");
                    gunSc.exist[CharaIndex] = false;
                    // Debug.Log(gunSc.PlayerExistObj[pastPlayerIndex]);

                    gunSc.PlayerExistObj[pastPlayerIndex] = null;
                    Destroy(delete);
                }

            }
            else
            {
                if (HP[CharaIndex] <= 0)
                {
                    Debug.Log("A");
                    //if (!gunSc.Rescued)
                    //{
                    //最後の一人　ゲームオーバー

                    if (HP.Sum() < 0)
                    {
                        Debug.Log("GAMEOVER");

                        // gameOverCanvas.SetActive(true);
                        if (CreateGameOverCanvas)//まだ作っていない
                        {
                            createdCanvas = Instantiate(gameOverCanvas, transform.position, transform.rotation);
                            gameOverBlack = createdCanvas.GetComponent<GameOver>().blackBack;//背景取得
                            CreateGameOverCanvas = false;
                        }



                        isGameOver = true;
                        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;//死亡時に当たり判定をトリガーに変更


                        gameOverImageColor = gameOverBlack.color;
                        gameOverAlpha += fadeSpeed * Time.deltaTime;
                        gameOverBlack.color = new Color(currentColor.r, currentColor.g, currentColor.b, gameOverAlpha);

                        playercamera.transform.parent = null;
                        tombLimit--;
                        //}
                        gun.SetActive(false);

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            // SceneManager.LoadScene("Title");//直で飛んだらプレイヤーがタイトルに行ってしまう

                        }





                    }
                }


            }
            if (tombLimit > 0)
            {
                Instantiate(grave, transform.position, transform.rotation);
            }

        }



    }

}
