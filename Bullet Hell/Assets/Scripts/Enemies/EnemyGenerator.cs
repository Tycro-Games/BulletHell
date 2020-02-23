using UnityEngine;

public static class EnemyGenerator
{

    public static GameObject RandomPrefab (GameObject[] prefabs)
    {
        int index = Random.Range (0, prefabs.Length);
        return prefabs[index];
    }
    
}
