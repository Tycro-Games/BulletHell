using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speedMovement=0.0f;
    CharacterController characterController;
    PlayerInput input;
    Vector2 movement;
    private void Awake()
    {
        input = new PlayerInput();
        input.PlayerMovement.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = movement.x;
        float v = movement.y;
         Vector3 velocity = new Vector3(h, 0, v);
        characterController.Move(velocity * Time.deltaTime * speedMovement);
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
}
