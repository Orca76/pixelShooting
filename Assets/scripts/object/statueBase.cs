using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class statueBase : MonoBehaviour
{
    //�v���C���[�̃X�e�[�^�X�������o����Α��@�g�����ƂɑΉ��X�e�[�^�X+0.2�{�@�g�p���邲�Ƃɒl�i2�{�@�����l�i50

    public float collisionDistance;//������@������߂��ƐڐG����
    GameObject Player;//�v���C���[
    public int prayNum;//�Α����g�p������
    public int cost;//�K�v���z

    public int statusType;//�㏸���Ă����X�e�[�^�X ������HP MP MP�񕜑��x 
    public TextMeshProUGUI costText;

    public int circleType = 0;//0�Ȃ�X�e�[�^�X�����@1�Ȃ�� 2�Ȃ珢��
    public GameObject secretBoss;
    public int baseCost = 50;

    public GameObject healParticle;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        costText.text = "$" + cost;
        cost =(int)(baseCost * Mathf.Pow(2, prayNum));
        if (collisionDistance > Vector3.Distance(gameObject.transform.position, Player.transform.position))//�v���C���[�̋߂�
        {
            PlayerBase Pdata = Player.GetComponent<PlayerBase>();
            Pdata.dynamicGuide.text = "�F��:Space";
            Pdata.dynamicGuide.alpha = 1;
            if (Input.GetKeyDown(KeyCode.Space))//
            {
                if (Pdata.money >= cost)//����������Ă�
                {

                    Pdata.dynamicGuide.text = "";

                    if(circleType == 0)
                    {
                        switch (statusType)
                        {
                            case 0://HP�Α�
                                Pdata.MaxHp[Pdata.CharaIndex] *= 1.4f;
                                break;
                            case 1://MP�Α�
                                Pdata.MaxMp[Pdata.CharaIndex] *= 1.4f;
                                break;
                            case 2://MP�����񕜐Α�
                                Pdata.MpRegenerateSpeed[Pdata.CharaIndex] *= 1.4f;
                                break;


                        }
                        Player.GetComponent<PlayerBase>().money -= cost;//������
                        prayNum++;
                    }
                    else if (circleType == 1)
                    {
                        Pdata.HP[Pdata.CharaIndex] += 30;
                        Player.GetComponent<PlayerBase>().money -= cost;//������
                        //��
                        Instantiate(healParticle, Player.transform.position, transform.rotation);
                    }
                    else
                    {
                        Instantiate(secretBoss, transform.position, transform.rotation);
                        Player.GetComponent<PlayerBase>().money -= cost;//������
                        Destroy(gameObject);
                    }
                

                }
                
            }
        }
    }
}
