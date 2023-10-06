using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class charaCreator : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] charas;
    GameObject system;
    public int nowCharaCount;
    public GameObject chest;
    void Start()
    {
        system = GameObject.Find("GunSystem");
        charas = system.GetComponent<DataBase>().charas;
        nowCharaCount = system.GetComponent<GunManager>().PlayerExistObj.Count(item => item);//trueÇÃêî
        if (nowCharaCount < 4)
        {
            
            Instantiate(charas[Random.Range(0, charas.Length)], transform.position, transform.rotation);
        }
        else
        {
            Instantiate(chest, transform.position, transform.rotation);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
