using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class ExplosionColor : MaterialGetter
    {
        private RandomColor color = null;

        private void Start()
        {
            color = GetComponent<RandomColor>();
            GetMaterial();
            material.SetColor("_color", color.RandomBetween());
        }
    }
}