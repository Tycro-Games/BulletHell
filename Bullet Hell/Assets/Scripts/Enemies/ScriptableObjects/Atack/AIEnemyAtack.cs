using UnityEngine;
using UnityEngine.Events;

public enum Attack { Melee, Ranged, Stay }

[CreateAssetMenu (fileName = "new atack", menuName = "Create enemies/new atack")]
public class AIEnemyAtack : ScriptableObject
{
    public Transform PlayerTransfrom { get; private set; }

    [HideInInspector]
    public UnityEvent OnChangeAtack;
    [SerializeField]
    private Attack typeOfAtack;
    public Attack TypeOfAtack
    {
        get
        {
            return typeOfAtack;
        }
        set
        {
            typeOfAtack = value; //assign the type of movement

            OnChangeAtack.Invoke ();     //notify that we changed the movement     
        }
    }
}
