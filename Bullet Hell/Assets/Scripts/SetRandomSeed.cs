using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomSeed
{



    public SetRandomSeed (bool UseRandomSeed = true, int seed = 0)
    {
        if (UseRandomSeed)
        {
            seed = Random.Range (0, int.MaxValue);
        }
        Random.InitState (seed);
    }
}
