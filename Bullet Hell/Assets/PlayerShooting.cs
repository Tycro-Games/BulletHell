using System;
using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private bool shooting = false;
    
    public void Shoot ()
    {

        
        

        if (shooting)
        { 
            
            StartCoroutine (Shooting ());
        }
    }
    IEnumerator Shooting ()
    {
        Debug.Log ("Shooting");
        while (shooting)
        {
            Debug.Log ("Shoot");
            yield return null;
        }

    }
    




}
