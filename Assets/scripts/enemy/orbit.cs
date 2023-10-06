using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{
    public GameObject center;//公転中心となるオブジェクト
    public float radius;//半径
    public float speed;//回転速度
    float t;
    public int shotPosCount;//撃ちだす場所の数
    public int shotPosNum;//通し番号
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        float offset = (2 * Mathf.PI / shotPosCount) * shotPosNum;
        center = ClosestComponent(GameObject.FindGameObjectsWithTag("enemy"));
        gameObject.transform.position = center.transform.position + new Vector3(Mathf.Cos(t * speed+offset)*radius, Mathf.Sin(t * speed+offset)*radius, 0);

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
