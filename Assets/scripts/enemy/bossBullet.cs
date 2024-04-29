using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet : MonoBehaviour
{
    //�O�Ղ̒e��


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

        // LineRenderer�̈ʒu��ݒ肷��
        Vector3[] positions = new Vector3[2];
        positions[0] = firstPos; // �J�n�ʒu
        positions[1] = destination; // �I���ʒu
        line.positionCount = positions.Length; // �|�W�V�����̐���ݒ�
        line.SetPositions(positions);


       
        // �Ώە��ւ̃x�N�g�����Z�o
        Vector3 toDirection = destination - transform.position;
        // �Ώە��։�]����
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
    }

    // Update is called once per frame
    void Update()
    {


    }


}
