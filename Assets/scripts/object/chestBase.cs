using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestBase : MonoBehaviour
{
    // Start is called before the first frame update
    public float distance;//�f�o�b�O�p�@�v���C���[�Ƃ̋���
    GameObject player;
    DataBase data;
   public bool opened = false;

    public Sprite OpenedSprite;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        data = GameObject.Find("GunSystem").GetComponent<DataBase>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("distance" + Vector3.Distance(gameObject.transform.position, player.transform.position));
        if (!opened)//�J���Ă��Ȃ�
        {
            if (distance > Vector3.Distance(gameObject.transform.position, player.transform.position))//�v���C���[������ȏ�߂��i�󔠂ɐG��Ă���j
            {
                //�{�^���̏����ǉ����邩��
                Instantiate(data.Components[Random.Range(0, data.Components.Length)], transform.position, transform.rotation);//�R���|�[�l���g�������_���ň����
                gameObject.GetComponent<SpriteRenderer>().sprite = OpenedSprite;
                opened = true;
            }
        }
        
    }

}
