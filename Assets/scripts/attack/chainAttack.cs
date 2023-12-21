using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainAttack : MonoBehaviour
{
    GameObject targetEnemy;
    GameObject lastHitEnemy;
    public float chainRange;//伝う最大距離
    public bool noEnemy;//フィールドに敵が一体以下でチェインが発動不可
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
        //直進処理
        rig.velocity = transform.up*speed;//直進

        //ターゲットへの回転処理
        // 対象物へのベクトルを算出

        Vector3 toDirection = targetEnemy.transform.position - transform.position;
        // 対象物へ回転する
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //ダメージ処理
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
                if (randomEnemy != exception)//同じ敵ではない
                {
                    if (Vector3.Distance(randomEnemy.transform.position, gameObject.transform.position) < chainRange)//ターゲットが遠すぎない
                    {

                        break;
                    }
                }

            }


        }
        return randomEnemy;
    }
}
