using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwishSwash : MonoBehaviour
{
    public Camera mapCamera, roomCamera;
    public bool first = true;
    public bool camSwitch = true;
    GameObject[] theObjects;

    private void Update()
    {
        if(Input.GetKeyDown("m"))
        {
            if(first != false)
                theObjects = GameObject.FindGameObjectsWithTag("Room");
            first = false;
            camSwitch = !camSwitch;
            roomCamera.gameObject.SetActive(camSwitch);
            mapCamera.gameObject.SetActive(!camSwitch);
            
            foreach(GameObject obj in theObjects)
            {
                obj.SetActive(camSwitch);
            }
        }
    }
}
