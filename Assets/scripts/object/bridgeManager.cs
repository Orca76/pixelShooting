using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] bridgeGate;//���̃Q�[�g�@�G�Ɛ���Ă���Ԃ͕܂�
    void Start()
    {
        foreach (GameObject obj in bridgeGate)
        {
            obj.SetActive(false);//�퓬���ł͂Ȃ��Ƃ��͔�A�N�e�B�u�ɐݒ肷��
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("enemy").Length >= 1)
        {
            foreach (GameObject obj in bridgeGate)
            {
                obj.SetActive(true);//�G�Ƃ̐퓬���͕�������o���Ȃ��悤�ɂ���
            }
        }
        else
        {
            foreach (GameObject obj in bridgeGate)
            {
                obj.SetActive(false);//�퓬���ł͂Ȃ��Ƃ��͔�A�N�e�B�u�ɐݒ肷��
            }

        }
    }
}
