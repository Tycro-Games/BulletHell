using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    PlayerMovement movement;

    [SerializeField]
    private Animator anim = null;
    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        float x = movement.GetMovement().x;
        float y = movement.GetMovement().y;
        if (Mathf.RoundToInt(x) == 0 && Mathf.RoundToInt(y) == 0)
        {
            anim.SetFloat("X", 0);
            anim.SetTrigger("stop");
        }
        else
        {
            ChangeDirection(x);
        }
    }
    void ChangeDirection(float x)
    {
        anim.SetFloat("X", 1);
    }

}
