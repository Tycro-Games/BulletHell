using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualFuckingRoomGenerator : MonoBehaviour
{
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

    public float minScale = 0.8f;
    public float maxScale = 1.2f;
    public float maxDist = 1f;

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
        if (width % 2 == 0)
            width++;
        if (height % 2 == 0)
            height++;
        tiles = new RoomTile[width, height];
        GenerateTiles(width, height);
    }
    
    public Vector2 ReturnLocalPos(RoomTile roomTile)
    {
        return new Vector2(roomTile.x, roomTile.y);
    }

    public Vector2 ReturnGlobalPos(RoomTile roomTile)
    {
        Vector2 pos = new Vector2(transform.parent.position.x + roomTile.x, transform.parent.position.y + roomTile.y);
        return pos;
    }

    public void GenerateTiles(int width, int height)
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                GenerateTile(i, j);
                tiles[i, j].x = i;
                tiles[i, j].y = j;             
            }
        }

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (Random.Range(0, 100) < propChance && tiles[i, j].walkable == true && tiles[i, j].door == false)
                {
                    if (roomType == 4 || roomType == 6)
                    {
                        if (!(i < width / 2 + 3 && i > width / 2 - 3) && !(j < height / 2 + 3 && j > height / 2 - 3))
                        {
                            SpawnProps(i, j);
                        }
                        else
                        {
                            Vector3 spawnPos = new Vector3(width / 2 + transform.position.x, height / 2 + transform.position.y, 0);
                            Instantiate(props[10], spawnPos, Quaternion.identity);
                        }
                    }
                    else
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

        if (roomType == 4 || roomType == 6)
        {
            if ((x < width / 2 + 3 && x > width / 2 - 3) && (y < height / 2 + 3 && y > height / 2 - 3))
            {
                tiles[x, y].door = false;
                tiles[x, y].walkable = true;
            }
        }
    }

    void SpawnProps(int x, int y)
    {
        if(roomType != 5)
        {
            foreach (GameObject prop in props)
            {
                if (Random.Range(0, 100) < 100 / props.Length - 1 && prop != props[10])
                {
                    tiles[x, y].props = true;
                    Vector3 spawnPos = new Vector3(tiles[x, y].x + transform.position.x, tiles[x, y].y + transform.position.y, 0);
                    GameObject obj = Instantiate(prop, spawnPos, Quaternion.identity);
                    RandomizeScale(obj, x, y);
                    RandomizePos(obj, x, y);
                    break;
                }
            }
        }
        else
        {
            if(Random.Range(0, 100) < 30)
            {
                tiles[x, y].props = true;
                Vector3 spawnPos = new Vector3(tiles[x, y].x + transform.position.x, tiles[x, y].y + transform.position.y, 0);
                GameObject obj = Instantiate(props[9], spawnPos, Quaternion.identity);
                RandomizeScale(obj, x, y);
            }
        }
        
    }

    void RandomizeScale(GameObject obj, int x, int y)
    {
        float scale;
        if((x < width && tiles[x + 1, y].walkable == false) || (x > 0 && tiles[x - 1, y].walkable == false) || (y < height && tiles[x, y + 1].walkable == false) || (y > 0 && tiles[x, y - 1].walkable == false))
        {
            scale = Random.Range(minScale, 1);
        }
        else if((x < width && tiles[x + 1, y].props) || (x > 0 && tiles[x - 1, y].props) || (y < height && tiles[x, y + 1].props) || (y > 0 && tiles[x, y - 1].props))
        {
            scale = Random.Range(minScale, Mathf.Max(tiles[x + 1, y].propSize, tiles[x - 1, y].propSize, tiles[x, y + 1].propSize, tiles[x, y - 1].propSize));
        }
        else
        {
            scale = Random.Range(minScale, maxScale);
        }
        obj.transform.localScale = new Vector3(scale * 2f, scale * 2f, scale * 2f);
    }

    void RandomizePos(GameObject obj, int x, int y)
    {
        float xOffset = 0;
        float yOffset = 0;
        if(x < width && tiles[x + 1, y].walkable == false)
        {
            if(x > 0 && tiles[x - 1, y].walkable == false)
            {
                xOffset = 0;
            }
            else
            {
                xOffset = Random.Range(-maxDist, 0f);
            }
        }
        else if(x > 0 && tiles[x - 1, y].walkable == false)
        {
            xOffset = Random.Range(0f, maxDist);
        }
        else
        {
            yOffset = Random.Range(-maxDist, maxDist);
        }
        if (y < height && tiles[x, y + 1].walkable == false)
        {
            if (y > 0 && tiles[x, y - 1].walkable == false)
            {
                yOffset = 0;
            }
            else
            {
                yOffset = Random.Range(-maxDist, 0f);
            }
        }
        else if(y > 0 && tiles[x, y - 1].walkable == false)
        {
            yOffset = Random.Range(0f, maxDist);
        }
        else
        {
            yOffset = Random.Range(-maxDist, maxDist);
        }

        if (x < width && tiles[x + 1, y].props == true)
        {
            if (x > 0 && tiles[x - 1, y].props == true)
            {
                xOffset = 0;
            }
            else
            {
                xOffset += Random.Range(-0.3f, 0f);
            }
        }
        else if (x > 0 && tiles[x - 1, y].props == true)
        {
            xOffset += Random.Range(0f, 0.3f);
        }
        if (y < height && tiles[x, y + 1].props == true)
        {
            if (y > 0 && tiles[x, y - 1].props == true)
            {
                yOffset = 0;
            }
            else
            {
                yOffset += Random.Range(-0.3f, 0f);
            }
        }
        else if (y > 0 && tiles[x, y - 1].props == true)
        {
            yOffset += Random.Range(0f, 0.3f);
        }

        Vector3 offset = new Vector3(xOffset, yOffset, 0);

        obj.transform.localPosition += offset;
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
