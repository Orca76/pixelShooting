using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class shop : MonoBehaviour
{
    //�R���|�[�l���g�𐶐����A���i��ݒ肷��

    float collisionDistance;//�Q�[���I�u�W�F�N�g�ƃv���C���[�Ԃ̐ڐG����Ɏg�p��������
    GameObject player;
    public int costType;//���i�̒l�i�O���[�v�@�������̂ƍ������̂����݁@0�`50 20~100 50~200
    int cost;//���i�̒l�i
    DataBase data;

    public TextMeshProUGUI costText;//�l�i��\������text
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("GunSystem").GetComponent<DataBase>();
        switch (costType)//���i�̒l�i�����肷��
        {
            case 0:
                cost = Random.Range(0, 50);
                break;
            case 1:
                cost = Random.Range(20, 100);
                break;
            case 2:
                cost = Random.Range(50, 200);
                break;
        }
        GameObject item = Instantiate(data.Components[Random.Range(0, data.Components.Length)], transform.position, transform.rotation);//�R���|�[�l���g�������_���ň����
        item.GetComponent<BulletComponent>().itemPrice = cost;
        costText.text = "$" + cost;

    }

    // Update is called once per frame
    void Update()
    {


    }

}
