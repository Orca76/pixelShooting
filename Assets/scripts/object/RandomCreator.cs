using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCreator : MonoBehaviour
{
    // Start is called before the first frame update
    //�����_���ȃI�u�W�F�N�g�𐶐�����
    public GameObject[] objects;
    void Start()
    {
        Instantiate(objects[Random.Range(0, objects.Length)],transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
