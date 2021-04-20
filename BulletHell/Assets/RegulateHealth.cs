﻿using System.Collections;
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
        private int minVal = 0;
        [SerializeField]
        private int maxVal = 100;
       
        private void Start()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.sharedMaterial = new Material(sprite.sharedMaterial);
            material = sprite.sharedMaterial;
          
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
         
            float inter= Mathf.InverseLerp(minVal, maxVal, HP);
            col = new Color(col.r * inter * Multiplier, col.g * inter * Multiplier, col.b * inter * Multiplier);
            material.SetColor("_color", col);
        }
    }
}