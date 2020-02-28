using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private bool shooting = false;
    bool pressing;
    public void Shoot (InputAction.CallbackContext ctx)
    {
        pressing = ctx.ReadValue<Vector2> () != Vector2.zero ? true : false;
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
            Debug.Log ("Shoot");
            yield return null;
        }


        shooting = false;
    }





}
