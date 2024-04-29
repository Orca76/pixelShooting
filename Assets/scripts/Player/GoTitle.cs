using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTitle : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (player)
        {
            if (player.GetComponent<PlayerBase>().isGameOver)//�Q�[���I�[�o�[��
            {
               
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // SceneManager.LoadScene("Title");//���Ŕ�񂾂�v���C���[���^�C�g���ɍs���Ă��܂�
                    player.GetComponent<PlayerBase>().playercamera.transform.parent = null;
                    player.GetComponent<PlayerBase>().gameOverCanvas.transform.parent = null;
                    Destroy(player);
                    Destroy(player.GetComponent<PlayerBase>().gameOverCanvas);
                    SceneManager.LoadScene("Title");
                }
            }
        }
    }
}
