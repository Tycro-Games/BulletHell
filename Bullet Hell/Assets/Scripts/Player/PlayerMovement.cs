using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private AnimationCurve RotationCurve = null;
    [SerializeField]
    private float RotationSpeed = 10.0f;
    [SerializeField]
    private float speedMovement = 0.0f;
    Rigidbody2D rb;

    //Input
    private Camera cam;
    private Vector2 movement;
    private Quaternion newRotation;

    private void Awake ()
    {
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Confined;
    }
    void Start ()
    {
        newRotation = Quaternion.LookRotation (transform.forward, transform.up);

        rb = GetComponent<Rigidbody2D> ();
        StartCoroutine(Rotate ());

    }
    private void FixedUpdate ()
    {
        Move ();
    }
    private void Update ()
    {
        
    }
    public void SetMovement (InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2> ();
    }
    private void Move ()
    {
        rb.MovePosition (rb.position + movement * Time.fixedDeltaTime * speedMovement);
    }
    IEnumerator  Rotate ()
    {
        float initRot = transform.rotation.z;
        
        
        while (true)
        {
            float CurveX = Mathf.InverseLerp (initRot, newRotation.z, transform.rotation.z);
            while (CurveX <= 1)
            {
                CurveX += Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards (transform.rotation, newRotation, RotationCurve.Evaluate (CurveX)*RotationSpeed);
                yield return null;
            }
        }
    }
    public void SetRotationTurn (InputAction.CallbackContext ctx)
    {
        if (ctx.canceled == true)
            return;

        Vector2 input = ctx.ReadValue<Vector2> ();
        Vector2 MousePos = (cam.ScreenToWorldPoint (input) - transform.position).normalized;
        if (input != Vector2.zero)
        {
            newRotation = Quaternion.LookRotation (transform.forward, MousePos);
        }
    }
}
