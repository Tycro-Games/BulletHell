using UnityEngine;
using UnityEngine.Events;

namespace Bog.Assets.Scripts.Enemies.Funtionality
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemyPrefab = null;

        private RoomTriggerStart triggerStart = null;

        private void Start()
        {
            triggerStart = GetComponentInParent<RoomTriggerStart>();
            triggerStart.OnStart += Spawn;
            EnemiesSpawningManager.currentSpawners.Add(this);
        }

        public void Spawn()
        {
            GameObject enemy = Spawner.Spawn(enemyPrefab, transform.position, Quaternion.identity, transform);
            RoomTriggerStart.currentEnemies.Add(enemy);
        }
    }
}