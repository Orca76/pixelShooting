using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] attacks;
    public float span = 6f;
    float t;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > span)
        {
            int x = Random.Range(0, attacks.Length);
            Debug.Log("ç°âÒÇÕx=" + x);
            GameObject Magic = Instantiate(attacks[x], transform.position, transform.rotation);
            Debug.Log(Magic.name);
            Magic.transform.position = gameObject.transform.position;
            Magic.transform.parent = gameObject.transform;
            Debug.Log(Magic.transform.parent.name);
            t = 0;
        }
    }
}
