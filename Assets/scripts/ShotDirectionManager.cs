using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDirectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Bullet;
    BulletBase DataBulletBase;
    public GameObject[] ShotPos;//�������ƈʒu��ݒ肷��I�u�W�F�N�g
    public int ModuleType;//0:������,1:�ڐG,2:���Ԋu
    float t;
    public float span;
    public int BulletCharge;//���Ă�e�ۂ̐�
    GameObject Player;
    int SkillIndex;
    GunManager GunManager_Script;
    DataBase DataBase_Script;
    GameObject dataobj;
    float Cost;//�����
    int Index;//�g�p�L����
    void Start()
    {
        DataBulletBase = gameObject.GetComponent<BulletBase>();
        Bullet = DataBulletBase.NextBullet;//���ɔ��˂���I�u�W�F�N�g���擾
        Player = GameObject.FindWithTag("Player");
        if (gameObject.GetComponent<BulletBase>().RelatedComponent)
        {
            SkillIndex = gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().ComponentIndex;
        }
       

        dataobj = GameObject.Find("GunSystem");
        DataBase_Script = dataobj.GetComponent<DataBase>();
        GunManager_Script = dataobj.GetComponent<GunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Index = dataobj.GetComponent<GunManager>().ActiveCharactorIndex;
        if (ModuleType == 0)
        {
            for (int i = 0; i < ShotPos.Length; i++)
            {

                if (Player.GetComponent<PlayerBase>().MP[Player.GetComponent<PlayerBase>().CharaIndex] > 0)//���͂��c���Ă���ꍇ
                {
                    if (Bullet == null)
                    {
                        Bullet = DataBulletBase.NextBullet;//���ɔ��˂���I�u�W�F�N�g���擾
                    }
                    if (Bullet != null)
                    {
                       
                        GameObject FiredProjectile = Instantiate(Bullet, ShotPos[i].transform.position, ShotPos[i].transform.rotation);//�������e�ۂ��I�u�W�F�N�g�Ƃ��ĕێ�
                        BulletBase DataNewObj = FiredProjectile.GetComponent<BulletBase>();
                        Debug.Log("1");
                        if (DataNewObj.RelatedComponent == null)
                        {
                            Debug.Log("nnnn??" + FiredProjectile.name);
                        }
                       
                        if (Player.GetComponent<PlayerBase>().Capacity[Index] - 1 > SkillIndex)//�Ō�̒e�ۂɂ͎��̒e�ۂ�ݒ肵�Ȃ�
                        {
                            Debug.Log("2");
                            if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().NextComponent == true)//�����e�ۂ����݂���
                            {

                                //player0
                                if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 0)
                                {
                                    DataNewObj.RelatedComponent = GunManager_Script.Components0[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                                //Debug.Log(GunManager_Script.com);

                                    if (GunManager_Script.Components0[SkillIndex + 1] != null)
                                    {
                                        
                                        DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components0[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                    }
                                }else if(gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 1)
                                {
                                    DataNewObj.RelatedComponent = GunManager_Script.Components1[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                                //Debug.Log(GunManager_Script.com);
                                    if (GunManager_Script.Components1[SkillIndex + 1] != null)
                                    {
                                        
                                        DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components1[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                    }
                                }
                                else if(gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 2)
                                {
                                    DataNewObj.RelatedComponent = GunManager_Script.Components2[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                                //Debug.Log(GunManager_Script.com);
                                    if (GunManager_Script.Components2[SkillIndex + 1] != null)
                                    {
                                        
                                        DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components2[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                    }
                                }
                                else if(gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 3)
                                {
                                    DataNewObj.RelatedComponent = GunManager_Script.Components3[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                                //Debug.Log(GunManager_Script.com);
                                    if (GunManager_Script.Components3[SkillIndex + 1] != null)
                                    {
                                     
                                        DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components3[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                    }
                                }

                            }

                        }
                        Cost = DataNewObj.RelatedComponent.GetComponent<BulletComponent>().cost;
                        Player.GetComponent<PlayerBase>().MP[Player.GetComponent<PlayerBase>().CharaIndex] -= Cost;
                        Destroy(gameObject);
                    }



                }

               
            }

        }
        if (ModuleType == 2)
        {
            Debug.Log("T="+t);
            t += Time.deltaTime;
            if (t > span)
            {
                Debug.Log("TIme!");
                if (Player.GetComponent<PlayerBase>().MP[Player.GetComponent<PlayerBase>().CharaIndex] > 0)//���͂��c���Ă���ꍇ
                {
                    if (Bullet == null)
                    {
                        Bullet = DataBulletBase.NextBullet;//���ɔ��˂���I�u�W�F�N�g���擾
                    }
                    GameObject FiredProjectile = Instantiate(Bullet, ShotPos[0].transform.position, ShotPos[0].transform.rotation);//�������e�ۂ��I�u�W�F�N�g�Ƃ��ĕێ�
                    BulletBase DataNewObj = FiredProjectile.GetComponent<BulletBase>();
                  
                    if (Player.GetComponent<PlayerBase>().Capacity[Index] - 1 > SkillIndex)//�Ō�̒e�ۂɂ͎��̒e�ۂ�ݒ肵�Ȃ�
                    {
                        if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().NextComponent == true)//�����e�ۂ����݂���
                        {
                            //player0
                            if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 0)
                            {
                                DataNewObj.RelatedComponent = GunManager_Script.Components0[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                            //Debug.Log(GunManager_Script.com);
                                if (GunManager_Script.Components0[SkillIndex + 1] != null)
                                {

                                    DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components0[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                }
                            }
                            else if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 1)
                            {
                                DataNewObj.RelatedComponent = GunManager_Script.Components1[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                            //Debug.Log(GunManager_Script.com);
                                if (GunManager_Script.Components1[SkillIndex + 1] != null)
                                {

                                    DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components1[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                }
                            }
                            else if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 2)
                            {
                                DataNewObj.RelatedComponent = GunManager_Script.Components2[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                            //Debug.Log(GunManager_Script.com);
                                if (GunManager_Script.Components2[SkillIndex + 1] != null)
                                {

                                    DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components2[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                }
                            }
                            else if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 3)
                            {
                                DataNewObj.RelatedComponent = GunManager_Script.Components3[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                            //Debug.Log(GunManager_Script.com);
                                if (GunManager_Script.Components3[SkillIndex + 1] != null)
                                {

                                    DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components3[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                }
                            }

                        }
                        Cost = DataNewObj.RelatedComponent.GetComponent<BulletComponent>().cost;
                        BulletCharge--;

                        Player.GetComponent<PlayerBase>().MP[Player.GetComponent<PlayerBase>().CharaIndex] -= Cost;
                        t = 0;
                        if (BulletCharge <= 0)
                        {
                            Destroy(gameObject);
                        }
                    }


                }
               
            }

           
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ModuleType == 1)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                if (Player.GetComponent<PlayerBase>().MP[Player.GetComponent<PlayerBase>().CharaIndex] > 0)//���͂��c���Ă���ꍇ
                {
                    if (Bullet == null)
                    {
                        Bullet = DataBulletBase.NextBullet;//���ɔ��˂���I�u�W�F�N�g���擾
                    }
                    if (Bullet != null)
                    {
                        GameObject FiredProjectile = Instantiate(Bullet, ShotPos[0].transform.position, ShotPos[0].transform.rotation);//�������e�ۂ��I�u�W�F�N�g�Ƃ��ĕێ�
                        BulletBase DataNewObj = FiredProjectile.GetComponent<BulletBase>();

                        
                        if (Player.GetComponent<PlayerBase>().Capacity[Index] - 1 > SkillIndex)//�Ō�̒e�ۂɂ͎��̒e�ۂ�ݒ肵�Ȃ�
                        {
                            if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().NextComponent == true)//�����e�ۂ����݂���
                            {
                                //player0
                                if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 0)
                                {
                                    DataNewObj.RelatedComponent = GunManager_Script.Components0[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                                //Debug.Log(GunManager_Script.com);
                                    if (GunManager_Script.Components0[SkillIndex + 1] != null)
                                    {

                                        DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components0[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                    }
                                }
                                else if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 1)
                                {
                                    DataNewObj.RelatedComponent = GunManager_Script.Components1[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                                //Debug.Log(GunManager_Script.com);
                                    if (GunManager_Script.Components1[SkillIndex + 1] != null)
                                    {

                                        DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components1[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                    }
                                }
                                else if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 2)
                                {
                                    DataNewObj.RelatedComponent = GunManager_Script.Components2[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                                //Debug.Log(GunManager_Script.com);
                                    if (GunManager_Script.Components2[SkillIndex + 1] != null)
                                    {

                                        DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components2[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                    }
                                }
                                else if (gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>().PlayerIndex == 3)
                                {
                                    DataNewObj.RelatedComponent = GunManager_Script.Components3[SkillIndex + 1];//�V�����e�ۂɑΉ�����R���|�[�l���g��ݒ�
                                                                                                                //Debug.Log(GunManager_Script.com);
                                    if (GunManager_Script.Components3[SkillIndex + 1] != null)
                                    {

                                        DataNewObj.NextBullet = DataBase_Script.Bullets[GunManager_Script.Components3[SkillIndex + 1].GetComponent<BulletComponent>().NextBulletNumber];//���̃R���|�[�l���g�Ɏ��ɑ����e�ۂ��w��

                                    }
                                }

                            }

                        }
                        Cost = DataNewObj.RelatedComponent.GetComponent<BulletComponent>().cost;
                        Player.GetComponent<PlayerBase>().MP[Player.GetComponent<PlayerBase>().CharaIndex] -= Cost;

                        Destroy(gameObject);

                    }



                }
              
            }
        }

    }
}
