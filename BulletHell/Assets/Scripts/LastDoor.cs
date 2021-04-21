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

        [SerializeField]
        private SpriteRenderer ren = null;

        private void Start()
        {
            ren = GetComponentInChildren<SpriteRenderer>();
            doorCollide = GetComponent<Collider2D>();
            tele = GetComponent<Teleport>();
            DoorManager doorManager = FindObjectOfType<DoorManager>();
            doorManager.doors.Add(this);
        }

        public void Activate(Sprite sprite)
        {
            ren.sprite = sprite;
            tele.enabled = true;
            doorCollide.enabled = true;
        }
    }
}