using UnityEngine;
[CreateAssetMenu(fileName ="new movement",menuName ="Create enemies/Create movement")]
public class AIEnemyMovement : ScriptableObject
{
    private Transform playerTransfrom;

    public Transform PlayerTransfrom { get => playerTransfrom; }
    public void SetTransfrom(Transform player)
    {
        playerTransfrom = player;
      
    }
}