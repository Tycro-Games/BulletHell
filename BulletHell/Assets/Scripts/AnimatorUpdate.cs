using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorUpdate : MonoBehaviour
{
    [SerializeField]
    private bool IsRight = true;

    private Animator animator = null;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (agent == null)
            return;
        if (agent.remainingDistance <= agent.stoppingDistance || agent.isStopped)
        {
            animator.SetBool("Stopped", true);
        }
        else
        {
            animator.SetBool("Stopped", false);
        }

        if (agent.destination.x < transform.position.x && IsRight)
            Flip();
        if (agent.destination.x > transform.position.x && !IsRight)
            Flip();
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        IsRight = !IsRight;
    }
}