using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortOrder : MonoBehaviour
{


    public void Check()
    {

        Collider2D[] cols = new Collider2D[10];
        int i = Physics2D.OverlapCircleNonAlloc(transform.position, 1, cols);

        SpriteRenderer[] renderers = new SpriteRenderer[i];

        for (int j = 0; j < i; j++)
        {
            if (cols[j] != null)
            {
                SpriteRenderer sprite = cols[j].GetComponent<SpriteRenderer>();
                if (sprite != null)
                {

                    renderers[j] = sprite;
                    renderers[j].sortingOrder = Mathf.FloorToInt(renderers[j].transform.position.y * -100);
                }
                else
                    continue;
            }


        }


    }

}
