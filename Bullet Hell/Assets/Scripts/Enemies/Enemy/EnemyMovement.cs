﻿using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Seeker))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private AIEnemyMovement movement = null;

    private AIDestinationSetter setter;
    private void Start ()
    {
        setter = GetComponentInParent<AIDestinationSetter> ();
        ChangeOfMovement ();
    }
    public void ChangeOfMovement ()
    {
        switch (movement.TypeOfMove)
        {
            case Movement.Chase:
                setter.target = movement.PlayerTransfrom;
                break;
            case Movement.Run:
                //run from the player
                break;
            case Movement.Stay:
                //stay right where you are
                break;
        }
    }
    private void OnDrawGizmos ()
    {
        if (movement != null)
            Gizmos.DrawLine (transform.position, movement.PlayerTransfrom.position);
    }

}
