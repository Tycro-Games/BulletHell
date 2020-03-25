using System.Collections;
using UnityEngine;
using Pathfinding;
public class EnemyAtack : MonoBehaviour
{
    protected AIPath path;

    [SerializeField]
    protected Stats stats = null;

    // Start is called before the first frame update
    void Start ()
    {
        path = GetComponentInParent<AIPath> ();
    }
}
