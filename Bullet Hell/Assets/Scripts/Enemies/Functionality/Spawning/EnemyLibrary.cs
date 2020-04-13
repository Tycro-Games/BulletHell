using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLibrary : MonoBehaviour
{
    [SerializeField]
    private Transform parentTransform = null;
    [SerializeField]
    private GameObject[] EnemyPrefabs = null;
    // Start is called before the first frame update
    void Start ()
    {
        if (parentTransform == null)
            EnemiesSpawningManager.EnemySpawnersParent = transform.GetChild (0);//init
    }
    public void AllSpawnersEnemy ()
    {
        EnemiesSpawningManager.EverySpawnerAct (EnemyPrefabs[0]);
    }

}
