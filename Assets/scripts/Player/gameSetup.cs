using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSetup : MonoBehaviour
{
    public GameObject player;
    Vector3 playerCreatePos;
    public GameObject backGround;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            Instantiate(player, playerCreatePos, transform.rotation);

        }
        else
        {
            GameObject loadedPlayer = GameObject.FindWithTag("Player");
            loadedPlayer.transform.position = playerCreatePos;
        }
        if (GameObject.FindWithTag("backGround") == null)
        {
            Instantiate(backGround, playerCreatePos, transform.rotation);
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
