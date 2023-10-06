using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{
    public GameObject center;//���]���S�ƂȂ�I�u�W�F�N�g
    public float radius;//���a
    public float speed;//��]���x
    float t;
    public int shotPosCount;//���������ꏊ�̐�
    public int shotPosNum;//�ʂ��ԍ�
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
