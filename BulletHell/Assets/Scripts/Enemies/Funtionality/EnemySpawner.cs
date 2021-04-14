using UnityEngine;

namespace Bog.Assets.Scripts.Enemies.Funtionality
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawn enemyPrefab = null;

        private RoomTriggerStart triggerStart = null;

        private void Start()
        {
            triggerStart = GetComponentInParent<RoomTriggerStart>();
            triggerStart.OnStart += Spawn;
        }

        public void Spawn()
        {
            GameObject enemy = Spawner.Spawn(enemyPrefab.Enemies[Random.Range(0, enemyPrefab.Enemies.Length)], transform.position, Quaternion.identity, transform);
            RoomTriggerStart.currentEnemies.Add(enemy);
        }
    }
}