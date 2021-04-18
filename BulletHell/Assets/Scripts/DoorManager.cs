using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [HideInInspector]
    public List<LastDoor> doors = new List<LastDoor>();

    public void Activate()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].Activate();
        }
    }
}