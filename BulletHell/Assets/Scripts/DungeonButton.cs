using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bog
{
    public class DungeonButton : MonoBehaviour
    {
        private ButtonManager buttonDungeon = null;
        private SpriteRenderer sprite = null;
        private bool first = false;

        [SerializeField]
        private UnityEvent OnTouch = null;

        private void Start()
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
            buttonDungeon = FindObjectOfType<ButtonManager>();
            buttonDungeon.dungeonButtons.Add(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Teleport") && !first)
            {
                buttonDungeon.RemoveOne(this);
                first = true;
                OnTouch?.Invoke();
            }
        }
    }
}