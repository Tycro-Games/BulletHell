using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public enum Neighbours
    {
        left, up, right, down
    };

    [System.Serializable]
    public class Room
    {
        public Texture2D room = null;

        [HideInInspector]
        public Vector2[] neighbours = new Vector2[4];

        [HideInInspector]
        public Vector2 currentPos;

        public Room(Vector2 pos, Texture2D type)
        {
            room = type;
            //  neighbours[dir] = 1;
            currentPos = pos;
        }
    }
}