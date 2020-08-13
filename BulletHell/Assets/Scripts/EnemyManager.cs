using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager;
    [SerializeField]
    private UnityEvent[] OnEnd;
    int i = 0;
    private void Start()
    {
        if (enemyManager == null)
            enemyManager = this;
        else
            Destroy(gameObject);
    }
    public static List<GameObject> currentEnemies = new List<GameObject>();
    public void CheckList()
    {
        if (currentEnemies.Count == 0 && i < OnEnd.Length)
        {
            OnEnd[i]?.Invoke();
            i++;
        }
    }

}
