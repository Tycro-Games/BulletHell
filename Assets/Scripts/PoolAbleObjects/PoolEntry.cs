using System.Collections.Generic;
using UnityEngine;

class PoolEntry
{
    private Queue<GameObject> _objectPool;

    public PoolEntry()
    {
        _objectPool = new Queue<GameObject>();
    }

    public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (_objectPool.Count == 0)
        {
            return Object.Instantiate(prefab, position, rotation);
        }
        else
        {
            GameObject result = _objectPool.Dequeue();
            result.SetActive(true);
            result.transform.position = position;
            result.transform.rotation = rotation;

            return result;
        }
    }

    public void Destroy(GameObject objectToDestroy)
    {
        // Sanity checks
        Debug.Assert(objectToDestroy.GetComponent<PoolName>() != null);

        objectToDestroy.SetActive(false);
        _objectPool.Enqueue(objectToDestroy);
    }
}
