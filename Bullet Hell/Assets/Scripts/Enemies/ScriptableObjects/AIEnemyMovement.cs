using UnityEngine;
using UnityEngine.Events;

public enum Movement { Run, Chase, Stay }

[CreateAssetMenu (fileName = "new movement", menuName = "Create enemies/new movement")]
public class AIEnemyMovement : ScriptableObject
{
    public Transform PlayerTransfrom { get; private set; }

    [HideInInspector]
    public UnityEvent OnChangeMovement;
    [SerializeField]
    private Movement typeOfMove;
    public Movement TypeOfMove
    {
        get
        {
            return typeOfMove;
        }
        set
        {
            typeOfMove = value; //assign the type of movement

            OnChangeMovement.Invoke ();     //notify that we changed the movement     
        }
    }
    public void SetTransfrom (Transform player)
    {
        PlayerTransfrom = player;
    }
}