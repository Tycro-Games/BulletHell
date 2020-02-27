using UnityEngine;

public class Spawner
{
    
    public static GameObject Spawn (GameObject ObjectToSpawn,Transform transform)
    {
        GameObject spawnedObject = PoolingObjectsSystem.Instantiate (ObjectToSpawn, transform.position, Quaternion.identity);
        spawnedObject.transform.SetParent (transform);
        return spawnedObject;
    }

}