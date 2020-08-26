using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DoOnTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnTriggerEnter = null;
    [SerializeField]
    private bool DestroyEnter = false;
    [SerializeField]
    private UnityEvent OnTriggerStay = null;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnTriggerEnter?.Invoke();
            if (DestroyEnter)
                Destroy(this);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnTriggerStay?.Invoke();
        }
    }
}
