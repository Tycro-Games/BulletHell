﻿using System.Collections;
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

        [SerializeField]
        private SpriteRenderer Buttons = null;

        [SerializeField]
        private SpriteRenderer LastButton = null;

        private PlayerStats player;

        private void Start()
        {
            player = FindObjectOfType<PlayerStats>();
            sprite = GetComponentInChildren<SpriteRenderer>();
            buttonDungeon = FindObjectOfType<ButtonManager>();
            buttonDungeon.dungeonButtons.Add(this);
            buttonDungeon.OnChange += ChangeSprite;
            buttonDungeon.OnEnd += ChangeLastSprite;
            ChangeSprite(buttonDungeon.current);
        }

        private void ChangeSprite(Sprite sprite)
        {
            Buttons.sprite = sprite;
        }

        private void ChangeLastSprite(Sprite sprite)
        {
            LastButton.sprite = sprite;
        }

        private void OnDisable()
        {
            buttonDungeon.OnChange -= ChangeSprite;
            buttonDungeon.OnEnd -= ChangeLastSprite;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Teleport") && !first)
            {
                player.ResetHP();
                buttonDungeon.RemoveOne(this);
                first = true;
                OnTouch?.Invoke();
            }
        }
    }
}