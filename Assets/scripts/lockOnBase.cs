using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockOnBase : MonoBehaviour
{
    public bool playerShot;
    public bool firstLockOn;
    public bool Homing;
    // Start is called before the first frame update
    void Start()
    {
        if (firstLockOn)//�ŏ��Ƀ��b�N�I��
        {
            if (playerShot)//�v���C���[�̒e������G�����b�N�I��
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
            else
            {
                GameObject target = GameObject.FindWithTag("Player");
                // �Ώە��ւ̃x�N�g�����Z�o
                Vector3 toDirection = target.transform.position - transform.position;
                // �Ώە��։�]����
                transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
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

    // Update is called once per frame
    void Update()
    {
        if (playerShot)
        {
            if (Homing)
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
        }
        else
        {
            if (Homing)
            {
                GameObject target = GameObject.FindWithTag("Player");
                // �Ώە��ւ̃x�N�g�����Z�o
                Vector3 toDirection = target.transform.position - transform.position;
                // �Ώە��։�]����
                transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
            }
           
        }
    }
}
