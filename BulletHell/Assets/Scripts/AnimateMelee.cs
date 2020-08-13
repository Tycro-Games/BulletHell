using UnityEngine;
using UnityEngine.AI;
public class AnimateMelee : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    private NavMeshAgent agent;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, StaticInfo.PlayerPos) <= agent.stoppingDistance)
        {
            anim.SetBool("atack", true);
        }
        else
            anim.SetBool("atack", false);
    }
}
