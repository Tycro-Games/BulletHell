using UnityEngine;

namespace Bog.Assets.Scripts.Enemies.Funtionality
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] Enemies;

        private RoomTriggerStart triggerStart = null;

        private void Start()
        {
            triggerStart = GetComponentInParent<RoomTriggerStart>();
            triggerStart.OnStart += Spawn;
        }

        public void Spawn()
        {
            GameObject enemy = Spawner.Spawn(Enemies[Random.Range(0, Enemies.Length)], transform.position, Quaternion.identity, transform);
            RoomTriggerStart.currentEnemies.Add(enemy);
        }
    }
}