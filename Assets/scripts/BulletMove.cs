using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
   public float speed;
  public  float LifeTime;
    Rigidbody2D rig;
   public float velocityChangeRate = 0;
    float t;
    float firstSpeed;
    BulletComponent script;
    public float rotateSpeed=1;
    public float homingSpeed = 10;
    float expandValue;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        if (gameObject.GetComponent<BulletBase>().RelatedComponent)
        {
            script = gameObject.GetComponent<BulletBase>().RelatedComponent.GetComponent<BulletComponent>();
           // Debug.Log("script=" + script);
            LifeTime = script.lifeTime;
            if (script.Adjust == 3)//���x�㏸
            {
                speed *= 2;
            }
            else if (script.Adjust == 7)//���b�N�I��
            {
                if (GameObject.FindGameObjectsWithTag("enemy").Length > 0)
                {
                    GameObject target = ClosestComponent(GameObject.FindGameObjectsWithTag("enemy"));
                    // �Ώە��ւ̃x�N�g�����Z�o

                    Vector3 toDirection = target.transform.position - transform.position;
                    // �Ώە��։�]����
                    transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
                }
            }
            else if (script.Adjust == 9)//����
            {
                velocityChangeRate = 1;
            }
            else if (script.Adjust == 10)//����
            {
                velocityChangeRate = -1;
            }
            firstSpeed = speed;
        }
        Destroy(gameObject, LifeTime);
    }

    // Update is called once per frame
    void Update()
    {

        t += Time.deltaTime;
        //Debug.Log("Speed=" + speed + " velocitychange=" + velocityChangeRate + " firstspeed=" + firstSpeed);
        speed = Mathf.Exp(t * velocityChangeRate) * firstSpeed;
        rig.velocity = transform.up * speed;

        if (script.Adjust == 4)//��]
        {
            transform.Rotate(0, 0, rotateSpeed);
        }else if (script.Adjust == 6)//�z�[�~���O
        {
            if (GameObject.FindGameObjectsWithTag("enemy").Length > 0)
            {
                GameObject target = ClosestComponent(GameObject.FindGameObjectsWithTag("enemy"));
                // �Ώە��ւ̃x�N�g�����Z�o

                //// �Ώە��ւ̃x�N�g�����Z�o
                Vector3 toDirection = target.transform.position - transform.position;

                // �x�N�g������p�x���v�Z
                float angle = Mathf.Atan2(toDirection.y, toDirection.x) * Mathf.Rad2Deg;

                // �v���C���[���������ƃ^�[�Q�b�g�̕����Ɍ�����
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, homingSpeed * Time.deltaTime*t);

            }
        }
        else if (script.Adjust == 8)
        {
            expandValue += Time.deltaTime;
            gameObject.transform.localScale=new Vector3(expandValue*10, expandValue*10, 1);
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
