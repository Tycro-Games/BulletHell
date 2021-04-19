using UnityEngine;

public class FlipEnemy : MonoBehaviour
{
    [SerializeField]
    private bool IsRight = true;

    private Transform target;

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (target.position.x < transform.position.x && IsRight)
            Flip();
        if (target.position.x > transform.position.x && !IsRight)
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