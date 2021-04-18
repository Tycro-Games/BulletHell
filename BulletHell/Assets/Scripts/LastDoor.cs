using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDoor : MonoBehaviour
{
    private Collider2D doorCollide = null;

    private void Start()
    {
        doorCollide = GetComponent<Collider2D>();
        doorCollide.enabled = false;
        DoorManager doorManager = FindObjectOfType<DoorManager>();
        doorManager.doors.Add(this);
    }

    public void Activate()
    {
        doorCollide.enabled = true;
    }
}