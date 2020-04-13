using UnityEngine;
[ExecuteInEditMode]
public class MovementRef : MonoBehaviour
{
    [SerializeField]
    private AIEnemyMovement movement = null;

    [SerializeField]
    private Transform player = null;
  
    private void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;

        if (player != null)
            movement.SetTransfrom (player);
        else
        {
            Debug.LogError ("where da fuck is the player");
        }
    }

}
