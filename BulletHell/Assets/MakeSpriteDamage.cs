using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bog
{
    public class MakeSpriteDamage : MonoBehaviour
    {
        private SpriteRenderer ren = null;

        [SerializeField]
        private AnimationCurve animationUp = null;

        [SerializeField]
        private AnimationCurve animationDown = null;

        [SerializeField]
        private float Transparent = 0.5f;

        [SerializeField]
        private float Speed = 1f;

        [SerializeField]
        private float SpeedDown = 1f;

        private void Start()
        {
            ren = GetComponent<SpriteRenderer>();
            Color NewCol = ren.color;
            NewCol.a = 0;
            ren.color = NewCol;
        }

        public void Animate()
        {
            StopCoroutine("Anim");
            Color NewCol = ren.color;
            NewCol.a = 0;
            ren.color = NewCol;
            StartCoroutine(Anim());
        }

        private IEnumerator Anim()
        {
            float oldApl = ren.color.a;
            while (oldApl <= Transparent)
            {
                Color NewCol = ren.color;
                //   oldApl = Mathf.InverseLerp(0, Transparent, oldApl);
                oldApl += animationUp.Evaluate(Time.deltaTime * Speed);
                NewCol.a = oldApl;
                ren.color = NewCol;
                yield return null;
            }

            while (oldApl > 0)
            {
                Color NewCol = ren.color;
                //    downApl = Mathf.InverseLerp(0, Transparent, downApl);
                oldApl -= animationDown.Evaluate(Time.deltaTime * SpeedDown);

                NewCol.a = oldApl;
                ren.color = NewCol;
                yield return null;
            }
        }
    }
}