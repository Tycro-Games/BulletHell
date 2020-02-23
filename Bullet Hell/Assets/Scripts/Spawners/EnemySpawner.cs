using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private GameObject spawnedObject;
    private EnemyAI enemyAI;
    private void OnEnable ()
    {
        EnemyManager.onSpawn += Spawn;
    }
    private void OnDisable ()
    {
        EnemyManager.onSpawn -= Spawn;
    }

    public void Spawn (EnemyInfo enemyInfo)
    {
        spawnedObject = PoolingObjectsSystem.Instantiate (enemyInfo.EnemyPrefab, transform.position, Quaternion.identity);
        spawnedObject.transform.SetParent (transform);

        enemyAI = spawnedObject.GetComponent<EnemyAI> ();
        enemyAI.Init (enemyInfo);

    }
}
