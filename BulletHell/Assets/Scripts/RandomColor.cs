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

        public Color RandomBetween()
        {
            return Color.Lerp(firstColor, secondColor, Random.Range(0f, 1f));
        }
    }
}