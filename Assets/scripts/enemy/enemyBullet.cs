using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    // public float speed;
    public float Damage;
    public bool penetrate;//�ђʂ��邩
    public float speed;
    public float lifetime;
    Rigidbody2D rig;

    public float delay;
    public bool normalShot = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedStart()); // �ҋ@�������܂�StartCoroutine���Ăяo��
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(delay); // delay�b�ҋ@
        Destroy(gameObject, lifetime); // lifetime�b���GameObject��j��
        rig = GetComponent<Rigidbody2D>();
        if (normalShot)
        {
            if (gameObject.transform.parent)
            {
                gameObject.transform.parent = null;
            }
        }
     
    }


    // Update is called once per frame
    void Update()
    {
        rig.velocity = transform.up * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))//�v���C���[�ɐڐG��������HP�����炷����
        {
            collision.gameObject.GetComponent<PlayerBase>().HP[collision.GetComponent<PlayerBase>().CharaIndex] -= Damage;
            if (!penetrate)
            {
                Destroy(gameObject);
            }

        }
    }
}
