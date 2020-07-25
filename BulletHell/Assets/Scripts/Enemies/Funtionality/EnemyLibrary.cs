using UnityEngine;

public class EnemyLibrary : MonoBehaviour
{
    public GameObject[] EnemyPrefabs = null;

    public static EnemyLibrary enemyLibrary = null;

    [SerializeField]
    private Transform parentSpawners = null;

    private void Awake ()
    {
        DontDestroyOnLoad (gameObject);
        if (enemyLibrary == null)
            enemyLibrary = this;
        else
            Destroy (gameObject);
    }

    // Start is called before the first frame update
    void Start ()
    {
        EnemiesSpawningManager.EnemySpawnersParent = parentSpawners;//init
        
    }
    public void AllSpawnersEnemy ()
    {
        EnemiesSpawningManager.EverySpawnerAct (EnemyPrefabs[Random.Range (0, EnemyPrefabs.Length)]);
    }
    public void SpawnEnemy (EnemySpawner enemySpawner, GameObject enemyPrefab)
    {
        EnemiesSpawningManager.SpawnerAct (enemySpawner, enemyPrefab);
    }

}
