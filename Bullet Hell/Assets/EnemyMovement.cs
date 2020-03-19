using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private AIEnemyMovement movement = null;
    private void OnDrawGizmos ()
    {
        Gizmos.DrawLine (transform.position, movement.PlayerTransfrom.position);
    }

}
