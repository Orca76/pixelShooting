using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockOn : MonoBehaviour
{
    public GameObject Player;
    public float PlayerScale;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        // ‘ÎÛ•¨‚Ö‚ÌƒxƒNƒgƒ‹‚ðŽZo
        Vector3 toDirection = GetMouseWorldPos() - transform.position;
        // ‘ÎÛ•¨‚Ö‰ñ“]‚·‚é
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);

        if (toDirection.x > 0.05f)
        {
            Player.transform.localScale = new Vector3(-PlayerScale, PlayerScale, 1) ;
        }else if (toDirection.x < -0.05f)
        {
            Player.transform.localScale = new Vector3(PlayerScale, PlayerScale, 1);
        }
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
