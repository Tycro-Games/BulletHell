using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHolder : MonoBehaviour
{

    private Vector2[] points = null;
    [Header("Parameters")]
    [SerializeField]
    private float speed = 0.0f;



    public void Init(Vector2[] Pathpoints)
    {
        points = Pathpoints;
    }

    public IEnumerator MoveToLastWaypoint()
    {


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
