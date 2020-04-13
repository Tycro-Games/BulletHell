using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawnersSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject Spawner;
    [SerializeField]
    private float range = 5.0f;
    [SerializeField]
    private float height = 0.0f;
    public void ChangeSpawner (GameObject newSpawner)
    {
        Spawner = newSpawner;
    }
    public void AddSpawners ()
    {
        Vector2 random = Random.insideUnitCircle;
        Vector3 pos = new Vector3 (random.x, height, random.y);

        EnemiesSpawningManager.AddASpawner (pos * range, Spawner);
    }
}
