using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanceCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lance;
    GameObject player;
    public float span;
    float t;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > span)
        {
            float randomRotation = Random.Range(0f, 360f);
            Quaternion randomQuaternion = Quaternion.Euler(0f, 0f, randomRotation);
            Instantiate(lance, player.transform.position,randomQuaternion);
            t = 0;
        }
    }
}
