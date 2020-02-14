using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class BulletSpawner : Spawner
{
    public Transform path;
    PathPlacer placerPoints;
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
        StartCoroutine(pathHolder.MoveToLastWaypoint());


    }


}
