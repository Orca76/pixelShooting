using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestBase : MonoBehaviour
{
    // Start is called before the first frame update
    public float distance;//�f�o�b�O�p�@�v���C���[�Ƃ̋���
    GameObject player;
    DataBase data;
    public bool opened = false;

    public Sprite OpenedSprite;

    public bool unlocked = true;//�󂯂邱�Ƃ��ł���@�_���W�����̃f�t�H���g�͂��� �X�̂�false
    public int cost;//�󔠂̒l�i

    public int componentNumber;//�w��̃R���|�[�l���g���擾
    public bool randomItem = true;
    public bool mimic = false;
    public GameObject mimicObj;

    public AudioClip seClip; // �Đ�������SE��Inspector�Őݒ�
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        data = GameObject.Find("GunSystem").GetComponent<DataBase>();
        mimic = Random.Range(0, 100) < 10;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("distance" + Vector3.Distance(gameObject.transform.position, player.transform.position));

        if (!opened)//�J���Ă��Ȃ�
        {
            if (unlocked)
            {
                if (distance > Vector3.Distance(gameObject.transform.position, player.transform.position))//�v���C���[������ȏ�߂��i�󔠂ɐG��Ă���j
                {
                    GetComponent<AudioSource>().PlayOneShot(seClip);
                    if (mimic)
                    {
                        Instantiate(mimicObj, transform.position, transform.rotation);
                        Destroy(gameObject);
                    }

                    //�{�^���̏����ǉ����邩��
                    if (randomItem)
                    {
                        Instantiate(data.Components[Random.Range(0, data.Components.Length)], transform.position, transform.rotation);//�R���|�[�l���g�������_���ň����
                    }
                    else
                    {
                        Instantiate(data.Components[componentNumber], transform.position, transform.rotation);//�w��̃R���|�[�l���g�������
                    }
                    
                    gameObject.GetComponent<SpriteRenderer>().sprite = OpenedSprite;
                    opened = true;
                }
            }
            else
            {

                if (distance > Vector3.Distance(gameObject.transform.position, player.transform.position))//�v���C���[������ȏ�߂��i�󔠂ɐG��Ă���j
                {
                    //GetComponent<AudioSource>().PlayOneShot(seClip);
                    if (Input.GetKeyDown(KeyCode.Space))//���x����
                    {
                        if (player.GetComponent<PlayerBase>().money >= cost)//��������邩
                        {
                            //Instantiate(data.Components[Random.Range(0, data.Components.Length)], transform.position, transform.rotation);//�R���|�[�l���g�������_���ň����
                           // gameObject.GetComponent<SpriteRenderer>().sprite = OpenedSprite;
                            player.GetComponent<PlayerBase>().money -= cost;//��������
                            unlocked = true;
                            //opened = true;
                        }
                   
                    }
                   
                  
                }
            }
           

        }


    }

}
