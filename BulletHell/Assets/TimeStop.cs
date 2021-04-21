using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    public void StopTime(float time)
    {
        Time.timeScale = time;
    }
}