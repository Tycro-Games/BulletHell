using UnityEngine;

public class FlipEnemy : MonoBehaviour
{
    [SerializeField]
    private bool IsRight = true;
    Vector2 target;
    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform.position;
    }
    private void Update()
    {
        if (target.x < transform.position.x && IsRight)
            Flip();
        if (target.x > transform.position.x && !IsRight)
            Flip();
    }
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        IsRight = !IsRight;
    }
}
