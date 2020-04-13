using UnityEngine;

public class StaticUpdateTransform : MonoBehaviour
{
    private void Update ()
    {
        StaticInfo.GetPos (transform);
    }

}
