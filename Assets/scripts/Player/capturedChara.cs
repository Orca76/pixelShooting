using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class capturedChara : MonoBehaviour
{
    GameObject player;
    playerData levelData;

    float dist = 0.1f;

    public float HP;
    public float MaxHp;
    public float MP;
    public float MaxMp;
    public float MpRegenerateSpeed;
    public float Speed;
    public float ShotSpeed;
    public int Capacity;

    public RuntimeAnimatorController walkAnim;
    float firstScale;

    public bool unlocked;
    public int cost;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        firstScale = gameObject.transform.localScale.x;
        levelData = player.GetComponent<playerData>();

        MaxHp = MaxHp * levelData.ratio[levelData.HpLevel - 1];
        MaxMp = MaxMp * levelData.ratio[levelData.MpLevel - 1];
        Speed = Speed * levelData.ratioSpeed[levelData.speedLevel - 1];
        MpRegenerateSpeed = MpRegenerateSpeed * levelData.ratio[levelData.chargeLevel - 1];
        HP = MaxHp;
        MP = MaxMp;
    }

    // Update is called once per frame
    int earliestIndex(bool[] array)
    {//配列の中で一番早く出現するnullでない要素のインデックスを返す
        int index = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == false)
            {
                index = i;
                break; // 一番早く出現した null を見つけたらループを終了
            }
        }
        return index;

    }
    void Update()
    {
        if (player.transform.position.x > gameObject.transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-firstScale, firstScale, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(firstScale, firstScale, 1);
        }

        if (unlocked)
        {
            if (dist > Vector2.Distance(gameObject.transform.position, player.transform.position))//キャラの近くにいる
            {
                player.GetComponent<PlayerBase>().dynamicGuide.text = "タスケル:Space";
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    Debug.Log("nakama");
                    //仲間になる
                    player.GetComponent<PlayerBase>().dynamicGuide.text = "";
                    GunManager Gunsc = GameObject.Find("GunSystem").GetComponent<GunManager>();
                    //キャラ数を増やす
                    // Gunsc.CharactorCount++;
                    if (Gunsc.PlayerExistObj.Count(item => item) < 4)//trueが4未満　→キャラスロットの空きがある
                    {
                        PlayerBase Sc = player.GetComponent<PlayerBase>();
                        int newCharaIndex = earliestIndex(Gunsc.exist);
                        //データを追加
                        Sc.HP[newCharaIndex] = (HP);
                        Sc.MaxHp[newCharaIndex] = (MaxHp);
                        Sc.MP[newCharaIndex] = (MP);
                        Sc.MaxMp[newCharaIndex] = (MaxMp);
                        Sc.MpRegenerateSpeed[newCharaIndex] = (MpRegenerateSpeed);
                        Sc.Speed[newCharaIndex] = (Speed);
                        Sc.ShotSpeed[newCharaIndex] = (ShotSpeed);
                        Sc.Capacity[newCharaIndex] = (Capacity);
                        Sc.charactorImage[newCharaIndex] = (gameObject.GetComponent<SpriteRenderer>().sprite);
                        Sc.walkAnim[newCharaIndex] = (walkAnim);

                        Gunsc.exist[newCharaIndex] = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
        else
        {
            if (dist > Vector2.Distance(gameObject.transform.position, player.transform.position))//キャラの近くにいる
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (player.GetComponent<PlayerBase>().money >= cost)//金が足りるか
                    {

                        player.GetComponent<PlayerBase>().money -= cost;//金を失う
                        unlocked = true;

                    }
                }
            }

        }

    }
}