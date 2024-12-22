using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BulletBase : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RelatedComponent;
    public GameObject NextBullet;
    BulletComponent ComponentScript;

    public float Damage;
    public float requiredMp;

    public bool penetrate;//�G���ђʂ��邩

    public GameObject damageText;
    GameObject damageCanvas;


   public bool multiHit;//�ł̗�

    float timer;
    public float interval;
    public GameObject hitParticle;

    public GameObject particle;//�p�[�e�B����
    void Start()
    {
        if (RelatedComponent)
        {
            ComponentScript = RelatedComponent.GetComponent<BulletComponent>();
        }
        damageCanvas = GameObject.Find("damageCanvas");

        if (RelatedComponent)
        {
            Damage = RelatedComponent.GetComponent<BulletComponent>().damage;
           // Debug.Log("relatedname=" + RelatedComponent.name + " damage=" + Damage);
            requiredMp = RelatedComponent.GetComponent<BulletComponent>().cost;
          //  Debug.Log("relatedname=" + RelatedComponent.name + " mp=" + requiredMp);


            if (RelatedComponent.GetComponent<BulletComponent>().Adjust == 1)//���͏����
            {
                requiredMp /= 2;
            }
            else if (RelatedComponent.GetComponent<BulletComponent>().Adjust == 2)//�U���͏㏸
            {
                Damage *= 2;
            }
            else if (RelatedComponent.GetComponent<BulletComponent>().Adjust == 5)//�ђʒe
            {
                penetrate = true;
            }

        }



    }

    // Update is called once per frame
    void Update()
    {
        if (RelatedComponent)
        {
            if (ComponentScript.Connect)//�܂������p�[�c������ꍇ
            {
                GameObject DataBase = GameObject.Find("GunSystem");

                NextBullet = DataBase.GetComponent<DataBase>().Bullets[ComponentScript.NextBulletNumber];

            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))//�u���b�N�ɓ���������
        {
            if (particle)
            {
                particle.transform.parent = null;
            }
            Destroy(gameObject);
        }
        if (!multiHit)
        {
            if (collision.CompareTag("enemy"))//�G�ɓ���������
            {
                Debug.Log("�G�ɐڐG �G��="+collision.name);
                //�_���[�W�\�L
                GameObject DamageUI = Instantiate(damageText, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                DamageUI.transform.SetParent(damageCanvas.transform);

                DamageUI.GetComponent<TextMeshProUGUI>().color = Color.white;

                DamageUI.GetComponent<TextMeshProUGUI>().text = Damage.ToString();
                //

                collision.gameObject.GetComponent<enemyBase>().hp -= Damage;//�G��hp�����炷


                //�p�[�e�B�N������
                if(hitParticle != null)
                {
                    Instantiate(hitParticle, transform.position, transform.rotation);
                }
              
                if (!penetrate)//�ђʂ��Ȃ��ꍇ
                {
                    if (particle)
                    {
                        particle.transform.parent = null;
                    }
                
                    Destroy(gameObject);
                }

            }
            if (collision.CompareTag("Box"))
            {
                Destroy(collision.gameObject);
                if (!penetrate)
                {particle.transform.parent = null;
                    Destroy(gameObject);
                }

            }
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (multiHit)
        {
            if (collision.CompareTag("enemy"))//�G�ɓ���������
            {

                timer += Time.deltaTime;

                if (timer >= interval)
                {
                    GameObject DamageUI = Instantiate(damageText, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                    DamageUI.transform.SetParent(damageCanvas.transform);

                    DamageUI.GetComponent<TextMeshProUGUI>().color = Color.white;

                    DamageUI.GetComponent<TextMeshProUGUI>().text = Damage.ToString();
                    //

                    collision.gameObject.GetComponent<enemyBase>().hp -= Damage;//�G��hp�����炷

                    timer = 0;
                }
                //�_���[�W�\�L

            }
        }

    }
}
