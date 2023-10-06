using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcreaterooms : MonoBehaviour
{
    public GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.Log("testcreate");
        if (Input.GetMouseButtonDown(0)) // 0ÇÕç∂ÉNÉäÉbÉN
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(tapPoint, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject tappedObject = hit.transform.gameObject;
                string objectName = tappedObject.name;
                Debug.Log("Tapped Object Name: " + objectName);
            }
        }
    }
}
