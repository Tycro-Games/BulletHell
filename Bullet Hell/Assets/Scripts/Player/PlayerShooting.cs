using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private bool shooting = false;
    bool pressing;
    [SerializeField]
    private UnityEvent OnShoot = null;
   
    public void Shoot (InputAction.CallbackContext ctx)
    {
        pressing = ctx.ReadValue<float> () == 1 ? true : false;
        if (pressing)
        {
            if (!shooting)
            {
                shooting = true;
                StartCoroutine (Shooting ());

            }
        }
    }
    IEnumerator Shooting ()
    {
        while (pressing)
        {
            OnShoot.Invoke ();
            yield return null;
        }
        shooting = false;
    }
}
