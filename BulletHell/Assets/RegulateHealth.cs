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
        [SerializeField]
        private float initIntensity=1;
        float hp=100;
        private void Start()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.sharedMaterial = new Material(sprite.sharedMaterial);
            material = sprite.sharedMaterial;
            hp = 100;
        }

        private void OnEnable()
        {
            PlayerStats.Hit += UpdateHealth;
        }

        private void OnDisable()
        {
            PlayerStats.Hit -= UpdateHealth;
        }

        public void UpdateHealth(int HP)
        {
            Color col = material.GetColor("_color");
            
            Color.RGBToHSV(material.GetColor("_color"), out float H, out float S, out float V);
         
           
            col = new Color(col.r * HP * Multiplier, col.g * HP * Multiplier, col.b * HP * Multiplier);
            material.SetColor("_color", col);
        }
    }
}