using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static Transform playerTransform = null;

    private AIDestinationSetter setter;

    private BaseEnemy atack;

    [SerializeField]
    private bool Shooting = false;
    [SerializeField]
    private bool damageProximity = false;

    [Header ("RepathSpeeds")]
    [SerializeField]
    private float RepathSpeed = 0.75f;
    void Start()
    {
        setter = GetComponentInParent<AIDestinationSetter> ();

        atack = GetComponentInChildren<BaseEnemy> ();

        atack.Init (Shooting, damageProximity, RepathSpeed);
    }
}
