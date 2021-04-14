using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public enum Neighbours
    {
        up, down, right, left
    };

    [System.Serializable]
    public class Room
    {
        public Texture2D room = null;

        [HideInInspector]
        public List<Vector2> neighboursFrom = new List<Vector2>();

        [HideInInspector]

        public List<Vector2> neighboursBack = new List<Vector2>();[HideInInspector]

        public Dictionary<Vector2, Neighbours> ToRoomD = new Dictionary<Vector2, Neighbours>();

        [HideInInspector]
        public List<Vector2> ToRoom = new List<Vector2>();

        [HideInInspector]
        public Vector2 currentPos;

        public Room(Vector2 pos, Texture2D type)
        {
            room = type;

            currentPos = pos;
        }
    }
}