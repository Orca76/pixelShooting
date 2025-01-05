using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyBase : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float speed;
    public GameObject bullet;
    public float shotSpan;
    public GameObject turret;
    float t;
    GameObject player;

    public Slider hpGauge;

    public int dropMoney;
    public GameObject gold;
    public GameObject silver;
    public GameObject copper;
    float firstScale;

    public int SpriteDirection = 1;//�X�v���C�g�̌����̔��]�@�G�̒i�K�Ń~�X���Ă���

    public float dropRate;
    public float itemDropRate = 10;

    public bool minion = true;//�ʏ�̎G�������ʏ�U�����s���� 
    public bool lockOn = true;//�v���C���[�ւ̃��b�N�I�����s����
    public bool disappier = true;//���񂾂Ƃ������邩�@�h���[���̏��������Ɣ�邽�ߍ쐬

    public GameObject HpBar;//���]�����p

    public GameObject ClearCanvas;//�{�X�h���b�v�݂����Ȃ��� �N���A�\��
    public bool secretBoss = false;
    public GameObject queen;
    // Start is called before the first frame update

    public AudioClip sound1; // �Đ�������SE����1
    public AudioClip sound2; // �Đ�������SE����2
    private AudioSource audioSource;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        firstScale =Mathf.Abs( gameObject.transform.localScale.x);
        if (gameObject.transform.parent)
        {
            gameObject.transform.parent = null;
        }
        audioSource = GetComponent<AudioSource>();
      //  audioSource.PlayOneShot(sound1);

    }

    // Update is called once per frame
    void Update()
    {

        
       

        t += Time.deltaTime;
        if (minion)
        {
            HpBar.transform.localScale = new Vector3(Mathf.Abs(HpBar.transform.localScale.x) *
           Mathf.Sign(gameObject.transform.localScale.x), HpBar.transform.localScale.y, HpBar.transform.localScale.z);//HP�Q�[�W���]����
            if (t > shotSpan)
            {
                Instantiate(bullet, transform.position, turret.transform.rotation);
                t = 0;
            }
        }
       

        if (gameObject.transform.position.x < player.transform.position.x)//�v���C���[�����ɂ���Ƃ��X�v���C�g�𔽓]
        {
            gameObject.transform.localScale = new Vector3(-firstScale * SpriteDirection,Mathf.Abs( firstScale), 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(firstScale * SpriteDirection, Mathf.Abs(firstScale), 1);
        }

        hpGauge.value = hp / maxHp;

        if (lockOn)
        {
            // �Ώە��ւ̃x�N�g�����Z�o
            Vector3 toDirection = player.transform.position - gameObject.transform.position;
            // �Ώە��։�]����
            turret.transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
        }
       

        if (hp <= 0&&disappier)
        {
            AudioSource.PlayClipAtPoint(sound2, transform.position);

            //�h���b�v�̏����Ƃ���������
            int goldDropNum = (dropMoney < 20) ? 0 : (dropMoney / 20);
            dropMoney -= goldDropNum * 20;
            int silverDropNum = (dropMoney < 5) ? 0 : (dropMoney / 5);
            dropMoney -= silverDropNum * 5;
            int copperDropNum = (dropMoney < 1) ? 0 : (dropMoney);

            Debug.Log("gold_silver_copper=:" + goldDropNum + " " + silverDropNum + " " + copperDropNum);
            for (int i = 0; i < goldDropNum; i++)
            {
                Instantiate(gold, transform.position + new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0).normalized * 0.05f, transform.rotation);
            }
            for (int i = 0; i < silverDropNum; i++)
            {
                Instantiate(silver, transform.position + new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0).normalized * 0.05f, transform.rotation);
            }
            for (int i = 0; i < copperDropNum; i++)
            {
                Instantiate(copper, transform.position + new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0).normalized * 0.05f, transform.rotation);
            }

            int x = Random.Range(0, 100);
            if (x < dropRate)//�h���b�v
            {
                GameObject[] s = GameObject.Find("GunSystem").GetComponent<DataBase>().Components;
                Instantiate(s[Random.Range(0, s.Length)], gameObject.transform.position, gameObject.transform.rotation);
            }


            int y = Random.Range(0, 100);
            if (x < itemDropRate)//�h���b�v
            {
                GameObject[] s = GameObject.Find("GunSystem").GetComponent<DataBase>().items;
                Instantiate(s[Random.Range(0, s.Length)], gameObject.transform.position, gameObject.transform.rotation);
            }
            if (!minion)//�{�X���S��
            {
                Instantiate(ClearCanvas);//�N���A���o
            }
            if (secretBoss)
            {
                Instantiate(queen, transform.position,transform.rotation);
            }

            Destroy(gameObject);
        }


    }



}
