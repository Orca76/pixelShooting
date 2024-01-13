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
    {//�z��̒��ň�ԑ����o������null�łȂ��v�f�̃C���f�b�N�X��Ԃ�
        int index = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == false)
            {
                index = i;
                break; // ��ԑ����o������ null ���������烋�[�v���I��
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
            if (dist > Vector2.Distance(gameObject.transform.position, player.transform.position))//�L�����̋߂��ɂ���
            {
                player.GetComponent<PlayerBase>().dynamicGuide.text = "�^�X�P��:Space";
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    Debug.Log("nakama");
                    //���ԂɂȂ�
                    player.GetComponent<PlayerBase>().dynamicGuide.text = "";
                    GunManager Gunsc = GameObject.Find("GunSystem").GetComponent<GunManager>();
                    //�L�������𑝂₷
                    // Gunsc.CharactorCount++;
                    if (Gunsc.PlayerExistObj.Count(item => item) < 4)//true��4�����@���L�����X���b�g�̋󂫂�����
                    {
                        PlayerBase Sc = player.GetComponent<PlayerBase>();
                        int newCharaIndex = earliestIndex(Gunsc.exist);
                        //�f�[�^��ǉ�
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
            if (dist > Vector2.Distance(gameObject.transform.position, player.transform.position))//�L�����̋߂��ɂ���
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (player.GetComponent<PlayerBase>().money >= cost)//��������邩
                    {

                        player.GetComponent<PlayerBase>().money -= cost;//��������
                        unlocked = true;

                    }
                }
            }

        }

    }
}