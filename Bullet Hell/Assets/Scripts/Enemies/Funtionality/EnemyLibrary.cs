using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLibrary : MonoBehaviour
{
    [SerializeField]
    private GameObject[] EnemyPrefabs = null;
    // Start is called before the first frame update
    void Start ()
    {
            EnemiesSpawningManager.EnemySpawnersParent = transform.GetChild (0);//init
    }
    public void AllSpawnersEnemy ()
    {
        EnemiesSpawningManager.EverySpawnerAct (EnemyPrefabs[Random.Range(0,EnemyPrefabs.Length)]);
    }

}
