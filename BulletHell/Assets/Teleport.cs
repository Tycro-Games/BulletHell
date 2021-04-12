using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class Teleport : MonoBehaviour
    {
        public Vector2 Destination ;
        public Vector3 parentRoom;
        public static Action<Vector3, Vector2> OnTeleport;
        PlayerMovement movement;
        Transform player;
        [SerializeField]
        private float offset = 0.5f;
        private void Start()
        {
            movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            player = movement.transform;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")&&!movement.Teleported)
            {
                OnTeleport?.Invoke(parentRoom, Destination+dir(Destination)*offset);
                movement.Teleported = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && movement.Teleported)
            {
               
                movement.Teleported = false;
            }
     
        }
        Vector2 dir(Vector3 target)
        {
           return ( target-player.position).normalized;
        }
    }
}