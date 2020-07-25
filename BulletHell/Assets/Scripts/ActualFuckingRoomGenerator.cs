using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualFuckingRoomGenerator : MonoBehaviour
{
    public Texture2D tex;
    public int width = 17;
    public int height = 11;
    public int obstacleChance = 20;
    public int propChance = 40;
    public bool doorLeft = false;
    public bool doorRight = false;
    public bool doorBot = false;
    public bool doorTop = false;

    public int roomType;
    public GameObject[] props;
    public GameObject[] door;
    public GameObject[] empty;
    public GameObject[] wall;
    public RoomTile[,] tiles;

    public bool randomizeSeed;
    public int seed;

    //public ColorToGameObject[] mappings;

    public void Start()
    {
        if (randomizeSeed)
        {
            seed = Random.Range(0, 9999999);
        }

        Random.InitState(seed);
        GenerateRoom();
        SpawnRoom();
    }

    public void GenerateRoom()
    {
        
        tiles = new RoomTile[width, height];
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GenerateTile(i, j);
                tiles[i, j].x = i;
                tiles[i, j].y = j;

                if(tiles[i, j].walkable == true && tiles[i, j].door != true)
                {
                    if(Random.Range(0, 100) < propChance)
                    {
                        SpawnProps(i, j);
                    }       
                }
            }
        }
    }

    public void GenerateTile(int x, int y)
    {
        tiles[x, y] = new RoomTile();
        if (y == 0 && x == width / 2 && doorBot == true)
        {
            tiles[x, y].door = true;
            tiles[x, y].walkable = true;
        }
        else if (y == height - 1 && x == width / 2 && doorTop == true)
        {
            tiles[x, y].door = true;
            tiles[x, y].walkable = true;
        }
        else if (y == height / 2 && x == 0 && doorLeft == true)
        {
            tiles[x, y].door = true;
            tiles[x, y].walkable = true;
        }
        else if (y == height / 2 && x == width - 1 && doorRight == true)
        {
            tiles[x, y].door = true;
            tiles[x, y].walkable = true;
        }
        else if (x == 0 || y == 0 || y == height - 1 || x == width - 1)
        {
            tiles[x, y].door = false;
            tiles[x, y].walkable = false;
        }
        else if (y == height / 2 && doorRight == true && x > width / 2)
        {
            tiles[x, y].door = false;
            tiles[x, y].walkable = true;
        }
        else if (y == height / 2 && doorLeft == true && x < width / 2)
        {
            tiles[x, y].door = false;
            tiles[x, y].walkable = true;
        }
        else if (x == width / 2 && doorBot == true && y < height / 2)
        {
            tiles[x, y].door = false;
            tiles[x, y].walkable = true;
        }
        else if (x == width / 2 && doorTop == true && y > height / 2)
        {
            tiles[x, y].door = false;
            tiles[x, y].walkable = true;
        }
        else
        {
            if (Random.Range(0, 100) < obstacleChance)
            {
                tiles[x, y].door = false;
                tiles[x, y].walkable = false;
            }
            else
            {
                tiles[x, y].door = false;
                tiles[x, y].walkable = true;
            }
        }
    }

    void SpawnProps(int x, int y)
    {
        foreach(GameObject prop in props)
        {
            if(Random.Range(0, 100) < 100 / props.Length)
            {
                Vector3 spawnPos = new Vector3(tiles[x, y].x + transform.position.x, tiles[x, y].y + transform.position.y, 0);
                Instantiate(prop, spawnPos, Quaternion.identity);
                break;
            }
        }
    }

    public void SpawnRoom()
    {
        foreach (RoomTile tile in tiles)
        {
            Vector2 spawnPos = new Vector2((tile.x + transform.position.x), (tile.y + transform.position.y));
            if(tile.door == true)
            {
                Instantiate(door[roomType], spawnPos, Quaternion.identity).transform.parent = this.transform; ;

            }
            else if(tile.walkable == true)
            {
                Instantiate(empty[roomType], spawnPos, Quaternion.identity).transform.parent = this.transform; ;
            }
            else
            {
                Instantiate(wall[roomType], spawnPos, Quaternion.identity).transform.parent = this.transform; ;
            }
        }

                            // I met her in a club down in old Soho
                            // Where you drink champagne and it tastes just like
                            //  Coca Cola
                            // C - O - L - A Cola
                            // She walked up to me and she asked me to dance
                            // I asked her her name and in a dark brown voice she said, "Lola"
                            // L - O - L - A Lola, lo lo lo lo Lola
                            // Well, I'm not the world's most physical guy
                            // But when she squeezed me tight she nearly broke my spine
                            // Oh my Lola, lo lo lo lo Lola
                            // Well, I'm not dumb but I can't understand
                            // Why she walked like a woman but talked like a man
                            // Oh my Lola, lo lo lo lo Lola, lo lo lo lo Lola
                            // Well, we drank champagne and danced all night
                            // Under electric candlelight
                            // She picked me up and sat me on her knee
                            // She said, "Little boy, won't you come home with me?"
                            // Well, I'm not the world's most passionate guy
                            // But when I looked in her eyes
                            // Well, I almost fell for my Lola
                            // Lo lo lo lo Lola, lo lo lo lo Lola
                            // Lola, lo lo lo lo Lola, lo lo lo lo Lola
                            // I pushed her away
                            // I walked to the door
                            // I fell to the floor
                            // I got down on my knees
                            // Then I looked at her, and she at me
                            // Well, that's the way that I want it to stay
                            // And I always want it to be that way for my Lola
                            // Lo lo lo lo Lola
                            // Girls will be boys, and boys will be girls
                            // It's a mixed up, muddled up, shook up world
                            // Except for Lola
                            // Lo lo lo lo Lola
                            // Well, I'd left home just a week before
                            // And I'd never ever kissed a woman before
                            // But Lola smiled and took me by the hand
                            // She said, "Little boy, gonna make you a man"
                            // Well, I'm not the world's most masculine man
                            // But I know what I am and I'm glad I'm a man
                            // And so is Lola
                            // Lo lo lo lo Lola, lo lo lo lo Lola
                            // Lola, lo lo lo lo Lola, lo lo lo lo Lola
                            // Lola, lo lo lo lo Lola, lo lo lo lo Lola
                            // Lola, lo lo lo lo Lola, lo lo lo lo Lola
                            // Lola, lo lo lo lo Lola, lo lo lo lo Lola
                            // Lola, lo lo lo lo Lola, lo lo lo lo Lola                                 
    }
}
