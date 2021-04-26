using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class RandomColor : MonoBehaviour
    {
        [ColorUsage(true, true)]
        [SerializeField]
        private Color firstColor;

        [ColorUsage(true, true)]
        [SerializeField]
        private Color secondColor;

        private SpriteRenderer sprite = null;

        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        public Color RandomBetween()
        {
            return Color.Lerp(firstColor, secondColor, Random.Range(0f, 1f));
        }

        public void SetColor()
        {
            sprite.color = RandomBetween();
        }
    }
}