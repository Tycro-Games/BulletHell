using UnityEngine;

public class PoolingObjectName : MonoBehaviour
{
    [SerializeField]
    private string entryName = "Test";
    private void Awake ()
    {
        if (entryName == "Test")
        {
            entryName = gameObject.name;
        }
    }
    public string EntryName
    {
        get
        {

            return entryName;
        }
    }
}
