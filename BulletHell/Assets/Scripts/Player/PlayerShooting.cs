using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnShoot = null;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shooting();
        }

    }
    void Shooting()
    {
        OnShoot?.Invoke();
    }
}
