using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesSpawningManager : MonoBehaviour
{
    public static UnityEvent OnEnemySpawn;
    [SerializeField]
    private static Transform EnemySpawnersParent = null;
    private static readonly List<EnemySpawner> currentSpawners = new List<EnemySpawner> ();
    private void Awake ()
    {
        if (EnemySpawnersParent == null)
        {
            EnemySpawnersParent = transform.GetChild (0);
        }
    }
    
    bool CheckForNull ()
    {
        if (currentSpawners.Count == 0)
            return true;
        return false;
    }
    public void EverySpawnerAct ()
    {
        if (CheckForNull ())
            return;
        foreach (EnemySpawner spawner in currentSpawners)
        {
            spawner.Spawn ();
        }
    }
    #region static
    public static void DeleteASpawner (EnemySpawner enemySpawner)
    {
        currentSpawners.Remove (enemySpawner);
        Destroy (enemySpawner.gameObject);

    }
    public static void AddASpawner (Vector3 pos, GameObject Spawner)
    {
        GameObject enemySpawner = Instantiate (Spawner, pos, Quaternion.identity);
        enemySpawner.transform.SetParent (EnemySpawnersParent);

        currentSpawners.Add (enemySpawner.GetComponent<EnemySpawner> ());
    }
    #endregion
}
