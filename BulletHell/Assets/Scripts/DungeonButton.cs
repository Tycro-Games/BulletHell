using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class DungeonButton : MonoBehaviour
    {
        private ButtonManager buttonDungeon = null;
        private SpriteRenderer sprite = null;

        private void Start()
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
            buttonDungeon = FindObjectOfType<ButtonManager>();
            buttonDungeon.dungeonButtons.Add(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Teleport"))
            {
                buttonDungeon.RemoveOne(this);
            }
        }
    }
}