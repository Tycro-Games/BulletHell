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
    private Vector2 MousePos;
    void Start ()
    {
        cam = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        newRotation = transformToTurn.rotation;

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
        Transform previousTransform = transformToRotate;

        while (true)
        {            
                Quaternion rotation = Quaternion.RotateTowards (previousTransform.rotation, newRotation, RotationSpeed);

                transformToRotate.RotateAround (transform.position, Vector3.forward, Vector2.SignedAngle (previousTransform.localPosition, Input));

                transformToRotate.rotation = rotation;
                yield return null;           
        }
    }
    public void SetRotationTurn (InputAction.CallbackContext ctx)
    {
        Input = ctx.ReadValue<Vector2>();
        if (Input == Vector2.zero)
            return;


        if (Input.magnitude > 1)//it is a mouse
        {
            Input = (cam.ScreenToWorldPoint (Input) - transformToTurn.position).normalized; //make it a direction
        }

        newRotation = Quaternion.LookRotation (transform.forward, Input);
    }
}
