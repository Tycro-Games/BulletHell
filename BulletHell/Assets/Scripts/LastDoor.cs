using Bog.Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class LastDoor : MonoBehaviour
    {
        private Collider2D doorCollide = null;
        private Teleport tele = null;


        private void Start()
        {
            doorCollide = GetComponent<Collider2D>();
            tele = GetComponent<Teleport>();
            DoorManager doorManager = FindObjectOfType<DoorManager>();
            doorManager.doors.Add(this);
        }

        public void Activate()
        {
            tele.enabled = true;
            doorCollide.enabled = true;
        }
    }
}