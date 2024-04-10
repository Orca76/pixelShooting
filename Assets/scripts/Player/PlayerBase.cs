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
    public List<int> Capacity;//�X�L���X���b�g�̐�
    public List<Sprite> charactorImage;//�L�����̉摜

    public List<RuntimeAnimatorController> walkAnim;//���s�p�A�j���[�V����

    public GameObject grave;//��


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
    public TextMeshProUGUI floorText;//���݂̊K�w�̃e�L�X�g�@�J�n���Ƀt�F�[�h�A�E�g
    public int currentFloor;//���݂̊K�w

    public float floorFadeoutSpeed;//�K�w�e�L�X�g�̃t�F�[�h�A�E�g���x

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
            Debug.Log("HP������");
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

        moneyText.text = "��" + money.ToString();//��������\��
        if (!gunSc)
        {
            gunSc = GameObject.Find("GunSystem").GetComponent<GunManager>();
        }
        CharaIndex = gunSc.ActiveCharactorIndex;//���ݎg�p���Ă���L�����ԍ�
        HpGauge.value = HP[CharaIndex] / MaxHp[CharaIndex];//HP�Q�[�W�̏���
        MpGauge.value = MP[CharaIndex] / MaxMp[CharaIndex];//Mp�Q�[�W�̏���
        hpText.text = HP[CharaIndex].ToString() + "/" + MaxHp[CharaIndex].ToString();
        mpText.text = Mathf.FloorToInt(MP[CharaIndex]).ToString() + "/" + MaxMp[CharaIndex].ToString();

        for (int i = 0; i < HP.Count; i++)//�ߏ�񕜖h�~
        {
            if (HP[i] > MaxHp[i])
            {
                HP[i] = MaxHp[i];
            }
           


        }

        rendere.sprite = charactorImage[CharaIndex];//�X�v���C�g���g�p�L�����ɕύX


        for (int i = 0; i < 4; i++)
        {
            if (MP[i] < MaxMp[i])
            {
                MP[i] += MpRegenerateSpeed[i];//Mp������
            }
        }



        if (HP[CharaIndex] <= 0)//���S��
        {
            Debug.Log(gunSc.exist.Count(item => item));
            if (gunSc.PlayerExistObj.Count(item => item) > 1)//�Ō�̈�l�ł͂Ȃ�
            {
                Debug.Log("sibou");
                int pastPlayerIndex = CharaIndex;//�����񂾃v���C���[�̔ԍ�
                Debug.Log("�����񂾃v���C���[�̔ԍ��́F" + CharaIndex);

                gunSc.Rescued = true; ;//null�ł͂Ȃ��v���C���[�Ɉڍs
                Debug.Log("���̃v���C���[�̔ԍ��́F" + CharaIndex);

                GameObject delete = gunSc.PlayerExistObj[pastPlayerIndex];

                Debug.Log(delete + "���폜���܂�");
                gunSc.exist[CharaIndex] = false;
                gunSc.PlayerExistObj[pastPlayerIndex] = null;
                Destroy(delete);




            }
            else
            {
                //�Ō�̈�l�@�Q�[���I�[�o�[
            }
        }



    }

}
