using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    // Start is called before the first frame update
    //minimap�̏����@���j���[�J�������ɂ͔�\��
    public GameObject gunsSystem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gunsSystem.GetComponent<GunManager>().EditOn)//�G�f�B�b�g��
        {

        }
    }
}
