using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class TeleportEnd : TeleportBase
    {
        private Collider2D circle = null;

        private void Start()
        {
            circle = GetComponent<Collider2D>();
            Init();
        }

        public override void Activate()
        {
            circle.enabled = true;
        }
    }
}