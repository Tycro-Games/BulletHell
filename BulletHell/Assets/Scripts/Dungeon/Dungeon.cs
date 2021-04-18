using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    [System.Serializable]
    public struct Cell
    {
        public int count;
        public Room room;
    }

    [System.Serializable]
    public struct Size
    {
        public int rows;
        public int col;
    }

    [System.Serializable]
    public struct Tiles
    {
        public Color col;
        public GameObject obj;
    }

    public class Dungeon : MonoBehaviour
    {
        [SerializeField]
        private Size size = new Size();

        [SerializeField]
        public float roomH = 10;

        [SerializeField]
        public float roomW = 18;

        [SerializeField]
        private GameObject[] normalDoor = null;

        [SerializeField]
        private GameObject[] lastDoor = null;

        [SerializeField]
        private GameObject RoomParent = null;

        [SerializeField]
        private Tiles[] tiles = null;

        [SerializeField]
        private Cell[] rooms = null;

        [SerializeField]
        private Room start = null;

        [SerializeField]
        private Room end = null;

        [HideInInspector]
        public Dictionary<Vector2, GameObject> grid = new Dictionary<Vector2, GameObject>();

        private List<Room> roomList = new List<Room>();

        private List<Texture2D> roomsToTake = new List<Texture2D>();
        private List<Vector2> possibles = new List<Vector2>();
        private HashSet<Vector2> occupied = new HashSet<Vector2>();

        private void Start()
        {
            GenerateRooms();
        }

        public void GenerateRooms()
        {
            roomList.Add(start);
            start.currentPos = new Vector2(size.col / 2, size.rows / 2);
            GetNeight(start);

            possibles.Remove(start.currentPos);

            InitRooms();
            while (roomsToTake.Count != 0)
            {
                PlaceRooms();
            }

            PlaceRooms(end);
            MakeRooms();
        }

        private void PlaceRooms()
        {
            int index = Random.Range(0, possibles.Count);
            Vector2 pos = possibles[index];
            if (occupied.Contains(pos))
            {
                possibles.Remove(pos);
                return;
            }
            Texture2D setRoom = roomsToTake[Random.Range(0, roomsToTake.Count)];
            Room newRoom = new Room(pos, setRoom);
            roomsToTake.Remove(setRoom);
            roomList.Add(newRoom);

            GetNeight(newRoom);
        }

        private void PlaceRooms(Room RoomToPlace)
        {
            bool placed = false;
            while (!placed)
            {
                int index = Random.Range(0, possibles.Count);
                Vector2 pos = possibles[index];
                if (occupied.Contains(pos))
                {
                    possibles.Remove(pos);
                    return;
                }
                Texture2D setRoom = RoomToPlace.room;
                Room newRoom = new Room(pos, setRoom);

                roomList.Add(newRoom);
                placed = true;
                GetNeight(newRoom);
            }
        }

        private void MakeRooms()
        {
            foreach (Room room in roomList)
            {
                Texture2D texture = room.room;
                GameObject roomObj = Instantiate(RoomParent);

                grid.Add(room.currentPos, roomObj);
                roomObj.transform.position = room.currentPos;
                roomObj.transform.parent = transform;

                for (int i = 0; i < texture.width; i++)//make tiles
                    for (int j = 0; j < texture.height; j++)
                    {
                        Color color = texture.GetPixel(i, j);

                        foreach (Tiles tile in tiles)
                        {
                            if (tile.col == color)
                            {
                                Instantiate(tile.obj, new Vector2(roomObj.transform.position.x - texture.width / 2, roomObj.transform.position.y - texture.height / 2) + new Vector2(i, j), Quaternion.identity, roomObj.transform.GetChild(0));
                                break;
                            }
                        }
                    }
                if (room != roomList[0] && room != roomList[roomList.Count - 1])
                    for (int i = 0; i < room.neighboursFrom.Count; i++)
                    {
                        int d1 = (int)room.ToRoomD[room.neighboursFrom[i]];

                        GameObject door1 = Instantiate(normalDoor[d1], room.neighboursFrom[i], Quaternion.identity, roomObj.transform);//tile for the door to the room
                        int d2 = (int)room.ToRoomD[room.neighboursBack[i]];

                        GameObject door2 = Instantiate(normalDoor[d2], room.neighboursBack[i],
                            Quaternion.identity,
                            grid[room.ToRoom[i]].transform);//tile for the room to the door

                        Teleport teleport1 = door1.GetComponent<Teleport>();
                        Teleport teleport2 = door2.GetComponent<Teleport>();
                        teleport1.Destination = door2.transform.position;
                        teleport1.parentRoom = room.ToRoom[i];
                        teleport2.Destination = door1.transform.position;
                        teleport2.parentRoom = room.currentPos;
                    }
                else if (room == roomList[roomList.Count - 1])
                {
                    roomObj.name = "End Room";
                    for (int i = 0; i < room.neighboursFrom.Count; i++)
                    {
                        int d1 = (int)room.ToRoomD[room.neighboursFrom[i]];

                        GameObject door1 = Instantiate(lastDoor[d1], room.neighboursFrom[i], Quaternion.identity, roomObj.transform);//tile for the door to the room
                        int d2 = (int)room.ToRoomD[room.neighboursBack[i]];

                        GameObject door2 = Instantiate(lastDoor[d2], room.neighboursBack[i], Quaternion.identity, grid[room.ToRoom[i]].transform);//tile for the room to the door

                        Teleport teleport1 = door1.GetComponent<Teleport>();
                        Teleport teleport2 = door2.GetComponent<Teleport>();
                        teleport1.Destination = door2.transform.position;
                        teleport1.parentRoom = room.ToRoom[i];
                        teleport2.Destination = door1.transform.position;
                        teleport2.parentRoom = room.currentPos;
                    }
                }
            }
        }

        private float[] NX = { 0, 0, 1, -1 };
        private float[] NY = { 1, -1, 0, 0 };

        private void GetNeight(Room room)
        {
            occupied.Add(room.currentPos);

            for (int i = 0; i < 4; i++)
            {
                Vector2 newPos = room.currentPos + new Vector2(NX[i] * roomW, NY[i] * roomH);
                if (!occupied.Contains(newPos))
                    possibles.Add(newPos);
                else
                {
                    Vector2 from = newPos - new Vector2(NX[i] * roomW / 2 + NX[i] * 1, NY[i] * roomH / 2 + NY[i] * 1);
                    room.neighboursFrom.Add(from);
                    Vector2 back = newPos - new Vector2(NX[i] * roomW / 2 - NX[i] * 1, NY[i] * roomH / 2 - NY[i] * 1);
                    room.neighboursBack.Add(back);
                    room.ToRoom.Add(newPos);
                    Neighbours neigh = new Neighbours();
                    if (i == 0)
                        neigh = Neighbours.up;
                    else if (i == 1)
                        neigh = Neighbours.down;
                    else if (i == 2)
                        neigh = Neighbours.right;
                    else if (i == 3)
                        neigh = Neighbours.left;
                    room.ToRoomD.Add(from, neigh);
                    if (neigh == Neighbours.up)
                        neigh = Neighbours.down;
                    else if (neigh == Neighbours.down)
                        neigh = Neighbours.up;
                    else if (neigh == Neighbours.right)
                        neigh = Neighbours.left;
                    else if (neigh == Neighbours.left)
                        neigh = Neighbours.right;
                    room.ToRoomD.Add(back, neigh);
                }
            }
        }

        private void InitRooms()
        {
            foreach (Cell room in rooms)
            {
                for (int i = 0; i < room.count; i++)
                {
                    roomsToTake.Add(room.room.room);
                }
            }
        }
    }
}