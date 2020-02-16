using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{

    [HideInInspector]
    public Path path;
    public Color startAnchordCol = Color.white;
    public Color endAnchorCol = Color.black;
    public Color anchorCol = Color.red;
    public Color controlCol = Color.white;
    public Color segmentCol = Color.green;
    public Color selectedSegmentCol = Color.yellow;
    public float anchorDiameter = .1f;
    public float controlDiameter = .075f;
    public bool displayControlPoints = true;
    public void GeneratePath()
    {
        path = PathGenerator.CreatePath(transform.position, transform.position);
    }
    public void CreatePath()
    {
        path = new Path(transform.position);
    }

    void Reset()
    {
        CreatePath();
    }
}
