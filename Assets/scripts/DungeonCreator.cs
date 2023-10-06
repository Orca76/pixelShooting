using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public int roomCount;
    public GameObject[] roomPrefabs;//0が小
    public GameObject startRoom;
    public GameObject endRoom;
    public Vector3 DungeonPosition;
    public int[,] roomData = new int[9, 9];
    Vector2 NowPos;//ワールド座標
    Vector2 NowData;
    public float gap;

    public GameObject[] bridges;//0が縦

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
        roomData[4, 4] = 1;//配列は[4][4]を中心とする
        NowData = new Vector3(4, 4);
        roomCount--;

        CreateRoomData(NowPos, NowData, LatestRoom);



        while (roomCount >= 0)
        {
            int Direction = Random.Range(0, 4);//次に部屋を作る方向を決める
            int roomSize = Random.Range(0, 2);

            //テスト
            RoomInfo RandomRoom = roomArray[Random.Range(0, roomArray.Count)];
            LatestRoom = RandomRoom.roomObject;
            NowPos = RandomRoom.roomWorldPosition;
            NowData = RandomRoom.roomVirtualPosition;
            //ここまで

            if (Direction == 0)//上
            {
                if ((int)NowData.y + 1 < 9)
                {
                    if (roomData[(int)NowData.x, (int)NowData.y + 1] == 0)//一つ上の場所に部屋を作っていない
                    {

                        NowPos = new Vector2(NowPos.x, NowPos.y + gap); ;//現在位置を更新

                        if (roomCount == 0)//最後の部屋
                        {
                            NextRoom = Instantiate(endRoom, NowPos, transform.rotation);//部屋を生成

                            LatestRoom.GetComponent<RoomManager>().GateOn[0] = true;//元々あった部屋の上の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[2] = true;//新しく作った部屋の下の入り口を開ける
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//最新の部屋を更新

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }
                        else


                        if (roomSize == 0)//小さい部屋を生成　生成数にカウントは無し
                        {
                            NextRoom = Instantiate(roomPrefabs[0], NowPos, transform.rotation);//部屋を生成

                            LatestRoom.GetComponent<RoomManager>().GateOn[0] = true;//元々あった部屋の上の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[2] = true;//新しく作った部屋の下の入り口を開ける
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//最新の部屋を更新

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);

                            // roomCount--;
                        }
                        else//大きい部屋を生成　生成数にカウント
                        {
                            NextRoom = Instantiate(roomPrefabs[1], NowPos, transform.rotation);//部屋を生成

                            LatestRoom.GetComponent<RoomManager>().GateOn[0] = true;//元々あった部屋の上の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[2] = true;//新しく作った部屋の下の入り口を開ける
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//最新の部屋を更新

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }

                    }
                }

            }
            else if (Direction == 1)//右
            {
                if ((int)NowData.x + 1 < 9)
                {
                    if (roomData[(int)NowData.x + 1, (int)NowData.y] == 0)//一つ右の場所に部屋を作っていない
                    {
                        NowPos = new Vector2(NowPos.x + gap, NowPos.y); ;//現在位置を更新

                        if (roomCount == 0)//最後の部屋
                        {
                            NextRoom = Instantiate(endRoom, NowPos, transform.rotation);//部屋を生成

                            LatestRoom.GetComponent<RoomManager>().GateOn[1] = true;//元々あった部屋の上の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[3] = true;//新しく作った部屋の下の入り口を開ける
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//最新の部屋を更新

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }
                        else

                        if (roomSize == 0)
                        {
                            NextRoom = Instantiate(roomPrefabs[0], NowPos, transform.rotation);//部屋を生成

                            // Debug.Log(LatestRoom.GetComponent<RoomManager>().GateOn[3]);
                            LatestRoom.GetComponent<RoomManager>().GateOn[1] = true;//元々あった部屋の右の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[3] = true;//新しく作った部屋の左の入り口を開ける
                            CreateBridge(Direction);

                            LatestRoom = NextRoom;//最新の部屋を更新

                            NowData = new Vector2(NowData.x + 1, NowData.y);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            //roomCount--;
                        }
                        else
                        {
                            NextRoom = Instantiate(roomPrefabs[1], NowPos, transform.rotation);//部屋を生成

                            // Debug.Log(LatestRoom.GetComponent<RoomManager>().GateOn[3]);
                            LatestRoom.GetComponent<RoomManager>().GateOn[1] = true;//元々あった部屋の右の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[3] = true;//新しく作った部屋の左の入り口を開ける

                            CreateBridge(Direction);

                            LatestRoom = NextRoom;//最新の部屋を更新

                            NowData = new Vector2(NowData.x + 1, NowData.y);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }

                    }
                }

            }
            else if (Direction == 2)//下
            {
                if ((int)NowData.y - 1 >= 0)
                {
                    if (roomData[(int)NowData.x, (int)NowData.y - 1] == 0)//一つしたの場所に部屋を作っていない
                    {
                        NowPos = new Vector2(NowPos.x, NowPos.y - gap); ;//現在位置を更新

                        if (roomCount == 0)//最後の部屋
                        {
                            NextRoom = Instantiate(endRoom, NowPos, transform.rotation);//部屋を生成

                            LatestRoom.GetComponent<RoomManager>().GateOn[2] = true;//元々あった部屋の上の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[0] = true;//新しく作った部屋の下の入り口を開ける
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//最新の部屋を更新

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }else

                        if (roomSize == 0)
                        {
                            NextRoom = Instantiate(roomPrefabs[0], NowPos, transform.rotation);//部屋を生成

                            LatestRoom.GetComponent<RoomManager>().GateOn[2] = true;//元々あった部屋の下の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[0] = true;//新しく作った部屋の上の入り口を開ける

                            CreateBridge(Direction);

                            LatestRoom = NextRoom;//最新の部屋を更新

                            NowData = new Vector2(NowData.x, NowData.y - 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            //roomCount--;
                        }
                        else
                        {
                            NextRoom = Instantiate(roomPrefabs[1], NowPos, transform.rotation);//部屋を生成

                            LatestRoom.GetComponent<RoomManager>().GateOn[2] = true;//元々あった部屋の下の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[0] = true;//新しく作った部屋の上の入り口を開ける

                            CreateBridge(Direction);

                            LatestRoom = NextRoom;//最新の部屋を更新

                            NowData = new Vector2(NowData.x, NowData.y - 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }

                    }
                }

            }
            else if (Direction == 3)//左
            {
                if ((int)NowData.x - 1 >= 0)
                {
                    if (roomData[(int)NowData.x - 1, (int)NowData.y] == 0)
                    {
                        NowPos = new Vector2(NowPos.x - gap, NowPos.y); ;//現在位置を更新


                        if (roomCount == 0)//最後の部屋
                        {
                            NextRoom = Instantiate(endRoom, NowPos, transform.rotation);//部屋を生成

                            LatestRoom.GetComponent<RoomManager>().GateOn[3] = true;//元々あった部屋の上の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[1] = true;//新しく作った部屋の下の入り口を開ける
                            CreateBridge(Direction);


                            LatestRoom = NextRoom;//最新の部屋を更新

                            NowData = new Vector2(NowData.x, NowData.y + 1);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            roomCount--;
                        }else

                        if (roomSize == 0)
                        {
                            NextRoom = Instantiate(roomPrefabs[0], NowPos, transform.rotation);//部屋を生成

                            LatestRoom.GetComponent<RoomManager>().GateOn[3] = true;//元々あった部屋の左の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[1] = true;//新しく作った部屋の→の入り口を開ける

                            CreateBridge(Direction);

                            LatestRoom = NextRoom;//最新の部屋を更新

                            NowData = new Vector2(NowData.x - 1, NowData.y);
                            roomData[(int)NowData.x, (int)NowData.y] = 1;
                            CreateRoomData(NowPos, NowData, LatestRoom);
                            // roomCount--;
                        }
                        else
                        {
                            NextRoom = Instantiate(roomPrefabs[1], NowPos, transform.rotation);//部屋を生成

                            LatestRoom.GetComponent<RoomManager>().GateOn[3] = true;//元々あった部屋の左の入り口を開ける
                            NextRoom.GetComponent<RoomManager>().GateOn[1] = true;//新しく作った部屋の→の入り口を開ける

                            CreateBridge(Direction);
                            Debug.Log(NextRoom.GetComponent<RoomManager>().GateOn[1]);

                            LatestRoom = NextRoom;//最新の部屋を更新

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


    void CreateBridge(int direction)//部屋を作る方向
    {
        if (direction == 0)//上
        {
           // Debug.Log("上に作る");
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
          //  Debug.Log("→に作る");
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
           // Debug.Log("下に作る");
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
            //Debug.Log("左に作る");
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
