using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDrone : MonoBehaviour
{
    enemyBase sc;
    GameObject player;
    public GameObject droneLastShot;//�j�󎞂ɕ������e��
    public bool laserOn;//���[�U�[��ł��ǂ���
    public GameObject[] bullet;
    public float[] span;//�U���Ԋu
    float t;
    public GameObject turret;//���ˌ�

    public int number;//�h���[���U���p�^�[��

    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<enemyBase>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (sc.hp <= 0)
        {
            Debug.Log("chargeShot");
            player = GameObject.FindWithTag("Player");
            Vector2 shotVector = (gameObject.transform.position - player.transform.position).normalized;
            float deg = Mathf.Atan2(shotVector.x, shotVector.y) * Mathf.Rad2Deg;

           
            Instantiate(droneLastShot, gameObject.transform.position, Quaternion.Euler(0, 0, -deg));
            Destroy(gameObject);
        }
        t += Time.deltaTime;
        switch (number)
        {
            case 0://�����A��
                if (t > span[0])
                {
                    Instantiate(bullet[0], transform.position, turret.transform.rotation);
                    t = 0;
                }
                break;
            case 1://���ԍ����b�N�I��
                if (t > span[1])
                {
                    Instantiate(bullet[1], transform.position, turret.transform.rotation);
                    t = 0;
                }
                break;
            case 2://�������]����
                if (t > span[2])
                {
                    Instantiate(bullet[2], transform.position, turret.transform.rotation);
                    t = 0;
                }
                break;

        }
        if (laserOn)//�����P�ɒe����������t�F�[�Y
        {
            
           
        }
        else
        {

        }
    }
}
