using UnityEngine;
using UnityEngine.AI;
public class AnimateMelee : MonoBehaviour
{
    Animator anim = null;

    [SerializeField]
    private NavMeshAgent agent = null;
    Vector2 target;
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target) <= agent.stoppingDistance)
        {
            anim.SetBool("atack", true);
        }
        else
            anim.SetBool("atack", false);
    }
}
