using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject ObjectToSpawn = null;
    protected GameObject spawnedObject;

    public virtual void Spawn()
    {
        spawnedObject = PoolingSystem.Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);


    }

}
