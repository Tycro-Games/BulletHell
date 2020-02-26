using UnityEngine;

public class PoolingObjectName : MonoBehaviour
{
    [SerializeField]
    private string entryName = "Test";
    public string EntryName
    {
        get
        {
            return entryName;
        }
    }
}
