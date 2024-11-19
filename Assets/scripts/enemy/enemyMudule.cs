using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMudule : MonoBehaviour
{
    public GameObject bullet;
    public int count;
    public float span;
    float t;
    GameObject enemy;
    public bool directShot;//自身から直接撃ちだす　

    public float interval;

    int x;
    float t_interval;
    // Start is called before the first frame update
    void Start()
    {
        enemy = ClosestComponent(GameObject.FindGameObjectsWithTag("enemy"));
      


    }
   

  
    // Update is called once per frame
    void Update()
    {
        
        if (directShot)
        {
            if (enemy)
            {
                gameObject.transform.position = enemy.transform.position;
            }
            else
            {
                Destroy(gameObject);
            }
          
        }
        t_interval += Time.deltaTime;
        if (t_interval > interval)
        {
            x++;
            t_interval = 0;
        }
        if (x % 2 == 0)
        {
            SpawnBullet();
        }


    }
    void SpawnBullet()
    {
        Debug.Log("呼ばれてます");
        if (0 < count)
        {
            t += Time.deltaTime;
            if (t > span)
            {
                Instantiate(bullet, transform.position, transform.rotation);
                count--;
                t = 0;
            }
           
        }
        else
        {
           
            Destroy(gameObject); // このスクリプトがアタッチされたGameObjectを破壊する（任意）
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
