using System.Collections;
using UnityEngine;

namespace Bog.Assets.Scripts.Enemies.Funtionality
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] Enemies;

        private RoomTriggerStart triggerStart = null;

        [SerializeField]
        private float minTime = 1.0f;

        [SerializeField]
        private float maxTime = 2.0f;

        private void Start()
        {
            triggerStart = GetComponentInParent<RoomTriggerStart>();
            triggerStart.OnStart += Spawn;
        }

        public void Spawn()
        {
            GameObject enemy = Spawner.Spawn(Enemies[Random.Range(0, Enemies.Length)], transform.position, Quaternion.identity, transform);
            RoomTriggerStart.currentEnemies.Add(enemy);
            enemy.SetActive(false);
            StartCoroutine(Spawning(enemy));
        }

        private IEnumerator Spawning(GameObject enemy)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            enemy.SetActive(true);
        }
    }
}