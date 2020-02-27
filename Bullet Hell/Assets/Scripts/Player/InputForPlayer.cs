using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof (PlayerInput))]
public class InputForPlayer : MonoBehaviour
{
    protected PlayerInput inputMaster = null;
    public virtual void Awake ()
    {
        inputMaster = GetComponent<PlayerInput> ();
    }
}
