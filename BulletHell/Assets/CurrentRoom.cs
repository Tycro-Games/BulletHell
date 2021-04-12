using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class CurrentRoom : MonoBehaviour
    {
        private Transform newCamera = null;
        private PlayerMovement player = null;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            newCamera = Camera.main.transform;
        }

        public void ChangeCurrentRoom(Vector3 newRoom, Vector2 destination)
        {
            newRoom.z = newCamera.position.z;
            newCamera.position = newRoom;
            player.ChangePosition(destination,newRoom);
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