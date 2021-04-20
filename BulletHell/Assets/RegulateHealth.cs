using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class RegulateHealth : MonoBehaviour
    {
        [SerializeField]
        private float Multiplier = 1f;

        private Material material;

        private void Start()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.sharedMaterial = new Material(sprite.sharedMaterial);
        }

        public void UpdateHealth(int HP)
        {
            material.shader.GetDependency("_color");
        }
    }
}