using Pathfinding;
using System.Collections;
using UnityEngine;
public class EnemyMovementController : MonoBehaviour
{
    [SerializeField]
    private AIEnemyMovement movement = null;

    private AIDestinationSetter setter;
    
    private void OnEnable ()
    {
        movement.OnChangeMovement.AddListener (ChangeOfMovement);
    }
    private void OnDisable ()
    {
        movement.OnChangeMovement.RemoveListener (ChangeOfMovement);
    }
    private void Start ()
    {
        setter = GetComponentInParent<AIDestinationSetter> ();
        ChangeOfMovement ();
    }
    public void ChangeOfMovement ()
    {
        switch (movement.TypeOfMove)
        {
            case Movement.Chase:
                setter.target = movement.PlayerTransfrom;
                break;
            case Movement.Run:
                //run from the player
                break;
            case Movement.Stay:
                setter.target = null;
                break;
        }
    }
}
