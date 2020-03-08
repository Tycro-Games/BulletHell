using System.Collections.Generic;
using UnityEngine;

public class PoolingEntry
{
    Queue<GameObject> entries;
    public PoolingEntry ()
    {
        entries = new Queue<GameObject> ();
    }
    public GameObject Instantiate (GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (entries.Count == 0)
        {
            return Object.Instantiate (prefab, pos, rot);
        }
        else
        {
            GameObject objectToAdd = entries.Dequeue ();

            objectToAdd.transform.position = pos;
            objectToAdd.transform.rotation = rot;
            objectToAdd.SetActive (true);
            

            return objectToAdd;
        }
    }
    public void Destroy (GameObject objectToAdd)
    {
        objectToAdd.SetActive (false);
        entries.Enqueue (objectToAdd);
    }
}
