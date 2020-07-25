using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private void Start ()
    {
        EnemiesSpawningManager.currentSpawners.Add (this);
    }
    public void Spawn (GameObject enemyPrefab)
    {
        GameObject enemy = Spawner.Spawn (enemyPrefab, transform.position, transform.rotation, transform);
    }

}
