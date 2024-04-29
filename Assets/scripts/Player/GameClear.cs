using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public RawImage backGround;
    Color backColor;
    float alpha;
    public float fadeSpeed;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        backColor = backGround.color;
        alpha += fadeSpeed * Time.deltaTime;
        backGround.color = new Color(backColor.r, backColor.g, backColor.b, alpha);




        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerBase>().playercamera.transform.parent = null;//ƒJƒƒ‰‚ÌeqŠÖŒW‰ğœ‚µ‰ğ‚­
            Destroy(player);
            SceneManager.LoadScene("Title");

        }
    }
}
