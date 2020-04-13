using UnityEngine;

public static class StaticInfo
{
    private static Vector3 playerPos;
    public static Vector3 PlayerPos
    {
        get
        {
            return playerPos;
        }
    }

    public static void GetPos (Transform transform)
    {
        playerPos = transform.position;
    }
}
