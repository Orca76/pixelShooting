using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public int Number;//�R���|�[�l���g�̃i���o�[
    public bool Connect;//�܂��p�[�c�͑�����
    public GameObject NextComponent;
    public int NextBulletNumber;
    public int ComponentIndex;//���Ԗڂ̃R���|�[�l���g�ƂȂ��Ă��邩
    public int PlayerIndex;//���Ԗڂ̃v���C���[�̕������U���� 0,1,2,3

    public string bulletName;
    public float cost;
    public float damage;
    public string explain;
    public float speed;
    public float lifeTime;

    GameObject gunSystem;
    GunManager gunSc;
    GameObject Player;
    TextMeshProUGUI bulletNameTx;
    TextMeshProUGUI bulletExplainTx;
    TextMeshProUGUI bulletDamageTx;
    TextMeshProUGUI bulletCostTx;

    public bool Picked;//�E��ꂽ

    public float collisionDistance;

    public int Adjust = 0;//1=Mp������@2�U���㏸ 3���x�㏸ 4��] 5�ђ� 6�z�[�~���O�@7���b�N�I�� 8�g�� 9�����@10����

    public int componentType = 0;//0�e��1���� 2 �R�l�N�g�p�[�c


    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        gunSystem = GameObject.Find("GunSystem");
        gunSc = gunSystem.GetComponent<GunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Picked)
        {
            if (collisionDistance > Vector3.Distance(gameObject.transform.position, Player.transform.position))//�v���C���[�̋߂�
            {
                Player.GetComponent<PlayerBase>().dynamicGuide.text = "�q���E:Space";
                if (Input.GetKeyDown(KeyCode.Space))//�E��
                {
                    Player.GetComponent<PlayerBase>().dynamicGuide.text = "";
                    gameObject.transform.parent = GameObject.FindWithTag("backGround").transform;//�o�b�N�O���E���h�̎q�I�u�W�F�N�g�Ɏw��
                    if (componentType == 0)
                    {
                        gameObject.transform.position = transform.parent.position + new Vector3(0, 0, -0.1f);//�e�I�u�W�F�N�g�̈ʒu�Ɉړ�
                    }
                    else
                    {
                        gameObject.transform.position = transform.parent.position + new Vector3(-0.5f, 0, -0.1f);//�e�I�u�W�F�N�g�̈ʒu�Ɉړ�
                    }


                    Picked = true;
                }
            }
        }


        if (componentType == 0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameObject[] Slots = GameObject.FindGameObjectsWithTag("SkillSlot");
                GameObject SetSlot = ClosestComponent(Slots);
                //float dist = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x - SetSlot.transform.position.x, 2) +
                //   Mathf.Pow(gameObject.transform.position.y - SetSlot.transform.position.y, 2));

                float dist = Vector2.Distance(gameObject.transform.position, SetSlot.transform.position);
                if (collisionDistance > dist)//�e�X�g
                {

                    gameObject.transform.position = SetSlot.transform.position + new Vector3(0, 0, -0.1f);
                    ComponentIndex = SetSlot.GetComponent<SkillSlot>().SkillIndex;
                    PlayerIndex = SetSlot.GetComponent<SkillSlot>().PlayerNumber;
                }
            }
        }

    }


    void OnMouseDrag()
    {

        if (!gunSystem)
        {

            gunSystem = GameObject.Find("GunSystem");
            gunSc = gunSystem.GetComponent<GunManager>();
        }
        if (gunSc.EditOn)//�h���b�O�ړ��̓G�f�B�b�g���̂�
        {

            transform.position = GetMouseWorldPos();


            bulletNameTx = GameObject.Find("name").GetComponent<TextMeshProUGUI>();
            bulletCostTx = GameObject.Find("cost").GetComponent<TextMeshProUGUI>();
            bulletDamageTx = GameObject.Find("damage").GetComponent<TextMeshProUGUI>();
            bulletExplainTx = GameObject.Find("explain").GetComponent<TextMeshProUGUI>();

            bulletNameTx.text = bulletName;
            bulletCostTx.text = "MP:" + cost.ToString();
            bulletDamageTx.text = "Damage:" + damage.ToString();
            bulletExplainTx.text = explain;

        }

    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 a = Camera.main.ScreenToWorldPoint(mousePos);

        a.z = -0.1f;

        return a;
    }
    GameObject ClosestComponent(GameObject[] compos)
    {
        float closestDistance = Mathf.Infinity;
        GameObject target = null;
        foreach (GameObject card in compos)
        {
            float distanceTocomp = Vector3.Distance(transform.position, card.transform.position);
            if (distanceTocomp < closestDistance)
            {
                closestDistance = distanceTocomp;
                target = card;
            }
        }
        return target;
    }
}
