using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomTile
{
    public int x = 0;
    public int y = 0;
    public bool door;
    public bool walkable;
    public bool props = false;
    public float propSize = 0.8f;
}
