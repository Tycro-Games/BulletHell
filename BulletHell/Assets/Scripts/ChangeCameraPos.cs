using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPos : MonoBehaviour
{
    public int x = 0, y = 0;
    GameObject mapGen;
    GameObject roomGen;
    MapGenerator mapGenScript;
    ActualFuckingRoomGenerator roomGenScript;
    private void Start()
    {
        mapGen = GameObject.Find("MapGenerator");
        mapGenScript = mapGen.GetComponent<MapGenerator>();
        
        x = mapGenScript.x;
        y = mapGenScript.y;
    }

    private void Update()
    {
        //if(Input.Get)
        // I'll be back
        float posX = x * 50f + mapGenScript.rooms[x, y].roomWidth / 2f;
        float posY = y * 50f + mapGenScript.rooms[x, y].roomHeight / 2f;
        transform.position = new Vector3(posX, posY, -5f);
        x = mapGenScript.x;
        y = mapGenScript.y;
        //Camera.main.orthographicSize = (Mathf.Max(mapGenScript.rooms[x, y].roomWidth, mapGenScript.rooms[x, y].roomHeight) + 1);
    }
}
