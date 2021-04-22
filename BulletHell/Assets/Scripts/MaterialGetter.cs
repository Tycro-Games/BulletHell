using UnityEngine;

namespace Bog
{
    public class MaterialGetter : MonoBehaviour
    {
        protected Material material = null;

        protected void GetMaterial()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.sharedMaterial = new Material(sprite.sharedMaterial);
            material = sprite.sharedMaterial;
        }
    }
}