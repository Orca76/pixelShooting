using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public GameObject damageText;
    GameObject damageCanvas;
    public bool normalShot = true;//�e�I�u�W�F�N�g�����݂���ꍇ�͊֌W��؂�@�ʏ�̒e�ۂƂ��Ă̋�����

    float t;
    public bool specialBullet = false;//����e�ہ@�G�̍U���𗘗p����ꍇ���G�Ƀ_���[�W���ʂ�
    // Start is called before the first frame update
    void Start()
    {
        damageCanvas = GameObject.Find("damageCanvas");
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
        //  t += Time.deltaTime;
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        if (delay <= 0)
        {
            rig.velocity = transform.up * speed;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))//�v���C���[�ɐڐG��������HP�����炷����
        {
            if (!specialBullet)
            {
                collision.gameObject.GetComponent<PlayerBase>().HP[collision.GetComponent<PlayerBase>().CharaIndex] -= Damage;



                GameObject DamageUI = Instantiate(damageText, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                DamageUI.transform.SetParent(damageCanvas.transform);

                DamageUI.GetComponent<TextMeshProUGUI>().color = Color.red;

                DamageUI.GetComponent<TextMeshProUGUI>().text = Damage.ToString();
                //

              //  collision.gameObject.GetComponent<PlayerBase>().HP[collision.gameObject.GetComponent<PlayerBase>().CharaIndex] -= Damage;//hp�����炷



                if (!penetrate)
                {
                    Destroy(gameObject);
                }

            }

        }
        if (collision.CompareTag("enemy"))
        {
            if (specialBullet)
            {
                collision.gameObject.GetComponent<enemyBase>().hp -= Damage;
                //if (!penetrate)
                //{
                //    Destroy(gameObject);//�ђʂ���e�ۂł��邽�ߎ��Ԍo�߂ł̂ݏ���
                //}
            }
        }
    }
}
