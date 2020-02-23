using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private int healthPoints;
    private void OnEnable ()
    {
        Invoke ("PretendToDoSomething", 2);
    }
    public void Init (EnemyInfo info)
    {
        healthPoints = info.HealthPoints;

    }
    public void PretendToDoSomething ()
    {
        Debug.Log (healthPoints);
        
    }
}
