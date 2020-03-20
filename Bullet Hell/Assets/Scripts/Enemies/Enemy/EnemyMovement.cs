using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Seeker))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private AIEnemyMovement movement = null;

    private AIPath path;
    private void Start ()
    {
        path = GetComponentInParent<AIPath> ();
    }
    public void ChangeOfMovement ()
    {
        switch (movement.TypeOfMove)
        {
            case Movement.Chase:
                path.destination = movement.PlayerTransfrom.position;
                break;
            case Movement.Run:
                //run from the player
                break;
            case Movement.Stay:
                //stay right where you are
                break;
        }
    }
    private void Update ()
    {
        ChangeOfMovement ();
    }
    private void OnDrawGizmos ()
    {
        if (movement != null)
            Gizmos.DrawLine (transform.position, movement.PlayerTransfrom.position);
    }

}
