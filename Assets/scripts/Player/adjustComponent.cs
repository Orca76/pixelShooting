using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public int adjustNumber;
    public bool unUsed = true;//Žg‚í‚ê‚Ä‚¢‚È‚¢

    float dist;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = ClosestComponent(GameObject.FindGameObjectsWithTag("Component"));
        dist = Vector2.Distance(gameObject.transform.position, target.transform.position);
        if (dist < 0.05f)
        {
            if (unUsed)
            {
                BulletComponent sc = target.GetComponent<BulletComponent>();
                if (sc.componentType == 0)
                {
                    if (sc.Adjust == 0)//‚Ü‚¾‰½‚à‚­‚Á‚Â‚¢‚Ä‚È‚¢
                    {
                        sc.Adjust = adjustNumber;
                        gameObject.transform.position = target.gameObject.transform.position + new Vector3(0, -0.08f, 0);
                        gameObject.transform.parent = target.transform;
                    }
                   
                }
                unUsed = false;
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
