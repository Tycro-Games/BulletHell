using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    private EnemyInfo enemy;

    public  delegate void OnSpawn(EnemyInfo info);
    public static event OnSpawn onSpawn;
   
    void GenerateEnemy ()
    {
        enemy = new EnemyInfo
        {
            EnemyPrefab = EnemyGenerator.RandomPrefab (EnemyPrefabs)
        };
       
    }

    public void Spawn ()
    {
        GenerateEnemy ();
        Debug.Assert (enemy != null, "object null");
        onSpawn (enemy);
    }

}
