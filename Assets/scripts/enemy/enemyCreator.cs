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
                        Instantiate(enemies[Random.Range(0, Mathf.Min(level*2,enemies.Length))], createPosition[createIndex].transform.position, transform.rotation);

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

            }
        

    }
}
