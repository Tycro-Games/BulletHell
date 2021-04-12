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
        private Size size;

        [SerializeField]
        public float roomH = 10;

        [SerializeField]
        public float roomW = 18;

        [SerializeField]
        private GameObject Door;

        [SerializeField]
        private Tiles[] tiles;

        [SerializeField]
        private Cell[] rooms;

        [SerializeField]
        private Room start;

        private List<Room> grid = new List<Room>();
        private List<Texture2D> roomsToTake = new List<Texture2D>();
        private List<Vector2> possibles = new List<Vector2>();
        private HashSet<Vector2> occupied = new HashSet<Vector2>();

        private void Start()
        {
            GenerateRooms();
        }

        public void GenerateRooms()
        {
            grid.Add(start);
            start.currentPos = new Vector2(size.col / 2, size.rows / 2);
            GetNeight(start);

            possibles.Remove(start.currentPos);

            InitRooms();
            while (roomsToTake.Count != 0)
            {
                int index = Random.Range(0, possibles.Count);
                Vector2 pos = possibles[index];
                if (occupied.Contains(pos))
                {
                    possibles.Remove(pos);
                    continue;
                }
                Texture2D setRoom = roomsToTake[Random.Range(0, roomsToTake.Count)];
                Room newRoom = new Room(pos, setRoom);
                roomsToTake.Remove(setRoom);
                grid.Add(newRoom);

                GetNeight(newRoom);
            }
            MakeRooms();
        }

        private void MakeRooms()
        {
            foreach (Room room in grid)
            {
                Texture2D texture = room.room;
                GameObject roomObj = new GameObject(texture.name);
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
                                Instantiate(tile.obj, new Vector2(roomObj.transform.position.x - texture.width / 2, roomObj.transform.position.y - texture.height / 2) + new Vector2(i, j), Quaternion.identity, roomObj.transform);
                                break;
                            }
                        }
                    }
                if (room != grid[0])
                    for (int i = 0; i < room.neighboursFrom.Count; i++)
                    {
                        GameObject door1 = Instantiate(Door, room.neighboursFrom[i], Quaternion.identity, roomObj.transform);//tile for the door to the room

                        GameObject door2 = Instantiate(Door, room.neighboursBack[i], Quaternion.identity, roomObj.transform);//tile for the room to the door

                        Teleport teleport1 = door1.GetComponent<Teleport>();
                        Teleport teleport2 = door2.GetComponent<Teleport>();
                        teleport1.Destination = door2.transform.position;
                        teleport1.parentRoom = room.ToRoom[i];
                        teleport2.Destination = door1.transform.position;
                        teleport2.parentRoom = room.currentPos;


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
                    room.neighboursFrom.Add(newPos - new Vector2(NX[i] * roomW / 2 + NX[i] * 1, NY[i] * roomH / 2 + NY[i] * 1));
                    room.neighboursBack.Add(newPos - new Vector2(NX[i] * roomW / 2 - NX[i] * 1, NY[i] * roomH / 2 - NY[i] * 1));
                    room.ToRoom.Add(newPos);
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