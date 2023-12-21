using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainAttack : MonoBehaviour
{
    GameObject targetEnemy;
    GameObject lastHitEnemy;
    public float chainRange;//�`���ő勗��
    public bool noEnemy;//�t�B�[���h�ɓG����̈ȉ��Ń`�F�C���������s��
    Rigidbody2D rig;

   public float speed;

    BulletComponent script;

    // Start is called before the first frame update
    void Start()
    {
        targetEnemy = ClosestComponent(GameObject.FindGameObjectsWithTag("enemy"));
        rig.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //���i����
        rig.velocity = transform.up*speed;//���i

        //�^�[�Q�b�g�ւ̉�]����
        // �Ώە��ւ̃x�N�g�����Z�o

        Vector3 toDirection = targetEnemy.transform.position - transform.position;
        // �Ώە��։�]����
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //�_���[�W����
            if (noEnemy)
            {
                Destroy(gameObject);
            }
            lastHitEnemy = collision.gameObject;
            targetEnemy = RandomSelect(GameObject.FindGameObjectsWithTag("enemy"),lastHitEnemy);
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

    GameObject RandomSelect(GameObject[] array, GameObject exception)
    {
        GameObject randomEnemy=null;
        if (array.Length > 1)
        {
            

            while (true)
            {
                randomEnemy = array[Random.Range(0, array.Length)];
                if (randomEnemy != exception)//�����G�ł͂Ȃ�
                {
                    if (Vector3.Distance(randomEnemy.transform.position, gameObject.transform.position) < chainRange)//�^�[�Q�b�g���������Ȃ�
                    {

                        break;
                    }
                }

            }


        }
        return randomEnemy;
    }
}
