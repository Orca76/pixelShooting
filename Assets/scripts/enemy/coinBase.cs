using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBase : MonoBehaviour
{
    public int value;
    GameObject player;
    float dist;
    public float movingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (dist < 0.2f)//ˆê’è”ÍˆÍ‚É“ü‚Á‚½‚ç
        {
            Vector3 toPlayer = -gameObject.transform.position + player.transform.position;
            gameObject.transform.Translate(toPlayer * movingSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBase>().money += value;
            //‚±‚±‚Å‚¨‹à‚ÌSE
            Destroy(gameObject);
        }
    }
}
