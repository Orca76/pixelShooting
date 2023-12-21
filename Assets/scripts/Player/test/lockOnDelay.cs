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
        //// �Ώە��ւ̃x�N�g�����Z�o
        Vector3 toDirection = target.transform.position - transform.position;

        // �x�N�g������p�x���v�Z
        float angle = Mathf.Atan2(toDirection.y, toDirection.x) * Mathf.Rad2Deg;

        // �v���C���[���������ƃ^�[�Q�b�g�̕����Ɍ�����
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
