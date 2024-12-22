using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public float detectRange;
    public GameObject[] enemies;
    public int totalEnemiesNum;//�����ɂ���G�̑���
    public int fieldEnemiesNum;//��x�ɏo�Ă�����G�̏��
    public GameObject[] createPosition;
    public bool enteredRoom;//�v���C���[�������ɓ�����
    int createIndex;
    GameObject player;

    public GameObject ClearText;
    int a = 1;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("������" + Vector2.Distance(gameObject.transform.position, player.transform.position));

        if (detectRange > Vector2.Distance(gameObject.transform.position, player.transform.position))
        {
            enteredRoom = true;
        }
        

            if (enteredRoom)//�v���C���[�������ɓ�����
            {
            int  level = player.GetComponent<PlayerBase>().currentFloor;
            enemies = GameObject.Find("GunSystem").GetComponent<DataBase>().enemies;
            if (totalEnemiesNum >= 0)//�����ɂ܂��o���ł���G���c���Ă���
                {
                    if (GameObject.FindGameObjectsWithTag("enemy").Length < fieldEnemiesNum)//�����ɂ܂��o���ł���X�y�[�X������
                    {
                    var CreatePos = gameObject.transform.position + new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.6f, 0.6f), 0);
                        Instantiate(enemies[Random.Range(0, Mathf.Min(level*2,enemies.Length))], CreatePos, transform.rotation);

                        if (createIndex == createPosition.Length - 1)
                        {
                            createIndex = 0;
                        }
                        else
                        {
                            createIndex++;
                        }
                        totalEnemiesNum--;

                    }
            }
            else
            {
                //�G�S��
                if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
                {
                    if (a > 0)
                    {
                        Instantiate(ClearText);
                        a = 0;
                    }
                }
              
            
            }

            }
        

    }
}
