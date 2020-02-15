using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PathPlacer : MonoBehaviour
{

    public float spacing = .1f;
    public float resolution = 1;
    [HideInInspector]
    public Vector2[] points;
    [SerializeField]
    GameObject Point = null;
    PathCreator pathCreator;
    GameObject lastpoint;
    private void Start()
    {
        pathCreator = GetComponent<PathCreator>();
        Move();
    }
    private void OnEnable()
    {
        Path.moved += Move;
    }
    private void OnDisable()
    {
        Path.moved -= Move;
    }
    void Move()
    {
        Delete();
        points = pathCreator.path.CalculateEvenlySpacedPoints(spacing, resolution);
        for (int i = 0; i < points.Length; i++)
        {
            GameObject g = PoolingSystem.Instantiate(Point, points[i], Quaternion.identity);
            g.transform.localScale = Vector3.one * spacing * .5f;
            g.transform.SetParent(transform);
        }
    }
    public Vector2[] GetPoints()
    {
        return points;
    }
    void Delete()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            PoolingSystem.Destroy(transform.GetChild(i).gameObject);
        }

    }


}
