using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class manualBtn : MonoBehaviour
{
    public GameObject tutorial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void tutorialBtn()
    {
        tutorial.SetActive(true);
    }
}
