using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHolder : MonoBehaviour
{

    private Vector2[] points = null;

    public void Init(Vector2[] Pathpoints)
    {
        points = Pathpoints;

    }

    public IEnumerator MoveToLastWaypoint(float speed)
    {
        transform.position = points[0];

        for (int i = 0; i < points.Length; i++)
        {

            Vector3 target = points[i];


            while (transform.position != target)
            {


                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);


                yield return null;
            }
        }
        PoolingSystem.Destroy(gameObject);
    }

}
