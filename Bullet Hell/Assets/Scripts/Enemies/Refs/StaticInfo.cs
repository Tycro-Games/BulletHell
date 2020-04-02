using UnityEngine;

public static class StaticInfo
{
    private static Vector2 playerPos;
    public static Vector2 PlayerPos
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
