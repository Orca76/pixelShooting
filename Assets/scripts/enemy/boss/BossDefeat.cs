using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossDefeat : MonoBehaviour
{
    // Start is called before the first frame update
    //ボス撃破→クリア時のスクリプト　ボスのスクリプトは通常敵のものを流用しているため
    public GameObject boss;
    public GameObject ClearCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!boss)
        {


            //ClearCanvas.SetActive(true);
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    SceneManager.LoadScene("Title");
            //}
        }
    }
}
