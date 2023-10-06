using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    GameObject player;
    float dist;
    public float distanceLimit;
    float moveSpeed;
    Vector3 direction;
    Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rig = GetComponent<Rigidbody2D>();
        moveSpeed = gameObject.GetComponent<enemyBase>().speed;
        
    }

    // Update is called once per frame
    void Update()
    {

        dist = Vector2.Distance(gameObject.transform.position, player.transform.position);
       // Debug.Log("kyori=" + dist);
        if (dist > distanceLimit)//“G‚ªƒvƒŒƒCƒ„[‚©‚çˆê’è‚Ì‹——£—£‚ê‚Ä‚¢‚é
        {
            direction = (player.transform.position - gameObject.transform.position).normalized;
            rig.velocity = direction * moveSpeed;
        }
        else
        {
            rig.velocity =Vector2.zero;
        }
        
       

        
    }
}
