using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorUpdate : MonoBehaviour
{
    private Animator animator = null;

    private NavMeshAgent agent;
    void Start ()
    {
        agent = GetComponentInParent<NavMeshAgent> ();
        animator = GetComponent<Animator> ();
    }
    void Update ()
    {
        if (agent.remainingDistance<=agent.stoppingDistance)
        {
            animator.SetBool ("Stopped", true);
        }
        else
        {
            animator.SetBool ("Stopped", false);
        }
    }
}
