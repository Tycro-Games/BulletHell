﻿using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class DoorManager : MonoBehaviour
    {
        [HideInInspector]
        public List<LastDoor> doors = new List<LastDoor>();

        [SerializeField]
        private Sprite UnblockedDoor = null;

        public void Activate()
        {
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].Activate(UnblockedDoor);
            }
        }
    }
}