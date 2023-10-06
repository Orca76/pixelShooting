using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTrigger : MonoBehaviour
{
    public GameObject obj;
    public Vector3 CreatePos;
    public bool Created;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Created == false)
            {
                Instantiate(obj, CreatePos, transform.rotation);
                Created = true;
            }
           
        }
    }
}
