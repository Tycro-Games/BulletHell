using Bog.Assets.Scripts.Dungeon;
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
    public class Cells
    {
        public int count;
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

    [System.Serializable]
    public class TilesRand
    {
        public Color col;
        public GameObject[] obj;
    }

    public class Dungeon : MonoBehaviour
    {
        private CurrentRoom roomFirst;

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
        private TilesRand[] tilesRandom = null;

        [SerializeField]
        private RoomScriptable[] oneRoomRandom = null;

        public Texture2D[] RandomRooms = null;

        [SerializeField]
        private Room start = null;

        [SerializeField]
        private RoomScriptable end = null;

        [HideInInspector]
        public Dictionary<Vector2, GameObject> grid = new Dictionary<Vector2, GameObject>();

        private List<Room> roomList = new List<Room>();

        private List<Texture2D> roomsToTake = new List<Texture2D>();
        private List<Vector2> possibles = new List<Vector2>();
        private HashSet<Vector2> occupied = new HashSet<Vector2>();

        private void Start()
        {
            roomFirst = GetComponent<CurrentRoom>();
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

            PlaceRooms(end.room[Random.Range(0, end.room.Length)]);
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

        private void PlaceRooms(Texture2D RoomToPlace)
        {
            Vector2 pos;

            for (int i = 0; i < possibles.Count; i++)
            {
                pos = possibles[i];
                if (occupied.Contains(pos))
                {
                    possibles.Remove(pos);
                }
            }
            int index = Random.Range(0, possibles.Count);
            pos = possibles[index];
            while (occupied.Contains(pos))
            {
                possibles.Remove(pos);
                index = Random.Range(0, possibles.Count);
                pos = possibles[index];
            }
            Texture2D setRoom = RoomToPlace;
            Room newRoom = new Room(pos, setRoom);

            roomList.Add(newRoom);

            GetNeight(newRoom);
        }

        private void MakeRooms()
        {
            foreach (Room room in roomList)
            {
                Texture2D texture = room.room;
                GameObject roomObj = Instantiate(RoomParent);
                if (texture == null)
                    continue;
                roomObj.name = texture.name;
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
                        if (tilesRandom.Length != 0)
                            foreach (TilesRand tile in tilesRandom)
                            {
                                if (tile.col == color)
                                {
                                    Instantiate(tile.obj[Random.Range(0, tile.obj.Length)], new Vector2(roomObj.transform.position.x - texture.width / 2, roomObj.transform.position.y - texture.height / 2) + new Vector2(i, j), Quaternion.identity, roomObj.transform.GetChild(0));
                                    break;
                                }
                            }
                    }
                if (room != roomList[0] && room != roomList[roomList.Count - 1])
                {
                    roomObj.transform.GetChild(1).gameObject.SetActive(false);
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
                        teleport1.otherDoor = door2;
                        teleport1.Destination = door2.transform.position;
                        teleport1.parentRoom = room.ToRoom[i];
                        teleport2.Destination = door1.transform.position;
                        teleport2.parentRoom = room.currentPos;
                        teleport2.otherDoor = door1;
                    }
                }
                else if (room == roomList[roomList.Count - 1])
                {
                    roomObj.transform.GetChild(1).gameObject.SetActive(false);
                    roomObj.name = "End Room";
                    for (int i = 0; i < room.neighboursFrom.Count; i++)
                    {
                        int d1 = (int)room.ToRoomD[room.neighboursFrom[i]];

                        GameObject door1 = Instantiate(lastDoor[d1], room.neighboursFrom[i], Quaternion.identity, roomObj.transform);//tile for the door to the room
                        int d2 = (int)room.ToRoomD[room.neighboursBack[i]];

                        GameObject door2 = Instantiate(lastDoor[d2], room.neighboursBack[i], Quaternion.identity, grid[room.ToRoom[i]].transform);//tile for the room to the door

                        Teleport teleport1 = door1.GetComponent<Teleport>();
                        Teleport teleport2 = door2.GetComponent<Teleport>();
                        teleport1.otherDoor = door2;
                        teleport1.Destination = door2.transform.position;
                        teleport1.parentRoom = room.ToRoom[i];
                        teleport2.otherDoor = door1;
                        teleport2.Destination = door1.transform.position;
                        teleport2.parentRoom = room.currentPos;
                    }
                }
                else if (room == roomList[0])
                {
                    roomFirst.lastRoom = roomObj;
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
            foreach (RoomScriptable room in oneRoomRandom)
            {
                for (int i = 0; i < room.count; i++)
                {
                    roomsToTake.Add(room.room[Random.Range(0, room.room.Length)]);
                }
            }
        }
    }
}