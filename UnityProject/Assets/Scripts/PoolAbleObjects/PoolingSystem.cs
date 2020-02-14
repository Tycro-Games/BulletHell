using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem
{
    private static Dictionary<string, PoolEntry> ObjectPools = new Dictionary<string, PoolEntry>();

    public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        PoolName poolableObjectComponent = prefab.GetComponent<PoolName>();
        if (poolableObjectComponent != null)
        {
            // step 1 - check if we have a pool entry - if not we create one
            string className = poolableObjectComponent.PoolableObjectClass;

            PoolEntry actualPoolEntry = null;
            if (ObjectPools.ContainsKey(className))
            {
                actualPoolEntry = ObjectPools[className];
            }
            else
            {
                actualPoolEntry = new PoolEntry();
            }

            // step 2 - instantiate using the pool entry object
            return actualPoolEntry.Instantiate(prefab, position, rotation);

        }
        else
        {
            // we instantiate without shame
            return Object.Instantiate(prefab, position, rotation);
        }
    }

    public static void Destroy(GameObject gameObject)
    {
        PoolName poolableObjectComponent = gameObject.GetComponent<PoolName>();
        if (poolableObjectComponent != null)
        {
            // step 1 - check if we have a pool entry - if not we create one
            string className = poolableObjectComponent.PoolableObjectClass;

            PoolEntry actualPoolEntry = null;
            if (ObjectPools.ContainsKey(className))
            {
                actualPoolEntry = ObjectPools[className];
            }
            else
            {
                actualPoolEntry = new PoolEntry();
                ObjectPools.Add(className, actualPoolEntry);
            }

            // step 2 - destroy the object using the pool entry
            actualPoolEntry.Destroy(gameObject);
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }
}