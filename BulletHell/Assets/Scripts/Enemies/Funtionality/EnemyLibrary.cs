using Bog.Assets.Scripts.Enemies.Funtionality;
using UnityEngine;

public class EnemyLibrary : MonoBehaviour
{
    public GameObject[] EnemyPrefabs = null;

    public static EnemyLibrary enemyLibrary = null;

    [SerializeField]
    private Transform parentSpawners = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (enemyLibrary == null)
            enemyLibrary = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        EnemiesSpawningManager.EnemySpawnersParent = parentSpawners;//init
    }

    public void AllSpawnersEnemy()
    {
        EnemiesSpawningManager.EverySpawnerAct();
    }

    public void SpawnEnemy(EnemySpawner enemySpawner)
    {
        EnemiesSpawningManager.SpawnerAct(enemySpawner);
    }
}