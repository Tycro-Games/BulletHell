using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class Flip : MonoBehaviour
    {
        private void Start()
        {
           SpriteRenderer spriteRenderer= GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = Random.Range(0.0f, 1.0f) < .5f ? true : false;
        }
    }
}