using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dustshoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Time.timeScale‚ª0‚Å‚à•¨—‰‰Z‚ğè“®‚Åi‚ß‚é
        if (Time.timeScale == 0)
        {
            Physics.Simulate(Time.unscaledDeltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Component"))
        {
            Destroy(collision.gameObject);
        }
        //red
        var judge=collision.GetComponent<BulletComponent>();
        Debug.Log(judge);
        if(judge)
        {
            Destroy(collision.gameObject);
        }
    }
}
