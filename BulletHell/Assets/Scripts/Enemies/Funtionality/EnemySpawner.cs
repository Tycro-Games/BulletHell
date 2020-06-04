using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void Spawn (GameObject enemyPrefab)
    {
        GameObject enemy = Spawner.Spawn (enemyPrefab, transform.position,transform.rotation, transform);       
    }
}
