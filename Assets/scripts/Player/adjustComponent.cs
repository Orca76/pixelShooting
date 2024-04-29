using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public int adjustNumber;
    public bool unUsed = true;//使われていない

    float dist;
   public GameObject target;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        
        dist = Vector2.Distance(gameObject.transform.position, ClosestComponent(GameObject.FindGameObjectsWithTag("Component")).transform.position);
        if (dist < 0.05f)//接触した時
        {
            if (unUsed)//まだこの調整パーツはどのアイテムにもくっついていない
            {
                if (!target)
                {
                    target = ClosestComponent(GameObject.FindGameObjectsWithTag("Component"));
                }
                BulletComponent sc = target.GetComponent<BulletComponent>();//接触したコンポーネントの情報を取得
                if (sc.componentType == 0)//通常の弾丸である（赤いパーツではない）
                {
                    if (sc.Adjust == 0)//まだ何もくっついてない
                    {
                        Debug.Log("何もくっついていない");
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

        if (!unUsed)//変な位置でくっついてしまった時の修正
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
