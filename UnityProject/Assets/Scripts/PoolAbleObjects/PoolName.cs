using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolName : MonoBehaviour
{
    [SerializeField]
    private string PoolableClass = "";
    public string PoolableObjectClass
    {
        get { return (PoolableClass); }
    }
}