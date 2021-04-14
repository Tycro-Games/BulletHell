using UnityEngine;

namespace Bog.Assets.Scripts.Enemies.Funtionality
{
    [CreateAssetMenu(fileName = "new enemySpawn", menuName = "Create staff/EnemySpawn", order = 0)]
    public class EnemySpawn : ScriptableObject
    {
        public GameObject[] Enemies;
    }
}