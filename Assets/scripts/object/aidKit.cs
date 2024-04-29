using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aidKit : MonoBehaviour
{

    float dist = 0.1f;
    GameObject player;
    public float HealValue = 50f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (dist > Vector2.Distance(gameObject.transform.position, player.transform.position))//アイテムの近くにいる
        {
            player.GetComponent<PlayerBase>().dynamicGuide.text = "ツカウ:Space";
            player.GetComponent<PlayerBase>().dynamicGuide.alpha = 1;
            if (Input.GetKeyDown(KeyCode.Space))
            {

        
                player.GetComponent<PlayerBase>().dynamicGuide.text = "";


                PlayerBase pData = player.GetComponent<PlayerBase>();

               
                player.GetComponent<PlayerBase>().HP[player.GetComponent<PlayerBase>().CharaIndex] += HealValue;


                Destroy(gameObject);

            }
        }
    }
}
