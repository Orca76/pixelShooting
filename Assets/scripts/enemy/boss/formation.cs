using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class formation : MonoBehaviour
{
    public GameObject[] drones;
    public GameObject[] droneturrets;

    float t;

    //public float distance1;//パターン１　回転半径
    //public float rotationSpeed;//パターン1回転速度
    //public float rotateS2;
    //public float rotateS3;//パターン3回転速度　
    //public float distance3;//パターン3　プレイヤーからの距離
    //public float radius3;//回転半径3

    public float[] rotateSpeed;//回転速度
    public float[] radius;//回転半径
    public Vector3[] positionVec;//基準点からの位置ベクトル

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // puttern1();
        //puttern2();
        pattern3();
    }
    public void pattern1()
    {
        t = Time.time;
        if (drones[0])
        {
            drones[0].transform.position = gameObject.transform.position + new Vector3(Mathf.Cos(t * rotateSpeed[0]), Mathf.Sin(t * rotateSpeed[0]), 0) * radius[0];
            droneturrets[0].transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2((gameObject.transform.position - drones[0].transform.position).normalized.x,
          (gameObject.transform.position - drones[0].transform.position).normalized.y) * Mathf.Rad2Deg + 180f);
        }
        if (drones[1])
        {
            drones[1].transform.position = gameObject.transform.position +
           new Vector3(Mathf.Cos(t * rotateSpeed[0] + 90f * Mathf.Deg2Rad), Mathf.Sin(t * rotateSpeed[0] + 90f * Mathf.Deg2Rad), 0) * radius[0];
            droneturrets[1].transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2((gameObject.transform.position - drones[1].transform.position).normalized.x,
         (gameObject.transform.position - drones[1].transform.position).normalized.y) * Mathf.Rad2Deg + 180f);

        }
        if (drones[2])
        {
            drones[2].transform.position = gameObject.transform.position +
          new Vector3(Mathf.Cos(t * rotateSpeed[0] + 180f * Mathf.Deg2Rad), Mathf.Sin(t * rotateSpeed[0] + 180f * Mathf.Deg2Rad), 0) * radius[0];
            droneturrets[2].transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2((gameObject.transform.position - drones[2].transform.position).normalized.x,
         (gameObject.transform.position - drones[2].transform.position).normalized.y) * Mathf.Rad2Deg + 180f);
        }
        if (drones[3])
        {
            drones[3].transform.position = gameObject.transform.position +
        new Vector3(Mathf.Cos(t * rotateSpeed[0] + 270f * Mathf.Deg2Rad), Mathf.Sin(t * rotateSpeed[0] + 270f * Mathf.Deg2Rad), 0) * radius[0];
            droneturrets[3].transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2((gameObject.transform.position - drones[3].transform.position).normalized.x,
          (gameObject.transform.position - drones[3].transform.position).normalized.y) * Mathf.Rad2Deg + 180f);
        }



    }
    public void pattern2()
    {
        t = Time.time;
        if (drones[0])
        {
            drones[0].transform.position = gameObject.transform.position + new Vector3(Mathf.Cos(t * rotateSpeed[1]), Mathf.Sin(t * rotateSpeed[1]), 0) * radius[1];
            droneturrets[0].transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2((gameObject.transform.position - drones[0].transform.position).normalized.x,
          (gameObject.transform.position - drones[0].transform.position).normalized.y) * Mathf.Rad2Deg + 180f);
        }
        if (drones[1])
        {
            drones[1].transform.position = gameObject.transform.position +
           new Vector3(Mathf.Cos(t * rotateSpeed[1]), Mathf.Sin(t * rotateSpeed[1]), 0) * (radius[1] + 0.2f);
            droneturrets[1].transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2((gameObject.transform.position - drones[1].transform.position).normalized.x,
         (gameObject.transform.position - drones[1].transform.position).normalized.y) * Mathf.Rad2Deg + 180f);

        }
        if (drones[2])
        {
            drones[2].transform.position = gameObject.transform.position +
          new Vector3(Mathf.Cos(t * rotateSpeed[1]), Mathf.Sin(t * rotateSpeed[1]), 0) *(radius[1] + 0.4f);
            droneturrets[2].transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2((gameObject.transform.position - drones[2].transform.position).normalized.x,
         (gameObject.transform.position - drones[2].transform.position).normalized.y) * Mathf.Rad2Deg + 180f);
        }
        if (drones[3])
        {
            drones[3].transform.position = gameObject.transform.position +
        new Vector3(Mathf.Cos(t * rotateSpeed[1]), Mathf.Sin(t * rotateSpeed[1]), 0) * (radius[1] + 0.6f);
            droneturrets[3].transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2((gameObject.transform.position - drones[3].transform.position).normalized.x,
          (gameObject.transform.position - drones[3].transform.position).normalized.y) * Mathf.Rad2Deg + 180f);
        }
    }

    public void pattern3()
    {
        t = Time.time;
        if (drones[0])
        {
            drones[0].transform.position = 
                player.transform.position+positionVec[2] + new Vector3(Mathf.Cos(t * rotateSpeed[2]), 2*Mathf.Sin(t * rotateSpeed[2]), 0) * radius[2];
            droneturrets[0].transform.rotation = Quaternion.Euler(0, 0,90);
        }
        if (drones[1])
        {
            drones[1].transform.position = player.transform.position + positionVec[2] +
                new Vector3(Mathf.Cos(t * rotateSpeed[2]+90f*Mathf.Deg2Rad), 2 * Mathf.Sin(t * rotateSpeed[2] + 90f * Mathf.Deg2Rad), 0) * radius[2];
            droneturrets[1].transform.rotation = Quaternion.Euler(0, 0, 90);

        }
        if (drones[2])
        {
            drones[2].transform.position = player.transform.position + positionVec[2] +
                new Vector3(Mathf.Cos(t * rotateSpeed[2] + 180f * Mathf.Deg2Rad), 2 * Mathf.Sin(t * rotateSpeed[2] + 180f * Mathf.Deg2Rad), 0) * radius[2];
            droneturrets[2].transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (drones[3])
        {
            drones[3].transform.position = player.transform.position + positionVec[2] +
                new Vector3(Mathf.Cos(t * rotateSpeed[2] + 270f * Mathf.Deg2Rad), 2 * Mathf.Sin(t * rotateSpeed[2] + 270f * Mathf.Deg2Rad), 0) * radius[2];
            droneturrets[3].transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    void pattern4()
    {

    }
}
