using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public EnemyInfo enemyInfo = null; 
    private GameObject spawnedObject;

    public void Spawn ( )
    {
        spawnedObject = Instantiate (enemyInfo.EnemyPrefab, transform.position, Quaternion.identity);
        spawnedObject.transform.SetParent (transform);
    }
}
