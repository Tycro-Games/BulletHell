using UnityEngine;

public class Spawner
{

    public static GameObject Spawn (GameObject ObjectToSpawn, Transform transform, bool parent = false, Transform parentTransform=null)
    {
        GameObject spawnedObject = PoolingObjectsSystem.Instantiate (ObjectToSpawn, transform.position, transform.rotation);
        if (parent)
            spawnedObject.transform.SetParent (parentTransform);
        return spawnedObject;
    }
}