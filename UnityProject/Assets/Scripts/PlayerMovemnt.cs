using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerActions inputActions;
    Vector2 movement;
    [SerializeField]
    float speed = 0;
    private void Awake()
    {
        inputActions = new PlayerActions();
        inputActions.PlayerController.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Debug.Log(movement);
        rb.MovePosition((Vector2)transform.position + movement * Time.fixedDeltaTime * speed);
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

}
