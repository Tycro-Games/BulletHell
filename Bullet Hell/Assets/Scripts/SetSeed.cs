using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSeed : MonoBehaviour
{
    public string stringSeed = "seed string";
    public bool useStringSeed;
    public int seed;
    public bool randomizeSeed;

    private void Awake()
    {
        if(useStringSeed)
        {
            seed = stringSeed.GetHashCode();
        }

        if(randomizeSeed)
        {
            seed = Random.Range(0, 9999999);
        }

        Random.InitState(seed);
    }
}
