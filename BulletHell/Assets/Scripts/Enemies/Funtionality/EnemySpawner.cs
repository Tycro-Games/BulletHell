using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnStart = null;
    private void Start()
    {
        EnemiesSpawningManager.currentSpawners.Add(this);
        OnStart?.Invoke();
    }
    public void Spawn(GameObject enemyPrefab)
    {
        GameObject enemy = Spawner.Spawn(enemyPrefab, transform.position, Quaternion.identity, transform);
        EnemyManager.currentEnemies.Add(enemy);
    }

}
