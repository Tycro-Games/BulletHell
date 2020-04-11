using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private Transform transformToTurn = null;
    [SerializeField]
    private float RotationSpeed = 10.0f;
    [SerializeField]
    private float speedMovement = 0.0f;
    Rigidbody rb;
    [SerializeField]
    private float Height = 0.0f;

    //Input
    private Camera cam;
    private Vector2 movement;
    private Quaternion newRotation;
    private Vector2 Input;

    void Start ()
    {
        cam = Camera.main;

        rb = GetComponent<Rigidbody> ();

        StartCoroutine (Rotate (transformToTurn));

    }
    private void FixedUpdate ()
    {
        Move ();
    }
    public void SetMovement (InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2> ();
    }
    private void Move ()
    {
        Vector3 move = new Vector3 (movement.x, Height, movement.y);
        move *= Time.fixedDeltaTime * speedMovement;
        rb.MovePosition (rb.position + move);

    }
    IEnumerator Rotate (Transform transformToRotate)
    {

        Transform previousTransform = transformToRotate;//save the transform

        PlayerInput that = GetComponentInParent<PlayerInput> ();//ref to the player input

        while (true)
        {
            Vector3 dir = (CursorController.MousePosition () - transform.position).normalized;
            Quaternion newRotation = Quaternion.LookRotation (dir, transform.up);

            transformToRotate.rotation = Quaternion.RotateTowards (previousTransform.rotation, newRotation, RotationSpeed);

            transformToRotate.RotateAround (transform.position, Vector3.up, Vector3.SignedAngle (transformToRotate.localPosition,dir,Vector3.up));


            yield return null;
        }
    }
   
    public void SetRotationTurn (InputAction.CallbackContext ctx) //controller
    {

        Input = ctx.ReadValue<Vector2> ();


        newRotation = Quaternion.LookRotation (transform.forward, Input);
    }

}
