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
    Rigidbody2D rb;

    //Input
    private Camera cam;
    private Vector2 movement;
    private Quaternion newRotation;
    private Vector2 Input;

    void Start ()
    {
        cam = Camera.main;




        rb = GetComponent<Rigidbody2D> ();

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
        rb.MovePosition (rb.position + movement * Time.fixedDeltaTime * speedMovement);

    }
    IEnumerator Rotate (Transform transformToRotate)
    {

        Transform previousTransform = transformToRotate;//save the transform

        PlayerInput that = GetComponentInParent<PlayerInput> ();//ref to the player input
        newRotation = Quaternion.LookRotation (transform.forward, transformToRotate.up);
        while (true)
        {

            if (that.currentControlScheme == "Keyboard+Mouse")
            {
                SetRotationTurn (Mouse.current.position.ReadValue ());
            }

            transformToRotate.rotation = Quaternion.RotateTowards (previousTransform.rotation, newRotation, RotationSpeed);

            transformToRotate.RotateAround (transform.position, Vector3.forward, Vector2.SignedAngle (transformToRotate.localPosition, Input));


            yield return null;
        }
    }
    void SetRotationTurn (Vector2 input)
    {

        Input = input;

        if (Input.magnitude > 1)//it is a mouse
        {
            Input = (cam.ScreenToWorldPoint (Input) - transform.position).normalized; //make it a direction
        }

        newRotation = Quaternion.LookRotation (transform.forward, Input);
    }
    public void SetRotationTurn (InputAction.CallbackContext ctx) //controller
    {

        Input = ctx.ReadValue<Vector2> ();


        newRotation = Quaternion.LookRotation (transform.forward, Input);
    }
}
