using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DynamicScript : MonoBehaviour
{
    public TextMeshProUGUI dynamic;
     float floorFadeoutSpeed=0.5f;
    // Start is called before the first frame update
    //�`���[�g���A���e�L�X�g�����Ԍo�߂Ńt�F�[�h�A�E�g������X�N���v�g
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dynamic.alpha > 0)//�����łȂ�
        {

            dynamic.alpha -= floorFadeoutSpeed * Time.deltaTime;
           // Debug.Log("alpha--");
            //Destroy(floorText);
        }
    }
}
