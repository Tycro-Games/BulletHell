using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void Spawn (GameObject enemyPrefab)
    {
        Spawner.Spawn (enemyPrefab, transform, transform);

    }
}
