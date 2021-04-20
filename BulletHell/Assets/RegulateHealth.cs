using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class RegulateHealth : MonoBehaviour
    {
        private Material material;

        [SerializeField]
        [Range(0, 1)]
        private float multiplier = 1;

        [ColorUsage(true, true)]
        [SerializeField]
        private Color start;

        [ColorUsage(true, true)]
        [SerializeField]
        private Color end;

        private void Start()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.sharedMaterial = new Material(sprite.sharedMaterial);
            material = sprite.sharedMaterial;
            start = material.GetColor("_color");
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

            float inter = Mathf.InverseLerp(0, 100, HP * multiplier);
            col = Color.Lerp(end, start, inter);

            material.SetColor("_color", col);
            Debug.Log(inter);
        }
    }
}