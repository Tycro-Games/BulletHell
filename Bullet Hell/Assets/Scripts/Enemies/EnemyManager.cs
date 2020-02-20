using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemySpawner spawner;
    private void Start ( )
    {
        spawner = GetComponent<EnemySpawner> ();
    }

}
