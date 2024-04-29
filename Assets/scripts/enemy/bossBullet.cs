using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet : MonoBehaviour
{
    //軌跡の弾丸


    GameObject player;
    LineRenderer line;
    Vector3 firstPos;
    Vector3 targetPos;
    public float offset;
    Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
       
        player = GameObject.FindWithTag("Player");
        float x = Random.Range(0, 100f);
        targetPos = player.transform.position + new Vector3(Mathf.Cos(x) * offset, Mathf.Sin(x) * offset);
        Vector3 baseVector = (targetPos - gameObject.transform.position);
        destination = baseVector * 4 + gameObject.transform.position;
        firstPos = gameObject.transform.position ;

        // LineRendererの位置を設定する
        Vector3[] positions = new Vector3[2];
        positions[0] = firstPos; // 開始位置
        positions[1] = destination; // 終了位置
        line.positionCount = positions.Length; // ポジションの数を設定
        line.SetPositions(positions);


       
        // 対象物へのベクトルを算出
        Vector3 toDirection = destination - transform.position;
        // 対象物へ回転する
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
    }

    // Update is called once per frame
    void Update()
    {


    }


}
