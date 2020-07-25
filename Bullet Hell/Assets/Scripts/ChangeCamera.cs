using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
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
        roomGen = GameObject.Find("RoomInstance(Clone)");
        roomGenScript = roomGen.GetComponent<ActualFuckingRoomGenerator>();
        transform.position = new Vector3(x * 50f + roomGenScript.width / 2f, y * 50f + roomGenScript.height / 2f - 0.5f, -5f);
        Camera.main.orthographicSize = (roomGenScript.width + 1) / 3;
        x = mapGenScript.x;
        y = mapGenScript.y;
    }
}
