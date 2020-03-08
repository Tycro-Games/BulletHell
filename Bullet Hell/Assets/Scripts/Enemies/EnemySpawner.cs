using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public void Spawn ()
    {
        Spawner.Spawn (enemyPrefab, transform, transform);
    }
}
