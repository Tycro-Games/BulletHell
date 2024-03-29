using System;
using UnityEngine;
using UnityEngine.Events;

namespace Bog
{
    public class Teleport : TeleportBase
    {
        public Vector2 Destination;
        public Vector3 parentRoom;
        public static Action<Vector3, Vector2> OnTeleport;
        private PlayerMovement movement;
        private Transform player;

        [SerializeField]
        private float dist = 1f;

        [SerializeField]
        private float offset = 0.5f;

        [SerializeField]
        private UnityEvent OnEnter = null;

        [HideInInspector]
        public GameObject otherDoor = null;

        [SerializeField]
        private Sprite newSprite = null;

        private bool first = false;

        private void Start()
        {
            Init();
            movement = FindObjectOfType<PlayerMovement>();
            player = movement.transform;
        }

        public void ChangeSprites()
        {
            GetComponentInChildren<SpriteRenderer>().sprite = newSprite;
            otherDoor.GetComponentInChildren<SpriteRenderer>().sprite = newSprite;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (canTele && Vector2.Distance(player.position, transform.position) <= dist)
                if (collision.CompareTag("Teleport") && !movement.Teleported)
                {
                    OnTeleport?.Invoke(parentRoom, Destination + dir(Destination) * offset);
                    movement.Teleported = true;
                    if (!first)
                    {
                        ChangeSprites();
                        OnEnter?.Invoke();
                        first = true;
                    }
                }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Teleport") && movement.Teleported)
            {
                movement.Teleported = false;
            }
        }

        private Vector2 dir(Vector3 target)
        {
            return (target - player.position).normalized;
        }
    }
}