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
    public int currentFloor;



    public GameObject[] existCopy;
    // Start is called before the first frame update
    void Start()
    {
       // MaxHp = HP;
        gunSc = GameObject.Find("GunSystem").GetComponent<GunManager>();
        rendere = gameObject.GetComponent<SpriteRenderer>();

    }

    int earliestIndex(GameObject[] array)
    {//�z��̒��ň�ԑ����o������null�łȂ��v�f�̃C���f�b�N�X��Ԃ�
        int index = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null)
            {
                index = i;
                break; // ��ԑ����o������ null ���������烋�[�v���I��
            }
        }
        return index;

    }

    // Update is called once per frame
    void Update()
    {
        
        moneyText.text = "��"+money.ToString();//��������\��
        if (!gunSc)
        {
            gunSc = GameObject.Find("GunSystem").GetComponent<GunManager>();
        }
        CharaIndex = gunSc.ActiveCharactorIndex;//���ݎg�p���Ă���L�����ԍ�
        HpGauge.value = HP[CharaIndex] / MaxHp[CharaIndex];//HP�Q�[�W�̏���
        MpGauge.value = MP[CharaIndex] / MaxMp[CharaIndex];//Mp�Q�[�W�̏���
        hpText.text = HP[CharaIndex].ToString() + "/" + MaxHp[CharaIndex].ToString();
        mpText.text = Mathf.FloorToInt( MP[CharaIndex]).ToString() + "/" + MaxMp[CharaIndex].ToString();

      
        

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
