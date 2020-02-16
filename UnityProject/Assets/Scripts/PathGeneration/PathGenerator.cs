using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathGenerator
{

    public static Path CreatePath(Vector3 startPos, Vector3 endPos)
    {
        Path path = new Path(Vector3.zero);
        path.MovePoint(1, startPos);
        path.MovePoint(1, startPos);
        return path;
    }

}
