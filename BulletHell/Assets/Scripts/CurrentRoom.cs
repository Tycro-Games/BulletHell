using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Bog
{
    public class CurrentRoom : MonoBehaviour
    {
        private Transform newCamera = null;
        private PlayerMovement player = null;
        private Dungeon dungeon;
        private GameObject lastRoom;

        private void Start()
        {
            dungeon = GetComponent<Dungeon>();
            player = FindObjectOfType<PlayerMovement>();
            newCamera = FindObjectOfType<CinemachineVirtualCamera>().transform;
        }

        public void ChangeCurrentRoom(Vector3 newRoom, Vector2 destination)
        {
            newRoom.z = newCamera.position.z;
            newCamera.position = newRoom;
            player.ChangePosition(destination, newRoom);
            if (lastRoom != null)
                lastRoom.transform.GetChild(1).gameObject.SetActive(false);
            lastRoom = dungeon.grid[newRoom];
            lastRoom.transform.GetChild(1).gameObject.SetActive(true);
            ActivateCamera(lastRoom.GetComponent<RoomTriggerStart>());
        }

        public void ActivateCamera(RoomTriggerStart room)
        {
            room.FirstEnter();
        }

        private void OnEnable()
        {
            Teleport.OnTeleport += ChangeCurrentRoom;
        }

        private void OnDisable()
        {
            Teleport.OnTeleport -= ChangeCurrentRoom;
        }
    }
}