using UnityEngine;

public enum Movement { Run, Chase, Stay }

[CreateAssetMenu (fileName = "new movement", menuName = "Create enemies/new movement")]
public class AIEnemyMovement : ScriptableObject
{
    public Transform PlayerTransfrom { get; private set; }
    public Movement TypeOfMove;

    public void SetTransfrom (Transform player)
    {
        PlayerTransfrom = player;
    }
}