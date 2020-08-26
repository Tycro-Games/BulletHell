using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager;
    [SerializeField]
    private UnityEvent[] OnEnd = null;
    int i = 0;
    private void Awake()
    {
        currentEnemies = new List<GameObject>();
        if (enemyManager == null)
            enemyManager = this;
        else
            Destroy(gameObject);
    }
    public static List<GameObject> currentEnemies;
    public void CheckList()
    {
        if (currentEnemies.Count == 0 && i < OnEnd.Length)
        {
            OnEnd[i]?.Invoke();
            i++;
        }
    }

}
