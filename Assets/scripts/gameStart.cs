using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameStart : MonoBehaviour
{
    // Start is called before the first frame update
    //public int istutorialClear;//�`���[�g���A�����N���A���Ă��邩
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TestScene");
        }
    }
}
