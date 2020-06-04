using UnityEngine;

public class Spawner
{

    public static GameObject Spawn (GameObject ObjectToSpawn, Vector2 pos, Quaternion rot, Transform parentTransform = null)
    {
        GameObject spawnedObject = PoolingObjectsSystem.Instantiate (ObjectToSpawn, pos, rot);

        if (parentTransform != null)
            spawnedObject.transform.SetParent (parentTransform);
        return spawnedObject;
    }
}