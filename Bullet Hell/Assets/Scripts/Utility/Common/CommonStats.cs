using UnityEngine;

public class CommonStats : MonoBehaviour
{
    [SerializeField]
    protected Stats stats = null;

    [Header ("Stats")]
    [SerializeField]
    private int statingHP = 100;

    public virtual void Start ()
    {
        stats.Health = statingHP;
    }
}
