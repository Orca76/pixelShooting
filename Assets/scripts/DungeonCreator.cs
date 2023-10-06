using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public int roomCount;
    public GameObject[] roomPrefabs;//0����
    public GameObject startRoom;
    public GameObject endRoom;
    public Vector3 DungeonPosition;
    public int[,] roomData = new int[9, 9];
    Vector2 NowPos;//���[���h���W
    Vector2 NowData;
    public float gap;

    public GameObject[] bridges;//0���c

    GameObject LatestRoom;
    GameObject NextRoom;

    public GameObject BossRoom;


    public struct RoomInfo
    {
        public Vector2 roomWorldPosition;
        public Vector2 roomVirtualPosition;
        public GameObject roomObject;
    }

    public List<RoomInfo> roomArray = new List<RoomInfo>();

    void Start()
    {

        GameObject player = GameObject.FindWithTag("Player");
        if (player.GetComponent<PlayerBase>().currentFloor > 5)
        {
            Instantiate(BossRoom, transform.position, transform.rotation);
        }
        else
        {
            CreateDungeon();
        }
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateRoomData(Vector2 roomPos, Vector2 roomVirtualPos, GameObject gameObject)
    {
        RoomInfo newRoom = new RoomInfo();
        newRoom.roomWorldPosition = roomPos;
        newRoom.roomVirtualPosition = roomVirtualPos;
        newRoom.roomObject = gameObject;
        roomArray.Add(newRoom);
    }
    void CreateDungeon()
    {
        NowPos = DungeonPosition;
        LatestRoom = Instantiate(startRoom, NowPos, transform.rotation);
        roomData[4, 4] = 1;//�z���[4][4]�𒆐S�Ƃ���
        NowData = new Vector3(4, 4);
        roomCount--;

        CreateRoomData(NowPos, NowData, LatestRoom);



        while (roomCount >= 0)
        {
            int Direction = Random.Range(0, 4);//���ɕ����������������߂�
            int roomSize = Random.Range(0, 2);

            //�e�X�g
            RoomInfo RandomRoom = roomArray[Random.Range(0, roomArray.Count)];
            LatestRoom = RandomRoom.roomObject;
            NowPos = RandomRoom.roomWorldPosition;
            NowData = RandomRoom.roomVirtualPosition;
            //�����܂�

            if (Direction == 0)//��
            {
                if ((int)NowData.y + 1 < 9)
                {
                    if (roomData[(int)NowData.x, (int)NowData.y + 1] == 0)//���̏ꏊ�ɕ���������Ă��Ȃ�
                    {

                        NowPos = new Vector2(NowPos.x, NowPos.y + gap); ;//���݈ʒu���X�V

                        if (roomCount == 0)//�Ō�̕���
                        {
                            NextRoom = Instantiate(endRoom, NowPos, transform.rotation);//�����𐶐�

                            LatestRoom.GetComponent<RoomManager>().GateOn[0] = true;//���X�����������̏�̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[2] = true;//�V��������������̉��̓�������J����
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }
                        else


                        if (roomSize == 0)//�����������𐶐��@�������ɃJ�E���g�͖���
                        {
                            NextRoom = Instantiate(roomPrefabs[0], NowPos, transform.rotation);//�����𐶐�

                            LatestRoom.GetComponent<RoomManager>().GateOn[0] = true;//���X�����������̏�̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[2] = true;//�V��������������̉��̓�������J����
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);

                            // roomCount--;
                        }
                        else//�傫�������𐶐��@�������ɃJ�E���g
                        {
                            NextRoom = Instantiate(roomPrefabs[1], NowPos, transform.rotation);//�����𐶐�

                            LatestRoom.GetComponent<RoomManager>().GateOn[0] = true;//���X�����������̏�̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[2] = true;//�V��������������̉��̓�������J����
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }

                    }
                }

            }
            else if (Direction == 1)//�E
            {
                if ((int)NowData.x + 1 < 9)
                {
                    if (roomData[(int)NowData.x + 1, (int)NowData.y] == 0)//��E�̏ꏊ�ɕ���������Ă��Ȃ�
                    {
                        NowPos = new Vector2(NowPos.x + gap, NowPos.y); ;//���݈ʒu���X�V

                        if (roomCount == 0)//�Ō�̕���
                        {
                            NextRoom = Instantiate(endRoom, NowPos, transform.rotation);//�����𐶐�

                            LatestRoom.GetComponent<RoomManager>().GateOn[1] = true;//���X�����������̏�̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[3] = true;//�V��������������̉��̓�������J����
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }
                        else

                        if (roomSize == 0)
                        {
                            NextRoom = Instantiate(roomPrefabs[0], NowPos, transform.rotation);//�����𐶐�

                            // Debug.Log(LatestRoom.GetComponent<RoomManager>().GateOn[3]);
                            LatestRoom.GetComponent<RoomManager>().GateOn[1] = true;//���X�����������̉E�̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[3] = true;//�V��������������̍��̓�������J����
                            CreateBridge(Direction);

                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x + 1, NowData.y);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            //roomCount--;
                        }
                        else
                        {
                            NextRoom = Instantiate(roomPrefabs[1], NowPos, transform.rotation);//�����𐶐�

                            // Debug.Log(LatestRoom.GetComponent<RoomManager>().GateOn[3]);
                            LatestRoom.GetComponent<RoomManager>().GateOn[1] = true;//���X�����������̉E�̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[3] = true;//�V��������������̍��̓�������J����

                            CreateBridge(Direction);

                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x + 1, NowData.y);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }

                    }
                }

            }
            else if (Direction == 2)//��
            {
                if ((int)NowData.y - 1 >= 0)
                {
                    if (roomData[(int)NowData.x, (int)NowData.y - 1] == 0)//������̏ꏊ�ɕ���������Ă��Ȃ�
                    {
                        NowPos = new Vector2(NowPos.x, NowPos.y - gap); ;//���݈ʒu���X�V

                        if (roomCount == 0)//�Ō�̕���
                        {
                            NextRoom = Instantiate(endRoom, NowPos, transform.rotation);//�����𐶐�

                            LatestRoom.GetComponent<RoomManager>().GateOn[2] = true;//���X�����������̏�̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[0] = true;//�V��������������̉��̓�������J����
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }else

                        if (roomSize == 0)
                        {
                            NextRoom = Instantiate(roomPrefabs[0], NowPos, transform.rotation);//�����𐶐�

                            LatestRoom.GetComponent<RoomManager>().GateOn[2] = true;//���X�����������̉��̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[0] = true;//�V��������������̏�̓�������J����

                            CreateBridge(Direction);

                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x, NowData.y - 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            //roomCount--;
                        }
                        else
                        {
                            NextRoom = Instantiate(roomPrefabs[1], NowPos, transform.rotation);//�����𐶐�

                            LatestRoom.GetComponent<RoomManager>().GateOn[2] = true;//���X�����������̉��̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[0] = true;//�V��������������̏�̓�������J����

                            CreateBridge(Direction);

                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x, NowData.y - 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }

                    }
                }

            }
            else if (Direction == 3)//��
            {
                if ((int)NowData.x - 1 >= 0)
                {
                    if (roomData[(int)NowData.x - 1, (int)NowData.y] == 0)
                    {
                        NowPos = new Vector2(NowPos.x - gap, NowPos.y); ;//���݈ʒu���X�V


                        if (roomCount == 0)//�Ō�̕���
                        {
                            NextRoom = Instantiate(endRoom, NowPos, transform.rotation);//�����𐶐�

                            LatestRoom.GetComponent<RoomManager>().GateOn[3] = true;//���X�����������̏�̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[1] = true;//�V��������������̉��̓�������J����
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }else

                        if (roomSize == 0)
                        {
                            NextRoom = Instantiate(roomPrefabs[0], NowPos, transform.rotation);//�����𐶐�

                            LatestRoom.GetComponent<RoomManager>().GateOn[3] = true;//���X�����������̍��̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[1] = true;//�V��������������́��̓�������J����

                            CreateBridge(Direction);

                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x - 1, NowData.y);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            // roomCount--;
                        }
                        else
                        {
                            NextRoom = Instantiate(roomPrefabs[1], NowPos, transform.rotation);//�����𐶐�

                            LatestRoom.GetComponent<RoomManager>().GateOn[3] = true;//���X�����������̍��̓�������J����
                            NextRoom.GetComponent<RoomManager>().GateOn[1] = true;//�V��������������́��̓�������J����

                            CreateBridge(Direction);
                            Debug.Log(NextRoom.GetComponent<RoomManager>().GateOn[1]);

                            LatestRoom = NextRoom;//�ŐV�̕������X�V

                            NowData = new Vector2(NowData.x - 1, NowData.y);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }

                    }
                }

            }
        }
    }


    void CreateBridge(int direction)//������������
    {
        if (direction == 0)//��
        {
           // Debug.Log("��ɍ��");
            if (LatestRoom.tag == "smallRoom")
            {

                Instantiate(bridges[0], new Vector3(NowPos.x, NowPos.y - 1.44f, 0), transform.rotation);

            }
            Instantiate(bridges[0], new Vector3(NowPos.x, NowPos.y - 0.96f, 0), transform.rotation);
            if (NextRoom.tag == "smallRoom")
            {
                Instantiate(bridges[0], new Vector3(NowPos.x, NowPos.y - 0.48f, 0), transform.rotation);
            }
        }
        else if (direction == 1)
        {
          //  Debug.Log("���ɍ��");
            if (LatestRoom.tag == "smallRoom")
            {
               // Debug.Log(111);
                Instantiate(bridges[1], new Vector3(NowPos.x - 1.44f, NowPos.y, 0), transform.rotation);

            }
            Instantiate(bridges[1], new Vector3(NowPos.x - 0.96f, NowPos.y, 0), transform.rotation);
            if (NextRoom.tag == "smallRoom")
            {
                Instantiate(bridges[1], new Vector3(NowPos.x - 0.48f, NowPos.y, 0), transform.rotation);
            }
        }
        else if (direction == 2)
        {
           // Debug.Log("���ɍ��");
            if (LatestRoom.tag == "smallRoom")
            {

                Instantiate(bridges[0], new Vector3(NowPos.x, NowPos.y + 1.44f, 0), transform.rotation);

            }
            Instantiate(bridges[0], new Vector3(NowPos.x, NowPos.y + 0.96f, 0), transform.rotation);
            if (NextRoom.tag == "smallRoom")
            {
                Instantiate(bridges[0], new Vector3(NowPos.x, NowPos.y + 0.48f, 0), transform.rotation);
            }
        }
        else if (direction == 3)
        {
            //Debug.Log("���ɍ��");
            if (LatestRoom.tag == "smallRoom")
            {
               // Debug.Log(111);
                Instantiate(bridges[1], new Vector3(NowPos.x + 1.44f, NowPos.y, 0), transform.rotation);

            }
            Instantiate(bridges[1], new Vector3(NowPos.x + 0.96f, NowPos.y, 0), transform.rotation);
            if (NextRoom.tag == "smallRoom")
            {
                Instantiate(bridges[1], new Vector3(NowPos.x + 0.48f, NowPos.y, 0), transform.rotation);
            }
        }
    }

}
