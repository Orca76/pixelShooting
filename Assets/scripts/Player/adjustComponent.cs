using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public int adjustNumber;
    public bool unUsed = true;//�g���Ă��Ȃ�

    float dist;
   public GameObject target;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        
        dist = Vector2.Distance(gameObject.transform.position, ClosestComponent(GameObject.FindGameObjectsWithTag("Component")).transform.position);
        if (dist < 0.05f)//�ڐG������
        {
            if (unUsed)//�܂����̒����p�[�c�͂ǂ̃A�C�e���ɂ��������Ă��Ȃ�
            {
                if (!target)
                {
                    target = ClosestComponent(GameObject.FindGameObjectsWithTag("Component"));
                }
                BulletComponent sc = target.GetComponent<BulletComponent>();//�ڐG�����R���|�[�l���g�̏����擾
                if (sc.componentType == 0)//�ʏ�̒e�ۂł���i�Ԃ��p�[�c�ł͂Ȃ��j
                {
                    if (sc.Adjust == 0)//�܂������������ĂȂ�
                    {
                        Debug.Log("�����������Ă��Ȃ�");
                        sc.Adjust = adjustNumber;
                        gameObject.transform.position = target.gameObject.transform.position + new Vector3(0, -0.08f, 0);
                        gameObject.transform.parent = target.transform;
                        unUsed = false;
                    }
                   
                }
               
            }
        }
        else
        {
            if (unUsed)
            {
                target = null;
            }
           // 
        }

        if (!unUsed)//�ςȈʒu�ł������Ă��܂������̏C��
        {
            if (gameObject.transform.position != target.gameObject.transform.position + new Vector3(0, -0.08f, 0))
            {
                gameObject.transform.position = target.gameObject.transform.position + new Vector3(0, -0.08f, 0);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (unUsed)
        //{
        //    if (collision.CompareTag("Component"))
        //    {
        //        Debug.Log("COLLISIONTAG=" + collision.tag);
        //        BulletComponent sc = collision.GetComponent<BulletComponent>();
        //        if (sc.componentType == 0)
        //        {
        //            gameObject.transform.position = collision.gameObject.transform.position + new Vector3(0, -0.08f, 0);
        //            gameObject.transform.parent = collision.transform;
        //        }


        //    }
        //    unUsed = false;
        //}

    }
}
