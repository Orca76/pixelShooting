using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageText : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-20, 20), Random.Range(30, 60), 0));

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
