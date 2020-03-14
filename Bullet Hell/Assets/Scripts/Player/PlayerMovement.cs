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
    private Vector2 InputMousePosition;
    private Vector2 MousePos;
    void Start ()
    {
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;

        newRotation = transformToTurn.rotation;

        rb = GetComponent<Rigidbody2D> ();
        StartCoroutine (Rotate (transformToTurn));

    }
    private void FixedUpdate ()
    {
        Move ();
    }
    private void Update ()
    {
        SetRotationTurn ();
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
            float CurveX = Mathf.InverseLerp (previousTransform.rotation.z, newRotation.z, transformToRotate.rotation.z);
            while (CurveX <= 1)
            {
                Quaternion rotation = Quaternion.RotateTowards (previousTransform.rotation, newRotation, RotationSpeed);

                transformToRotate.RotateAround (transform.position, Vector3.forward, Vector2.SignedAngle (previousTransform.localPosition, MousePos));

                transformToRotate.rotation = rotation;
                yield return null;
            }
        }
    }
    public void SetRotationTurn ()
    {

        InputMousePosition = Mouse.current.position.ReadValue ();
         MousePos = (cam.ScreenToWorldPoint (InputMousePosition) - transform.position).normalized;
        if (InputMousePosition != Vector2.zero)
        {
            newRotation = Quaternion.LookRotation (transform.forward, MousePos);
        }

    }

    public static Vector3 ClampMagnitude (Vector3 v, float max, float min)
    {
        double sm = v.sqrMagnitude;
        if (sm > (double)max * (double)max) return v.normalized * max;
        else if (sm < (double)min * (double)min) return v.normalized * min;
        return v;
    }
}
