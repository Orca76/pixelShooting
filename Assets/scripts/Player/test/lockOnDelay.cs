using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockOnDelay : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //// 対象物へのベクトルを算出
        Vector3 toDirection = target.transform.position - transform.position;

        // ベクトルから角度を計算
        float angle = Mathf.Atan2(toDirection.y, toDirection.x) * Mathf.Rad2Deg;

        // プレイヤーをゆっくりとターゲットの方向に向ける
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
