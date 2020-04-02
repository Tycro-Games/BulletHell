using UnityEngine;

public class CommonStats : MonoBehaviour
{
    [SerializeField]
    protected Stats stats = null;

    [Header ("Stats")]
    [SerializeField]
    protected int HP = 100;

    public virtual void Start ()
    {
        stats.Health = HP;
    }
}
