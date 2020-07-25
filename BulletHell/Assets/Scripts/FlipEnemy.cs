using UnityEngine;

public class FlipEnemy : MonoBehaviour
{
    [SerializeField]
    private bool IsRight = true;


    private void Update ()
    {
            if (StaticInfo.PlayerPos.x < transform.position.x && IsRight)
                Flip ();
        if (StaticInfo.PlayerPos.x > transform.position.x && !IsRight)
            Flip ();
    }
    public void Flip ()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        IsRight = !IsRight;
    }
}
