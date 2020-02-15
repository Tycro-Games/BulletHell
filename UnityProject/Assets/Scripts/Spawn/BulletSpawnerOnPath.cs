using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletSpawnerOnPath : Spawner
{
    public Transform path;
    PathPlacer placerPoints;
    [Header("Parameters for Bullets")]
    [SerializeField]
    private float speed = 0;
    private void Start()
    {
        placerPoints = path.GetComponent<PathPlacer>();

    }


    public override void Spawn()
    {
        base.Spawn();
        spawnedObject.transform.SetParent(transform);

        PathHolder pathHolder = spawnedObject.GetComponent<PathHolder>();


        pathHolder.Init(placerPoints.GetPoints());//reset the path and shit
        StartCoroutine(pathHolder.MoveToLastWaypoint(speed));


    }


}
