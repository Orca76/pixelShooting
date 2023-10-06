using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlot : MonoBehaviour
{
    public int PlayerNumber;//���Ԗڂ̃v���C���[��skill 1...
    public int SkillIndex;//���Ԗڂ̃X�L���Ȃ̂��@0...
    public GameObject NearCompo;//��ԋ߂��R���|
    public GameObject Equip;//�Z�b�g���ꂽ�R���|�[�l���g
    public int SkillNumber;

    GunManager GunManager_Script;
    public GameObject gunsystemobj;
    // Start is called before the first frame update
    void Start()
    {
        gunsystemobj = GameObject.Find("GunSystem");
        GunManager_Script = gunsystemobj.GetComponent<GunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent == false)
        {
            //  gameObject.transform.parent = GameObject.Find("backGround").transform;
        }
        //SkillDetect();
    }
    public void SkillDetect()
    {
        GameObject[] Components = GameObject.FindGameObjectsWithTag("Component");
        NearCompo = ClosestComponent(Components);

        
        if (Vector2.Distance(gameObject.transform.position, NearCompo.transform.position) < 0.05f)//�����ƃZ�b�g����Ă���
        {
            if (gunsystemobj == null)
            {
                gunsystemobj = GameObject.Find("GunSystem");
                GunManager_Script = gunsystemobj.GetComponent<GunManager>();
            }

            Equip = NearCompo;
            SkillNumber = Equip.GetComponent<BulletComponent>().Number;

            if (PlayerNumber == 0)
            {
                
                GunManager_Script.Components0[SkillIndex] = Equip;
            }
            else if (PlayerNumber == 1)
            {
                GunManager_Script.Components1[SkillIndex] = Equip;
            }
            else if (PlayerNumber == 2)
            {
                GunManager_Script.Components2[SkillIndex] = Equip;
            }
            else if (PlayerNumber == 3)
            {
                GunManager_Script.Components3[SkillIndex] = Equip;
            }

        }
        else
        {
            Equip = null;
            if (gunsystemobj == null)
            {
                gunsystemobj = GameObject.Find("GunSystem");
                GunManager_Script = gunsystemobj.GetComponent<GunManager>();
            }

            if (PlayerNumber == 0)
            {
               // Debug.Log("����0");
                GunManager_Script.Components0[SkillIndex] = null;
                
            }
            else if (PlayerNumber == 1)
            {

               
                //Debug.Log(gunsystemobj);

                GunManager_Script.Components1[SkillIndex] = null;
            }
            else if (PlayerNumber == 2)
            {
                //Debug.Log("����2");
                GunManager_Script.Components2[SkillIndex] = null;
            }
            else if (PlayerNumber == 3)
            {
                GunManager_Script.Components3[SkillIndex] = null;
            }
        }

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
