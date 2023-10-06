using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class stairsBase : MonoBehaviour
{
    GameObject player;
    public float dist;
    GameObject backGround;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        backGround = GameObject.FindWithTag("backGround");
       
    }

    // Update is called once per frame
    void Update()
    {
        if (dist > Vector2.Distance(gameObject.transform.position, player.transform.position))
        {
            player.GetComponent<PlayerBase>().dynamicGuide.text = "ÉIÉäÉã:Space";
            if (Input.GetKeyDown(KeyCode.Space))//Ç®ÇËÇÈ
            {
                //éüÇÃÉtÉçÉAÇ÷
                player.GetComponent<PlayerBase>().dynamicGuide.text = "";
                DontDestroyOnLoad(player);
                DontDestroyOnLoad(backGround);
                
                PlayerBase Sc = player.GetComponent<PlayerBase>();
                Sc.currentFloor++;
                GunManager gunSc = GameObject.Find("GunSystem").GetComponent<GunManager>();

                gunSc.nameText.SetActive(true);
                gunSc.mpText.SetActive(true);
                gunSc.damageText.SetActive(true);
                gunSc.explainText.SetActive(true);

                Sc.existCopy = gunSc.PlayerExistObj;

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
