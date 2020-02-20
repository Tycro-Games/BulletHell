using UnityEngine;
[CreateAssetMenu (fileName = "new enemy stats", menuName = "CreateEnemies")]
public class EnemyInfo : ScriptableObject
{
    public GameObject EnemyPrefab;
    private EnemyAI enemyAI;
    [Header ("stats")]
    public float HealthPoints = 100.0f;
    public void Initialize ( )
    {
        enemyAI = EnemyPrefab.GetComponent<EnemyAI> ();

         //asignStats
    }
}
