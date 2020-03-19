using UnityEngine;

public class MovementRef : MonoBehaviour
{
    [SerializeField]
    private AIEnemyMovement movement = null;

    public static Transform PlayerPosition;

    private void Start ()
    {
        PlayerPosition = GameObject.FindGameObjectWithTag ("Player").transform;


        if (PlayerPosition != null)
            SetTransform (PlayerPosition);
        else
        {
            Debug.LogError ("where da fuck is the player");
        }
    }
    public void SetTransform (Transform player)
    {
        movement.SetTransfrom (player);
    }

}
