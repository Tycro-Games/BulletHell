using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
    public int width = 30;
    public int height = 30;
    public int chance = 50;
    public int numberOfRooms = 80;
    public float bossChance = 10;
    public float treasureChance = 7;
    public float bushChance = 10;
    public float ruinChance = 10;
    public GameObject roomObj;
    public GameObject roomSpawner;
    public FuckingRoom[,] rooms;
    public Transform mapRoot;

    ChangeCameraPos cameraChanger;

    public int roomSpriteWidth = 12;
    public int roomSpriteHeight = 6;

    public int x;
    public int y;

    int count = 0;

    void Start()
    {
        x = width / 2;
        y = height / 2;
        CreateRooms();
        SetDoors();
        SpawnMap();
        SpawnRooms();
    }

    private void Update()
    {
        if(Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(Input.GetKeyDown("w") && y + 1 < height && rooms[x, y + 1].empty == false)
        {
            rooms[x, y].sexuallyActive = false;
            y++;      
            rooms[x, y].sexuallyActive = true;
            SpawnMap();
        }
        if (Input.GetKeyDown("a") && x - 1 > 0 && rooms[x - 1, y].empty == false)
        {
            rooms[x, y].sexuallyActive = false;
            x--;
            rooms[x, y].sexuallyActive = true;
            SpawnMap();
        }
        if (Input.GetKeyDown("s") && y - 1 > 0 && rooms[x, y - 1].empty == false)
        {
            rooms[x, y].sexuallyActive = false;
            y--;
            rooms[x, y].sexuallyActive = true;
            SpawnMap();
        }
        if (Input.GetKeyDown("d") && x + 1 < width && rooms[x + 1, y].empty == false)
        {
            rooms[x, y].sexuallyActive = false;
            x++;
            rooms[x, y].sexuallyActive = true;
            SpawnMap();
        }
    }

    void CreateRooms()
    {
        rooms = new FuckingRoom[width, height];
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                rooms[i, j] = new FuckingRoom();
                if (i == width / 2 && j == height / 2)
                {
                    rooms[i, j].type = 1;
                    rooms[i, j].roomWidth = Random.Range(10, 30);
                    rooms[i, j].roomHeight = Random.Range(10, 30);
                }            
            }
        }
        GenerateRoom(width / 2, height / 2);
        if (numberOfRooms > width * height)
            numberOfRooms = width * height;
        if (count != numberOfRooms)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }

    void GenerateRoom(int x, int y)
    {
        rooms[x, y].empty = false;
        rooms[x, y].searched = true;
        if(x != width / 2 && y != height / 2)
        {
            if (Random.Range(0, 100) < bossChance)
            {
                rooms[x, y].type = 3;
            }
            if (Random.Range(0, 100) < treasureChance)
            {
                rooms[x, y].type = 4;
            }
            if(Random.Range(0, 100) < bushChance) // aia cu tufisuri
            {
               rooms[x, y].type = 5;
            }
            if(Random.Range(0, 100) < ruinChance) // aia cu relicva din mijloc
            {
               rooms[x, y].type = 6;
            }
        }
        
        count++;
        if (count != numberOfRooms && Random.Range(0, 100) < chance && x > 0 && rooms[x - 1, y].searched == false)
        {
            GenerateRoom(x - 1, y);
        }
        if (count != numberOfRooms && Random.Range(0, 100) < chance && y > 0 && rooms[x, y - 1].searched == false)
        {
           GenerateRoom(x, y - 1);
        }
        if (count != numberOfRooms && Random.Range(0, 100) < chance && x < width - 1 && rooms[x + 1, y].searched == false)
        {
            GenerateRoom(x + 1, y);
        }
        if (count != numberOfRooms && Random.Range(0, 100) < chance && y < height - 1 && rooms[x, y + 1].searched == false)
        {
            GenerateRoom(x, y + 1);
        }
    }

    void SetDoors()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                rooms[i, j].x = i;
                rooms[i, j].y = j;

                if (rooms[i, j].empty == false)
                {
                    if (i > 0 && rooms[i - 1, j].empty == false)
                    {
                        rooms[i, j].doorLeft = true;
                    }
                    if (i < width - 1 && rooms[i + 1, j].empty == false)
                    {
                        rooms[i, j].doorRight = true;
                    }
                    if (j > 0 && rooms[i, j - 1].empty == false)
                    {
                        rooms[i, j].doorBot = true;
                    }
                    if (j < height - 1 && rooms[i, j + 1].empty == false)
                    {
                        rooms[i, j].doorTop = true;
                    }
                }
            }
        }
    }

    void SpawnMap()
    {
        foreach (Transform child in mapRoot.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (FuckingRoom room in rooms)
        {
            if(room.empty == false)
            {
                Vector2 spawnPos = new Vector2(room.x * (roomSpriteWidth + roomSpriteWidth / 3), room.y * (roomSpriteHeight + roomSpriteHeight / 3));
                SpriteMapping mapper = Object.Instantiate(roomObj, spawnPos, Quaternion.identity).GetComponent<SpriteMapping>();

                if (room.sexuallyActive)
                    mapper.type = 2;
                else
                    mapper.type = room.type;
                mapper.doorTop = room.doorTop;
                mapper.doorBot = room.doorBot;
                mapper.doorRight = room.doorRight;
                mapper.doorLeft = room.doorLeft;
                mapper.gameObject.transform.parent = mapRoot;
            }
            
        }
    }

    void SpawnRooms()
    {
        foreach (FuckingRoom room in rooms)
        {
            if (room.empty == false)
            {
                Vector2 roomSpawnPos = new Vector2(room.x * 50f, room.y * 50f);
                ActualFuckingRoomGenerator roomGen = Object.Instantiate(roomSpawner, roomSpawnPos, Quaternion.identity).GetComponent<ActualFuckingRoomGenerator>();
                roomGen.width = Random.Range(10, 30);
                roomGen.height = Random.Range(10, 30);
                roomGen.doorTop = room.doorTop;
                roomGen.doorBot = room.doorBot;
                roomGen.doorRight = room.doorRight;
                roomGen.doorLeft = room.doorLeft;
                roomGen.roomType = room.type;
            }

        }
    }
}
