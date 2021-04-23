using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class ExplosionColor : MaterialGetter
    {
        private RandomColor color = null;
        private SpriteRenderer renderer;

        private void Start()
        {
            color = GetComponent<RandomColor>();
            renderer = GetComponent<SpriteRenderer>();
            GetMaterial();
            material.SetColor("_color", color.RandomBetween());
            renderer.color = color.RandomBetween();
        }
    }
}