using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class ColorChanger : MonoBehaviour
    {
        private SpriteRenderer ren = null;

        private void Start()
        {
            ren = GetComponent<SpriteRenderer>();
            Color.RGBToHSV(ren.color, out float H, out float S, out float V);

            H = Random.Range(0f, 1f);
            Color newCol = Color.HSVToRGB(H, S, V);
            ren.color = newCol;
        }
    }
}