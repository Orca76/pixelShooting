using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossDefeat : MonoBehaviour
{
    // Start is called before the first frame update
    //�{�X���j���N���A���̃X�N���v�g�@�{�X�̃X�N���v�g�͒ʏ�G�̂��̂𗬗p���Ă��邽��
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
