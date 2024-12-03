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
    public List<int> Capacity;//�X�L���X���b�g�̐�
    public List<Sprite> charactorImage;//�L�����̉摜
    public List<Sprite> rodImage;//����̉摜

    public List<RuntimeAnimatorController> walkAnim;//���s�p�A�j���[�V����

    public GameObject grave;//��

    public Slider HpGauge;
    public Slider MpGauge;

    public int CharaIndex;
    public GunManager gunSc;
    SpriteRenderer rendere;//�L�����摜�̃����_���[
    SpriteRenderer rodRenderer;//����

    public int money;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI dynamicGuide;
    public TextMeshProUGUI floorText;//���݂̊K�w�̃e�L�X�g�@�J�n���Ƀt�F�[�h�A�E�g
    public GameObject changeText;//���Ԑ؂�ւ��p��text�@���Ԃ��o���Ă���\���i�ŏ����̂��Ƃ��s���Ȉׁj

    public int currentFloor;//���݂̊K�w
    public bool floorChange;//�K�w���ς��������true�ɂȂ�

    public float floorFadeoutSpeed;//�K�w�e�L�X�g�̃t�F�[�h�A�E�g���x

    public GameObject gun;//�e

    //  playerData levelSc;

    public GameObject[] existCopy;

    public GameObject gameOverCanvas;
    bool CreateGameOverCanvas = true;//��x�����L�����o�X�����Ȃ� true�Ȃ����Ă���
    GameObject createdCanvas;//�������ꂽ�Q�[���I�[�o�[�L�����o�X
    public RawImage gameOverBlack;//���S����ʂ�^���Âɂ���
    float gameOverAlpha;
    Color gameOverImageColor;


    public bool isGameOver;//�S�Ł@



    int tombLimit = 2;//���S��悪�����ɍ���邽�߂���Ŗh��
    public GameObject minimap;//�~�j�}�b�v�@���j���[��\������
    // Start is called before the first frame update
    public GameObject playercamera;//���S��V�[���J�ڎ��Ɉ�u�����J�����������Ă��܂����玀�񂾂Ƃ��ɂ������Ɗ֌W��������

    public GameObject gunNameTextObj;
    public GameObject gunMpText;
    public GameObject gunDamageText;
    public GameObject gunExplainText;

    public RawImage fade;//�t�F�[�h�A�E�g
    Color currentColor;
    float newAlpha;
    public float fadeSpeed;
    public GameObject weapon;//����

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
            Debug.Log("HP������");
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
            fade.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);//�V�[���A�j���[�V����

            floorChange = false;

        }
        if (floorText.alpha > 0)
        {
            floorText.text = "1-" + currentFloor;
            floorText.alpha -= floorFadeoutSpeed * Time.deltaTime;
            Debug.Log("alpha--");
            //Destroy(floorText);
        }

        if (gunSc.EditOn)//�}�b�v�ƕ������d�Ȃ邩��Ώ�
        {
            minimap.SetActive(false);
        }
        else
        {
            minimap.SetActive(true);
        }

        if (fade.color.a > 0)//�ŏ��ɃV�[���ړ��������̃t�F�[�h����
        {
            //fadeOutBlack.color.a += fadeSpeed * Time.deltaTime;
            currentColor = fade.color;
            newAlpha -= fadeSpeed * Time.deltaTime;
            fade.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            Debug.Log("�����x" + fade.color.a);

        }



        moneyText.text = "��" + money.ToString();//��������\��
        if (!gunSc)
        {
            gunSc = GameObject.Find("GunSystem").GetComponent<GunManager>();
        }
        CharaIndex = gunSc.ActiveCharactorIndex;//���ݎg�p���Ă���L�����ԍ�
        HpGauge.value = HP[CharaIndex] / MaxHp[CharaIndex];//HP�Q�[�W�̏���
        MpGauge.value = MP[CharaIndex] / MaxMp[CharaIndex];//Mp�Q�[�W�̏���
        hpText.text = Mathf.Floor(HP[CharaIndex]).ToString() + "/" + Mathf.Floor(MaxHp[CharaIndex]).ToString();
        mpText.text = Mathf.FloorToInt(MP[CharaIndex]).ToString() + "/" + Mathf.FloorToInt(MaxMp[CharaIndex]).ToString();

        for (int i = 0; i < HP.Count; i++)//�ߏ�񕜖h�~
        {
            if (HP[i] > MaxHp[i])
            {
                HP[i] = MaxHp[i];
            }



        }

        rendere.sprite = charactorImage[CharaIndex];//�X�v���C�g���g�p�L�����ɕύX
        rodRenderer.sprite = rodImage[CharaIndex];//������g�p�L�����ɕύX
        


        for (int i = 0; i < 4; i++)
        {
            if (MP[i] < MaxMp[i])
            {
                MP[i] += MpRegenerateSpeed[i];//Mp������
            }
        }


        if (gunSc.PlayerExistObj.Count(item => item) > 1)//player��l�ȏ�
        {
            changeText.SetActive(true);
        }
        else
        {
            changeText.SetActive(false);
        }

        if (HP[CharaIndex] <= 0)//���S��
        {
            Debug.Log("�c��v���C���[�l��" + gunSc.exist.Count(item => item));
            if (gunSc.PlayerExistObj.Count(item => item) > 1)//�Ō�̈�l�ł͂Ȃ�
            {
                //Debug.Log("sibou");
                int pastPlayerIndex = CharaIndex;//�����񂾃v���C���[�̔ԍ�
                Debug.Log("�����񂾃v���C���[�̔ԍ��́F" + CharaIndex + "�Ԃł�");

                gunSc.Rescued = true; ;//null�ł͂Ȃ��v���C���[�Ɉڍs


                Debug.Log("Gunmanager�̏����͏I���PlayerBase�ł�");
                Debug.Log("���̃v���C���[�̔ԍ��́F" + CharaIndex);

                //  if (gunSc.PlayerExistObj[pastPlayerIndex] != null)
                {
                    GameObject delete = gunSc.PlayerExistObj[pastPlayerIndex];

                    Debug.Log(delete + "���폜���܂�");
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
                    //�Ō�̈�l�@�Q�[���I�[�o�[

                    if (HP.Sum() < 0)
                    {
                        Debug.Log("GAMEOVER");

                        // gameOverCanvas.SetActive(true);
                        if (CreateGameOverCanvas)//�܂�����Ă��Ȃ�
                        {
                            createdCanvas = Instantiate(gameOverCanvas, transform.position, transform.rotation);
                            gameOverBlack = createdCanvas.GetComponent<GameOver>().blackBack;//�w�i�擾
                            CreateGameOverCanvas = false;
                        }



                        isGameOver = true;
                        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;//���S���ɓ����蔻����g���K�[�ɕύX


                        gameOverImageColor = gameOverBlack.color;
                        gameOverAlpha += fadeSpeed * Time.deltaTime;
                        gameOverBlack.color = new Color(currentColor.r, currentColor.g, currentColor.b, gameOverAlpha);

                        playercamera.transform.parent = null;
                        tombLimit--;
                        //}
                        gun.SetActive(false);

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            // SceneManager.LoadScene("Title");//���Ŕ�񂾂�v���C���[���^�C�g���ɍs���Ă��܂�

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
