using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityEnemySpawnAdd : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemySpawner = null;
    public void CallAddEnemySpawn ()
    {
        if (EnemySpawner.Length != 0)
            EnemiesSpawningManager.AddASpawner (Random.insideUnitSphere * 10, EnemySpawner[Random.Range (0, EnemySpawner.Length)]);
    }
}
