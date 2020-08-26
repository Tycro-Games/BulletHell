using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System;
public class EnemyController : MonoBehaviour
{
    public static Transform playerTransform = null;

    protected NavMeshAgent agent = null;

    protected Transform enemyTransform = null;

    private BaseEnemy[] atack;
    [SerializeField]
    private bool Rotate = true;

    [SerializeField]
    private UnityEvent ToStart = null;

    [Header("RepathSpeeds")]
    [SerializeField]
    private float RepathSpeed = 0.75f;
    public delegate void Onhit(int dg);
    public static Onhit HitEvent = null;
    Transform target;

    void Awake()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        agent = GetComponentInParent<NavMeshAgent>();
        enemyTransform = agent.transform;

        if (!Rotate)
            agent.updateRotation = false;

        atack = GetComponentsInChildren<BaseEnemy>();
        for (int i = 0; i < atack.Length; i++)
        {
            atack[i].Init(target, RepathSpeed, enemyTransform, agent);
        }
    }
    private void Start()
    {
        ToStart?.Invoke();
    }
}

