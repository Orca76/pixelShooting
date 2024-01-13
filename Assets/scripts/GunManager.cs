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
    public GameObject EmptySlot;//��̃X�L���X���b�g

    public int baseIndex;//4�Ŋ��錳�̐�
    public int ActiveCharactorIndex;//���ݑ��쒆�̃L�����N�^�[�@0,1,2,3�ŋ��
    //public int CharactorCount = 1;//���݂̒��Ԃ̐� 1~4

    public GameObject EditBackGround;
    public bool EditOn;//�ҏW��
    int editnum = 0;
    DataBase DataBase_Script;
    public Vector3 EditPosition;//�G�f�B�b�g�̃I�u�W�F�N�g

    public GameObject cameraObj;//�J����

    public bool Rescued;//���Ԃ�������

    public GameObject nameText;
    public GameObject mpText;
    public GameObject damageText;
    public GameObject explainText;

    public bool gunSet;//�e���Z�b�g����@true�ŃZ�b�g�@�������o�����Ƃ��ƃV�[�������[�h������



    public GameObject[] PlayerExistObj;//�v���C���[�����݂���̂����ʗp�̃I�u�W�F�N�g�@�L�������������ꂽ�Ƃ��ɃX�L���X���b�g���i�[�@0,1,2,3

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
      

        if (Input.GetKeyDown(KeyCode.B) || Input.GetMouseButtonDown(0))//����
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

        if (Input.GetKeyDown(KeyCode.C)||Rescued)//����L�����ύX�@�f�o�b�O�p
        {
            Debug.Log("C����");
            int i = 1;
            while (true)
            {
                //Debug.Log("exist[(" + ActiveCharactorIndex + " + " + i + ") % 4]��" + exist[(ActiveCharactorIndex + i) % 4]);
                if (PlayerExistObj[(ActiveCharactorIndex + i) % 4] != false)//exist�ɘg������
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
          

            if (exist[j] == true)//�v���C���[�͑��݂��Ă���
            {

                if (PlayerExistObj[j] == null)//�X�L���͖���
                {
                   
                    int createNumber = j;//���ɃX�L���X���b�g�����C���f�b�N�X���擾

                    int SkillNum = Players.GetComponent<PlayerBase>().Capacity[createNumber];//�X�L���X���b�g�̐�

                    //�L�����̃X�v���C�g��\��
                    GameObject charaSprite = new GameObject();//��̃I�u�W�F�N�g�𐶐�

                    SpriteRenderer spriteSc = charaSprite.AddComponent<SpriteRenderer>();
                    spriteSc.sprite = Players.GetComponent<PlayerBase>().charactorImage[createNumber];//�X�v���C�g��o�^
                    spriteSc.sortingOrder = 6;//�\�����̒���
                    spriteSc.transform.localScale = new Vector3(-0.6f, 0.6f, 1);
                    spriteSc.transform.position = new Vector3(-0.9f, 0.3f - 0.2f * (createNumber), 0) + EditBackGround.transform.position;

                    //��A�̃I�u�W�F�N�g��S�ĂƂ���I�u�W�F�N�g�̎q�I�u�W�F�N�g�ɂ��Ă���
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

        if (gunSet)//�e�e�Z�b�g�@�f�o�b�O�p
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

            GameObject[] Slots = GameObject.FindGameObjectsWithTag("SkillSlot");//�X�L���X���b�g�����ׂĎ擾
            foreach (GameObject slo in Slots)
            {
                slo.GetComponent<SkillSlot>().SkillDetect();//�X�L�����m�̏���
            }
            int i = 0;


            while (true)
            {

               
                {
                    if (Components0[i] != null)//�ŏ��̍��ڂ����܂��Ă���
                    {
                        if (Components0[i + 1] == null)//���̎��̍��ڂ����܂��Ă��Ȃ�
                        {
                            break;//�ݒ�I��
                        }
                        else
                        {
                            Components0[i].GetComponent<BulletComponent>().NextComponent = Components0[i + 1];//�R���|�[�l���g�̎��̃R���|�[�l���g�ɐݒ�
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

                //if (ActiveCharactorIndex == 0)//0�Ԗڂ̃v���C���[
                {
                    if (Components1[i] != null)//�ŏ��̍��ڂ����܂��Ă���
                    {
                        if (Components1[i + 1] == null)//���̎��̍��ڂ����܂��Ă��Ȃ�
                        {
                            break;//�ݒ�I��
                        }
                        else
                        {
                            Components1[i].GetComponent<BulletComponent>().NextComponent = Components1[i + 1];//�R���|�[�l���g�̎��̃R���|�[�l���g�ɐݒ�
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

                //if (ActiveCharactorIndex == 0)//0�Ԗڂ̃v���C���[
                {
                    if (Components2[i] != null)//�ŏ��̍��ڂ����܂��Ă���
                    {
                        if (Components2[i + 1] == null)//���̎��̍��ڂ����܂��Ă��Ȃ�
                        {
                            break;//�ݒ�I��
                        }
                        else
                        {
                            Components2[i].GetComponent<BulletComponent>().NextComponent = Components2[i + 1];//�R���|�[�l���g�̎��̃R���|�[�l���g�ɐݒ�
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

                //if (ActiveCharactorIndex == 0)//0�Ԗڂ̃v���C���[
                {
                    if (Components3[i] != null)//�ŏ��̍��ڂ����܂��Ă���
                    {
                        if (Components3[i + 1] == null)//���̎��̍��ڂ����܂��Ă��Ȃ�
                        {
                            break;//�ݒ�I��
                        }
                        else
                        {
                            Components3[i].GetComponent<BulletComponent>().NextComponent = Components3[i + 1];//�R���|�[�l���g�̎��̃R���|�[�l���g�ɐݒ�
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




        if (EditOn)//�ҏW��
        {
            // EditBackGround.SetActive(true);
            EditBackGround.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, 0);
            nameText.SetActive(true);
            mpText.SetActive(true);
            damageText.SetActive(true);
            explainText.SetActive(true);


            if (Input.GetKeyDown(KeyCode.L) || Input.GetMouseButtonUp(0))//�e�e�Z�b�g�@�f�o�b�O�p
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

                GameObject[] Slots = GameObject.FindGameObjectsWithTag("SkillSlot");//�X�L���X���b�g�����ׂĎ擾
                foreach (GameObject slo in Slots)
                {
                    slo.GetComponent<SkillSlot>().SkillDetect();//�X�L�����m�̏���
                }
                int i = 0;


                while (true)
                {

                    //if (ActiveCharactorIndex == 0)//0�Ԗڂ̃v���C���[
                    {
                        if (Components0[i] != null)//�ŏ��̍��ڂ����܂��Ă���
                        {
                            if (Components0[i + 1] == null)//���̎��̍��ڂ����܂��Ă��Ȃ�
                            {
                                break;//�ݒ�I��
                            }
                            else
                            {
                                Components0[i].GetComponent<BulletComponent>().NextComponent = Components0[i + 1];//�R���|�[�l���g�̎��̃R���|�[�l���g�ɐݒ�
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

                    //if (ActiveCharactorIndex == 0)//0�Ԗڂ̃v���C���[
                    {
                        if (Components1[i] != null)//�ŏ��̍��ڂ����܂��Ă���
                        {
                            if (Components1[i + 1] == null)//���̎��̍��ڂ����܂��Ă��Ȃ�
                            {
                                break;//�ݒ�I��
                            }
                            else
                            {
                                Components1[i].GetComponent<BulletComponent>().NextComponent = Components1[i + 1];//�R���|�[�l���g�̎��̃R���|�[�l���g�ɐݒ�
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

                    //if (ActiveCharactorIndex == 0)//0�Ԗڂ̃v���C���[
                    {
                        if (Components2[i] != null)//�ŏ��̍��ڂ����܂��Ă���
                        {
                            if (Components2[i + 1] == null)//���̎��̍��ڂ����܂��Ă��Ȃ�
                            {
                                break;//�ݒ�I��
                            }
                            else
                            {
                                Components2[i].GetComponent<BulletComponent>().NextComponent = Components2[i + 1];//�R���|�[�l���g�̎��̃R���|�[�l���g�ɐݒ�
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

                    //if (ActiveCharactorIndex == 0)//0�Ԗڂ̃v���C���[
                    {
                        if (Components3[i] != null)//�ŏ��̍��ڂ����܂��Ă���
                        {
                            if (Components3[i + 1] == null)//���̎��̍��ڂ����܂��Ă��Ȃ�
                            {
                                break;//�ݒ�I��
                            }
                            else
                            {
                                Components3[i].GetComponent<BulletComponent>().NextComponent = Components3[i + 1];//�R���|�[�l���g�̎��̃R���|�[�l���g�ɐݒ�
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

            EditBackGround.transform.position = EditPosition;//�����֔�΂��Ă���
        }
    }
    void Shot()//�e�e������
    {
        if (ActiveCharactorIndex == 0)//4�Ŋ��������܂�Ŕ���
        {
            if (Components0[0] != null)
            {
                if (Players.GetComponent<PlayerBase>().MP[0] > 0)//���@�����MP���c���Ă���ꍇ
                {
                   // Debug.Log("SHOT");
                    GameObject FirstBullet = Instantiate(DataBase_Script.Bullets[Components0[0].GetComponent<BulletComponent>().Number]
               , Players.transform.position, turret.transform.rotation);
                    BulletBase Data = FirstBullet.GetComponent<BulletBase>();
                    Data.RelatedComponent = Components0[0];
                    Data.NextBullet = DataBase_Script.Bullets[Components0[0].GetComponent<BulletComponent>().NextBulletNumber];

                   // Debug.Log("Shot MP����=" + DataBase_Script.Bullets[Components0[0].GetComponent<BulletComponent>().Number]);
                    Players.GetComponent<PlayerBase>().MP[0] -= DataBase_Script.Components[Components0[0].GetComponent<BulletComponent>().Number].GetComponent<BulletComponent>().cost;
                }

            }

        }
        else if (ActiveCharactorIndex == 1)
        {
            if (Components1[0] != null)
            {
                if (Players.GetComponent<PlayerBase>().MP[1] > 0)//���@�����MP���c���Ă���ꍇ
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
                if (Players.GetComponent<PlayerBase>().MP[2] > 0)//���@�����MP���c���Ă���ꍇ
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
                if (Players.GetComponent<PlayerBase>().MP[3] > 0)//���@�����MP���c���Ă���ꍇ
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
