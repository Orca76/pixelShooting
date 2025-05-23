using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockOn : MonoBehaviour
{
    public GameObject Player;
    public float PlayerScale;
    public GameObject system;
    GunManager gunManager;
    // Start is called before the first frame update

    void Start()
    {
        system = GameObject.Find("GunSystem");
        gunManager = system.GetComponent<GunManager>();
    }

    // Update is called once per frame
    void Update()
    {


        if (!gunManager.EditOn)
        {
          //  ;/ Time.timeScale= 1.0f;
            // 対象物へのベクトルを算出
            Vector3 toDirection = GetMouseWorldPos() - transform.position;
            // 対象物へ回転する
            transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);

            if (toDirection.x > 0.05f)
            {
                Player.transform.localScale = new Vector3(-PlayerScale, PlayerScale, 1);
            }
            else if (toDirection.x < -0.05f)
            {
                Player.transform.localScale = new Vector3(PlayerScale, PlayerScale, 1);
            }
        }
        else
        {
           //Time.timeScale = 0;   
        }
          
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
